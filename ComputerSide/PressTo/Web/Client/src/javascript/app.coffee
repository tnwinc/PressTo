
React = require 'react'
Profiles = require './components/profiles'
ProfilesActions = require("./actions/profiles")
Transceiver = require("./utils/transceiver")
ProfilesStore = require("./stores/profiles")
$ = require 'jquery'

mockMoves = ->
  console.log 'moving right..'
  ProfilesActions.moveLeft 2

($.getJSON 'https://slack.com/api/users.list?token=xoxb-3273904185-Ll09SxRyaLdQsVXX8CDYcwEC')
  .done (data)=>
    ProfilesActions.receiveProfiles data
    React.render(
      React.createElement(Profiles),
      document.getElementById('content')
    )
    window.setTimeout ()->
      console.log 'started listening on client socket..'
      Transceiver(ProfilesActions, ProfilesStore)
    ,3000
