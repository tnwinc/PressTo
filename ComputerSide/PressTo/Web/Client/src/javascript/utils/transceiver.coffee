io = require('socket.io-client')

port = 8888
init = (ProfilesActions)->
  socket = io.connect "http://localhost:#{port}"

  socket.on 'command', (data)->
    switch data.command
      when 'move_right'
        ProfilesActions.moveRight 1
      when 'move_left'
        ProfilesActions.moveLeft 1
      when 'move_click'
        return true

module.exports= init
