using System;

namespace Hardware
{
  public class ButtonChangedEventArgs : EventArgs
  {
    public ButtonType Button { get; set; }
    public bool Pressed { get; set; }

    public ButtonChangedEventArgs(ButtonType button, bool pressed) {
      Button = button;
      Pressed = pressed;
    }

    public enum ButtonType
    {
      Red,
      Green
    }
  }
}
