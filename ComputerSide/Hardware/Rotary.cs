using System;
using Core.Extensions;

namespace Hardware
{
  public class Rotary : IRotary
  {
    public int Value { get; private set; }

    public void PopulateFromSerialReader(string value) {
      int result;
      if (Int32.TryParse(value.Trim(), out result)) {
        Value = result;
      } else {
        throw new InvalidOperationException("Could not parse a value of the rotary switch: {0}.".FormatWith(value));
      }
    }

    public const string ROTARY_SWITCH = "SWITCH_";
  }

  public interface IRotary
  {
    int Value { get; }
  }
}
