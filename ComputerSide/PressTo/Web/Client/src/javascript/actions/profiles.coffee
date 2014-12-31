ProfilesDispatcher = require('../dispatchers/profiles')
ProfilesConstants = require('../constants/profiles');

ProfileActions =
  receiveProfiles: (data)->
    ProfilesDispatcher.handleViewAction
      actionType: ProfilesConstants.RECEIVE_PROFILES,
      data: data

  selectProfile: (profile)->
    ProfilesDispatcher.handleViewAction
      actionType: ProfilesConstants.SELECT_PROFILE,
      profile: profile

  moveRight: (offset)->
    ProfilesDispatcher.handleViewAction
      actionType: ProfilesConstants.MOVE_RIGHT,
      offset: offset

  moveLeft: (offset)->
    ProfilesDispatcher.handleViewAction
      actionType: ProfilesConstants.MOVE_LEFT,
      offset: offset

module.exports = ProfileActions
