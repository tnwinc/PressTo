using System;

namespace Hardware
{
  public class RotaryChangedEventArgs : EventArgs
  {
    public RotaryChangedEventArgs(int value) {
      Value = value;
    }

    public int Value { get; set; }
  }
}