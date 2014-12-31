
React = require 'react'
Profiles = require './components/profiles'
ProfilesActions = require("./actions/profiles")
$ = require 'jquery'

mockMoves = ->
  console.log 'moving right..'
  ProfilesActions.moveRight 2

($.getJSON 'https://slack.com/api/users.list?token=xoxb-3273904185-Ll09SxRyaLdQsVXX8CDYcwEC')
  .done (data)=>
    ProfilesActions.receiveProfiles data
    React.render(
      React.createElement(Profiles),
      document.getElementById('content')
    )
    window.setInterval mockMoves, 3000
