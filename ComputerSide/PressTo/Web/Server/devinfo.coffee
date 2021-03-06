Spreadsheet = require('edit-google-spreadsheet')
_ = require 'underscore'
request = require 'request-json'
client = request.newClient 'https://slack.com/'
Secret = require './secret'
Q = require 'q'

module.exports = ()->
  payload = []
  deferred = Q.defer()
  Spreadsheet.load
    debug: true
    spreadsheetId: '1ha75e_ALAzkkbOv8mp_s5HhD1okQiqGlYKQhckkjHe8'
    worksheetName: "People"
    worksheetId: "od6"
    oauth:
      email: "686870306377-e9n4nmb6e13r88qgtjrd7vdfuqjs2q0e@developer.gserviceaccount.com"
      keyFile: "outbox.pem"
  , sheetReady = (err, spreadsheet)->
    throw err  if err
    spreadsheet.receive (err, rows, info)->
      throw err  if err
      devs = _.filter rows, (row, index)-> index >2 and index < 29

      client.get "api/users.list?token=#{Secret.token}", (err, response, profiles)->
        _(devs).each (dev, index)->
          info =
            id: index
            name: dev[1]
            title: 'not sure what I do :-/'
            phone: dev[2]
            email: dev[3]
            vacation: dev[4]
            hangouts_id: dev[6]
            imageUrl: null
            skype_id: null

          memberFound = _(profiles.members).find (member)-> dev[1] is member.profile.real_name_normalized
          if memberFound
              info.imageUrl = memberFound.profile.image_192
              info.skype_id = memberFound.profile.skype
              info.title = memberFound.profile.title
          payload.push info
        sortedPayload = _(payload).sortBy('name')
        deferred.resolve sortedPayload
  return deferred.promise
