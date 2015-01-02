React = require 'react/addons'
ProfilesActions = require '../actions/profiles'

Profile = React.createClass

  animateProfile: (event)->
    ProfilesActions.selectProfile @props.item

  componentDidUpdate: ->
    if(@props.item.selected)
      @refs['profileContainer'].getDOMNode().focus()

  render: ->
    devName = if  @props.item.profile.real_name_normalized then @props.item.profile.real_name_normalized  else 'Oh Noes! I haven\'t updated my slack profile '
    devTitle = if  @props.item.profile.title then @props.item.profile.title  else 'Not sure What I do!'

    tabindex = "#{@props.item.index}"
    cx = React.addons.classSet
    classes = cx(
      'profile': true,
      'profile_animate': @props.item.selected
    )

    <li>
      <div tabIndex={tabindex} className={classes} onClick={@animateProfile} ref="profileContainer" >
        <img src={@props.item.profile.image_192} />
        <div className ='dev_name'> {devName} </div>
        <div className='dev_info'>
          <div className ='dev_title' > {devTitle} </div>
          <div className='dev_phone' >
              {@props.item.profile.phone}
          </div>
          <div className='dev_skype'>
            {@props.item.profile.skype}
          </div>
          <div className='dev_hangouts'>
            coming soon...
          </div>
        </div>
      </div>
    </li>

module.exports = Profile
