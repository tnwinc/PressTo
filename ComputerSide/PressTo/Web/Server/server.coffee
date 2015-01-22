express = require('express')
app = express()
cors = require 'cors'
server = require('http').Server(app)
io = require('socket.io')(server)
SocketSerialPort = require("socket.io-serial").SerialPort
serialDevices = require("serialport")
fs = require("fs")
serialPort = require("serialport").SerialPort
DevInfo = require('./devinfo')

app.use cors()

server.listen 8888, ()-> console.log ' server listening on port 8888'


#io.on 'connect', (socket)->
  #console.log 'connection established...'
  #setInterval ()->
    #console.log 'sending move command...'
    #socket.emit 'command', {command: 'move', offset: 1}
    #console.log 'sending click command...'
    #socket.emit 'command', {command: 'click', offset: 1}
  #, 3000
  #socket.on 'clicked', (data)-> console.log 'clicked..', data.profile



app.get  '/users',
  (req, res)->
    console.log 'serving users...'
    DevInfo().done (data)-> res.send data


Arduino = undefined
ArduinoDataHandlers = new Array()
ArduinoDataHandlers.push
  socket: `undefined`
  method: (data)->
    console.log "Arduino data: " + data
    return

serialDevices.list (err, ports)->
  ports.forEach (port)->
    if port.manufacturer.indexOf("Arduino") > -1
      Arduino = new serialPort(port.comName,
        baudrate: 9600
        buffersize: 1024
        parser: serialDevices.parsers.readline("\r\n")
      , false)
      Arduino.open (error)->
        if error
          console.log "Failed to open Arduino connection: " + error
        else
          console.log "Opened Arduino connection!"
          Arduino.on "data", (data)->
            ArduinoDataHandlers.forEach (handlerInfo)->
              handlerInfo.method data, handlerInfo.socket
              return

            return

        return

    return

  return

io.on "connection", (socket)->
  ArduinoDataHandlers.push
    socket: socket
    method: (data, socket2)->
      if data.indexOf("wheel:") >= 0
        parsedOffset = parseInt(data.substring(data.indexOf(":") + 1))
        unless parsedOffset is NaN
          socket2.emit "command",
            command: "move"
            offset: parsedOffset

      else if data.indexOf("wheelButton:up") >= 0
        socket2.emit "command",
          command: "click"
          offset: 1

      return

  return
