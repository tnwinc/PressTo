var app = require('http').createServer(handler)
var io = require('socket.io')(app);
var SocketSerialPort = require('socket.io-serial').SerialPort;
var serialDevices = require("serialport");
var fs = require('fs');

var serialPort = require("serialport").SerialPort;

var Arduino;
var ArduinoDataHandlers = new Array();
ArduinoDataHandlers.push({ socket:undefined, method:function(data) { console.log('Arduino data: ' + data); }});

 
serialDevices.list(function (err, ports) {
  ports.forEach(function(port) {
    if (port.manufacturer.indexOf("Arduino") > -1)
    {
      Arduino = new serialPort(port.comName,{ baudrate: 9600, buffersize: 1024, parser: serialDevices.parsers.readline('\r\n') }, false);
      Arduino.open(function(error) {
        if (error){
          console.log('Failed to open Arduino connection: ' + error);
        } else {
          console.log('Opened Arduino connection!');
          Arduino.on('data', function(data) {
            ArduinoDataHandlers.forEach(function(handlerInfo) {
              handlerInfo.method(data, handlerInfo.socket);
            });
          });
        }
      });
    }
  });
});

app.listen(8888);

function handler (req, res) {
  fs.readFile(__dirname + '/index.html',
  function (err, data) {
    if (err) {
      res.writeHead(500);
      return res.end('Error loading index.html');
    }

    res.writeHead(200);
    res.end(data);
  });
}

io.on('connection', function (socket) {
  ArduinoDataHandlers.push({ socket:socket, method:function(data, socket2){
    if (data.indexOf('wheel:') >= 0) {
      var parsedOffset = parseInt(data.substring(data.indexOf(':') + 1));
      if (parsedOffset != NaN)
        socket2.emit('command', {command: 'move', offset: parsedOffset});
    }
    else if (data.indexOf('wheelButton:up') >= 0){
      socket2.emit('command', {command: 'click', offset: 1} );
    }
  }});
});


console.log("Server has started.");