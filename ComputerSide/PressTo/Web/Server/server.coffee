express = require('express')
app = express()
cors = require 'cors'
server = require('http').Server(app)
#io = require('socket.io')(server)
app.use cors()

server.listen 8888, ()-> console.log 'listening on port 8888'

Spreadsheet = require('edit-google-spreadsheet')
_ = require 'underscore'
request = require 'request-json'
client = request.newClient 'https://slack.com/'
Secret = require './secret'
Q = require 'q'

router = express.Router()

#io.on 'connect', (socket)->
  #console.log 'connection established...'
  #setInterval ()->
    #console.log 'sending move command...'
    #socket.emit 'command', {command: 'move', offset: 1}
    #console.log 'sending click command...'
    #socket.emit 'command', {command: 'click', offset: 1}
  #, 3000
  #socket.on 'clicked', (data)-> console.log 'clicked..', data.profile
  #

getDevInfo = ()->
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
      devs = _.filter rows, (row, index)-> index >3 and index < 29

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

        deferred.resolve payload
  return deferred.promise

app.get  '/users',
  (req, res)->
    console.log 'serving users...'
    getDevInfo().done (data)-> res.send data
