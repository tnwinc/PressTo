express = require('express')
app = express()
cors = require 'cors'
server = require('http').Server(app)
#io = require('socket.io')(server)
DevInfo = require('./devinfo')

app.use cors()

server.listen 8888, ()-> console.log 'listening on port 8888'


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
