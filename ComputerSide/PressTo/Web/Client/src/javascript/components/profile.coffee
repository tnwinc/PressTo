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
    devTitle = @props.item.title

    devVacation = if @props.item.vacation then "I will be out #{@props.item.vacation}" else ''

    tabindex = "#{@props.item.index}"
    cx = React.addons.classSet
    classes = cx(
      'profile': true,
      'profile_animate': @props.item.selected
    )

    <li>
      <div tabIndex={tabindex} className={classes} onClick={@animateProfile} ref="profileContainer" >
        <img src={@props.item.imageUrl} height=192 width=192 />
        <div className ='dev_name'> {devName} </div>
        <div className='dev_info'>
          <div className ='dev_title' >
            {devTitle}
          </div>
          <div className='dev_phone' >
              {@props.item.phone}
          </div>
          <div className='dev_hangouts'>
            {@props.item.hangouts_id}
          </div>
          <div className='dev_skype'>
            {@props.item.skype_id}
          </div>
          <div className='dev_vacation'>
            {devVacation}
          </div>

        </div>
      </div>
    </li>

module.exports = Profile
