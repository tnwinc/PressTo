app = require('express')()
server = require('http').Server(app)
io = require('socket.io')(server)
server.listen(8888)

io.on 'connect', (socket)->
  console.log 'connection established...'
  setInterval ()->
    console.log 'sending move command...'
    socket.emit 'command', {command: 'move', offset: 1}
    console.log 'sending click command...'
    socket.emit 'command', {command: 'click', offset: 1}
  , 5000

  socket.on 'clicked', (data)->
    console.log 'clicked..', data.profile
