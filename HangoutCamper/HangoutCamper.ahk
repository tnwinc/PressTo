/*
Author: Mark Crow
Date: 1/7/14

This script starts the scrum board in fullscreen mode then waits for a Hangouts message.
Once found, it will activate the window, maximize and accept calls.

Before you use this, it currently requires that everyone message and be accepted by the bot.
*/

SetTitleMatchMode 2
DetectHiddenWindows, On
DetectHiddenText, On

; This script is "hardcoded" for 1920x1080 resolution
; TODO: if no image search workaround, calculate the button locs from resolution
screenWidth  = %A_ScreenWidth%
screenHeight = %A_ScreenHeight%
currentlyInACall := false

; Startup Chrome for status board
RunWait "C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" http://labs.tnwinc.com/grc/board.html --enable-easy-off-store-extension-install --disable-web-security
Sleep, 3000
Send {F11}


Standby:
; Detect child hangout windows to get things started or the Google+ window to continue
GroupAdd, winOfInterest, ahk_class Chrome_WidgetWin_1
GroupAdd, winOfInterest, Google+
WinWait, ahk_group winOfInterest,,,Chrome,

;WinWait, ahk_class Chrome_WidgetWin_1,,,Google Chrome, Hangouts
WinActivate, ahk_class Chrome_WidgetWin_1,,Google Chrome, Hangouts


JoinCall:
; Join first video call invite
ImageSearch, FoundX, FoundY, 0, 0, 1000, 1000, *50 cameraButton.jpg
if (ErrorLevel = 0 and currentlyInACall = false) {
  WinActivate, ahk_class Chrome_WidgetWin_1,,Google Chrome, Hangouts
  Click %FoundX%, %FoundY%
  currentlyInACall := true
  Sleep, 3000
  WinClose, ahk_class Chrome_WidgetWin_1,,,Google Chrome, Hangouts ; close the msg window
  WinMaximize, Google+
}


MergeCalls:
; Already in a video call and a new video reqest is received
ImageSearch, FoundX, FoundY, 0, 0, 1000, 1000, *50 cameraButton.jpg
if (ErrorLevel = 0 and currentlyInACall = true) {
  ; Respond to them and close their message
  Sleep, 3000
  WinGetTitle, msgFromText

  ; TODO: Check if the name is valid or one of the system messages (such as a msg from a new contact)
  WinActivate, msgFromText
  Send, Hi %msgFromText%. A video call is already running, I will invite you in a sec.{enter}
  Send, Choose "End the other call" if you get the prompt. Don't respond to this message. {enter}
  WinClose, ahk_class Chrome_WidgetWin_1,,,Google Chrome, Hangouts ; close the msg window

  ; Invite them to call primary call (image searches are not working for some reason)
  WinActivate, Google+
  Click 850, 66 ; Invite people button
  Sleep, 2000
  Click 850, 66 ; Invite people button - 2 clicks to make sure the controls are faded in
  Sleep, 2000
  Click 930, 506 ; Add names box
  Sleep, 500
  SetKeyDelay, 200 ; This contact lookup form can be sensitive to fast text
  Send, %msgFromText%
  Sleep, 2000
  Send, {enter}{tab}
  Sleep, 2000
  Click 779, 615 ; Invite button
}


CloseCall:
; Close video windows that are empty
if (currentlyInACall = true) {
  dummyCount = 0
  WinGetText, videoChatText, Google+

  Loop, parse, videoChatText, `n, `r ; The text will be hidden
  {
    if (A_LoopField = "DummyWindowForActivation") 
      dummyCount++
  }
  
  if (dummyCount <= 2) {
    WinClose, Google+ ; close the video chat window
    currentlyInACall := false
    WinClose, ahk_class Chrome_WidgetWin_1,,,Google Chrome, Hangouts ; close the msg window
  }
}

Sleep, 5000
Goto, Standby
