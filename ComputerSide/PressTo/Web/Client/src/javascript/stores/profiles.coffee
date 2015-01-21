
ProfilesDispatcher = require("../dispatchers/profiles")
EventEmitter = require("events").EventEmitter
ProfilesConstants = require("../constants/profiles")
_ = require("underscore")

_profiles = []
_selectedProfileIndex = 0

loadProfiles = (data)->
  _profiles = data
  _profiles.map (profile)-> profile.selected = false
  return

selectProfile = (selectedProfile)->
  _profiles.map (profile, index)->
    if profile.id is selectedProfile.id
      profile.selected = true
      _selectedProfileIndex = index
    else
      profile.selected = false
  return _profiles

moveRight = (offset)->
  newIndex = offset + _selectedProfileIndex
  if newIndex > (_profiles.length - 1)  then newIndex = _profiles.length - 1
  selectProfile _profiles[newIndex]

moveLeft = (offset)->
  newIndex = _selectedProfileIndex - offset
  if newIndex < 0  then newIndex = 0
  selectProfile _profiles[newIndex]

ProfilesStore = _.extend({}, EventEmitter::,
  getSelectedProfile: -> _profiles[_selectedProfileIndex]

  getProfiles: -> _profiles

  emitChange: ->
    @emit "change"
    return

  addChangeListener: (callback)->
    @on "change", callback
    return

  removeChangeListener: (callback)->
    @removeListener "change", callback
    return
)

ProfilesDispatcher.register (payload)->
  action = payload.action
  text = undefined
  switch action.actionType
    when ProfilesConstants.RECEIVE_PROFILES
      loadProfiles action.data
    when ProfilesConstants.SELECT_PROFILE
      selectProfile action.profile
    when ProfilesConstants.MOVE_RIGHT
      moveRight action.offset
    when ProfilesConstants.MOVE_LEFT
      moveLeft action.offset
    else
      return true

  ProfilesStore.emitChange()
  true

module.exports = ProfilesStore
