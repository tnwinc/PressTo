
using System;
using Hardware;

namespace ConsoleSerialPortReader
{
  public class Program
  {

    static void Main() {
      var clicker = new Clicker();
      clicker.RotaryChanged += (sender, args) => Console.WriteLine(Rotary.ROTARY_SWITCH + args.Value);
      clicker.GreenButtonChanged += (sender, args) => Console.WriteLine(Button.GREEN_BUTTON);
      clicker.RedButtonChanged += (sender, args) => Console.WriteLine(Button.RED_BUTTON);
      while (true) { }
    }

  }
}
