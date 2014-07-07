/*
Author: Mark Crow
Date: 7/4/14

Description:
This script starts a new Hangout and invites someone.
*/

; command line parameters
Loop, %0%  ; For each parameter:
  inviteEmail .= %A_Index% "`r`n"

SetTitleMatchMode 2
DetectHiddenWindows, On
DetectHiddenText, On

screenWidth  = %A_ScreenWidth%
screenHeight = %A_ScreenHeight%

; Startup Chrome and hangout call
;RunWait "C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" http://hangouts.google.com/start
RunWait "http://hangouts.google.com/start" ; use default browser... better be chrome
Sleep, 2000
;Send {F11}
Sleep, 3000

; Invite person to call
;WinActivate, Google+
ImageSearch, FoundX, FoundY, 0, 0, screenWidth, screenHeight, *50 invitePlaceHolder.bmp
if ErrorLevel = 0 
{ 
  ; Invite people found
  Click %FoundX%, %FoundY%
  Sleep, 500

  SetKeyDelay, 50 ; This contact lookup form can be sensitive to fast text
  Send, %inviteEmail% {enter}
  Sleep, 1000

  Send, {tab} ; leave the box to make sure input is registered and button unblocked
  Sleep, 500

  ImageSearch, FoundX, FoundY, 0, 0, screenWidth, screenHeight, *50 inviteButton.bmp
  if ErrorLevel = 0 
  {
    ; Invite button found
    Click %FoundX%, %FoundY%
  }
  else 
  {
    MsgBox Debug: The invite button wasn't found
  }
}
else 
{
  MsgBox Debug: The invite people box wasn't found
}

Exit
