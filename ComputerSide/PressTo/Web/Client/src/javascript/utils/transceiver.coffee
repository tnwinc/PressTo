io = require('socket.io-client')

port = 8888
init = (ProfilesActions, ProfilesStore)->
  socket = io.connect "http://localhost:#{port}"

  socket.on 'command', (data)->
    switch data.command
      when 'move'
        console.log 'received move ..', data
        if data.offset > 0
          ProfilesActions.moveRight data.offset
        else
          ProfilesActions.moveLeft data.offset * (-1)

      when 'click'
        console.log 'received click..', data
        socket.emit 'clicked', {profile: ProfilesStore.getSelectedProfile()}

module.exports= init
