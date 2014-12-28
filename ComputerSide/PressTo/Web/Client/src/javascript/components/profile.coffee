React = require 'react'

Profile = React.createClass
  render: ->
    tileImage=
      backgroundImage: "url(#{@props.item.profile.image_192})"
    devName = if  @props.item.profile.real_name_normalized then @props.item.profile.real_name_normalized  else 'Oh Noes! I haven\'t updated my slack profile '
    devTitle = if  @props.item.profile.title then @props.item.profile.title  else ''
    <li>
      <ul className='profile' style={tileImage}>
        <li>
          {devName}
        </li>
        <li>
          {devTitle}
        </li>
      </ul>
    </li>

module.exports = Profile
