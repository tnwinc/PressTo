io = require('socket.io-client')

port = 8888
init = (ProfilesActions)->
  socket = io.connect "http://localhost:#{port}"

  socket.on 'command', (data)->
    switch data.command
      when 'move'
        if data.offset > 0
          ProfilesActions.moveRight data.offset
        else
          ProfilesActions.moveLeft data.offset * (-1)

      when 'click'
        return true

module.exports= init
