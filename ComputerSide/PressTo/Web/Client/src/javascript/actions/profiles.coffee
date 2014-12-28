ProfilesDispatcher = require('../dispatchers/profiles')
ProfilesConstants = require('../constants/profiles');

ProfileActions =
  receiveProfiles: (data)->
    ProfilesDispatcher.handleViewAction
      actionType: ProfilesConstants.RECEIVE_PROFILES,
      data: data

module.exports = ProfileActions
