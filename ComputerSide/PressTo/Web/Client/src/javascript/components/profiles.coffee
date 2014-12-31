React = require 'react'
ProfilesStore = require '../stores/profiles'
Profile = require './profile'
_ = require 'underscore'

getProfiles = -> profiles:  ProfilesStore.getProfiles()

Profiles = React.createClass
  getInitialState: -> getProfiles()
  componentDidMount: -> ProfilesStore.addChangeListener @_onChange
  componentWillUnmount: -> ProfilesStore.removeChangeListener @_onChange

  render: ->
    profiles = _(@state.profiles).map (profile, index)->
      <Profile key={profile.id} item ={profile} />
    <ul className='profiles' >
      {profiles}
    </ul>

  _onChange: -> @setState(getProfiles())


module.exports = Profiles
