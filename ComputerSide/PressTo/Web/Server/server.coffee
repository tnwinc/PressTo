app = require('express')()
server = require('http').Server(app)
io = require('socket.io')(server)
server.listen(8090)

io.on 'connect', (socket)->
  console.log 'connection established...'
  setInterval ()->
    console.log 'sending command...'
    socket.emit 'command', {command: 'move_right'}
  , 5000
