React = require 'react'

Profile = React.createClass
  render: ->
    devName = if  @props.item.profile.real_name_normalized then @props.item.profile.real_name_normalized  else 'Oh Noes! I haven\'t updated my slack profile '
    devTitle = if  @props.item.profile.title then @props.item.profile.title  else 'Not sure What I do!'

    tabindex = "#{@props.item.index}"
    <li>
      <div tabIndex={tabindex} className='profile profile_animate'>
        <img src={@props.item.profile.image_192} />
        <div className ='dev_name'> {devName} </div>
        <div className='dev_info'>
          <div className ='dev_title' > {devTitle} </div>
          <div className='dev_phone'>
              {@props.item.profile.phone}
          </div>
          <div className='dev_hangouts'>
            coming soon...
          </div>
        </div>
      </div>
    </li>

module.exports = Profile
