
ProfilesDispatcher = require("../dispatchers/profiles")
EventEmitter = require("events").EventEmitter
ProfilesConstants = require("../constants/profiles")
_ = require("underscore")

_profiles = []

loadProfiles = (data)->
  _profiles = data?.members
  return


ProfilesStore = _.extend({}, EventEmitter::,

  getProfiles: ->
    _(_profiles).filter (profile)-> profile.deleted is false

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
    else
      return true

  ProfilesStore.emitChange()
  true

module.exports = ProfilesStore
