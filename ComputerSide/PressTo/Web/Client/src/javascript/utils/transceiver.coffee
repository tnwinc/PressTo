io = require('socket.io-client')

init = (ProfilesActions)->
  socket = io.connect 'http://localhost:8090'

  socket.on 'command', (data)->
    switch data.command
      when 'move_right'
        ProfilesActions.moveRight 1
      when 'move_left'
        ProfilesActions.moveLeft 1
      when 'move_click'
        return true

module.exports= init
