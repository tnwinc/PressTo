var app = require('http').createServer(handler)
var io = require('socket.io')(app);
var fs = require('fs');
var serialPort = require("serialport");

var ports = "Empty";

serialPort.list(function (err, ports) {
  ports.forEach(function(port) {
    ports = port.comName;
    console.log(port.comName);
    console.log(port.pnpId);
    console.log(port.manufacturer);
  });
});

function onRequest(request, response) {
  console.log("Request received.");
  response.writeHead(200, {"Content-Type": "text/plain"});
  serialPort.list(function(err, ports) {
    ports.forEach(function(port) {
      ports = port.comName;
      console.log(port.comName);
    });
  });
  console.log(ports);
  response.write(ports);
  response.end();
}

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
  socket.emit('news', { hello: 'world' });
  serialPort.list(function(err, ports) {
    ports.forEach(function(port) {
      socket.emit('pressTo', port.comName);
    });
  });
  socket.on('my other event', function (data) {
    console.log(data);
  });
});


console.log("Server has started.");
console.log(ports);
