React = require 'react/addons'
ProfilesActions = require '../actions/profiles'

Profile = React.createClass

  animateProfile: (event)->
    ProfilesActions.selectProfile @props.item

  componentDidUpdate: ->
    if(@props.item.selected)
      @refs['profileContainer'].getDOMNode().focus()

  render: ->
    devName = @props.item.name
    devTitle = @props.title

    tabindex = "#{@props.item.index}"
    cx = React.addons.classSet
    classes = cx(
      'profile': true,
      'profile_animate': @props.item.selected
    )

    <li>
      <div tabIndex={tabindex} className={classes} onClick={@animateProfile} ref="profileContainer" >
        <img src={@props.item.imageUrl} />
        <div className ='dev_name'> {devName} </div>
        <div className='dev_info'>
          <div className ='dev_title' > {devTitle} </div>
          <div className='dev_phone' >
              {@props.item.phone}
          </div>
          <div className='dev_skype'>
            {@props.item.skype_id}
          </div>
          <div className='dev_hangouts'>
            {@props.item.hangouts_id}
          </div>
          <div className='dev_vacation'>
            Vacation: {@props.item.vacation}
          </div>

        </div>
      </div>
    </li>

module.exports = Profile
