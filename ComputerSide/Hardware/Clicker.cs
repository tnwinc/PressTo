using System;
using System.IO.Ports;
using Core.Extensions;

namespace Hardware
{
  public class Clicker
  {
    private SerialPort SerialPort { get; set; }

    private readonly Button RedButtonBacker= new Button();
    private readonly Button GreenButtonBacker = new Button();
    private readonly Rotary RotarySwitchBacker = new Rotary();

    public delegate void ButtonChangedHandler(object sender, ButtonChangedEventArgs e);
    public delegate void RotaryChangedHandler(object sender, RotaryChangedEventArgs e);

    public event RotaryChangedHandler RotaryChanged;
    public event ButtonChangedHandler RedButtonChanged;
    public event ButtonChangedHandler GreenButtonChanged;

    public IButton RedButton { get { return RedButtonBacker; } }
    public IButton GreenButton { get { return GreenButtonBacker; } }
    public IRotary RotarySwitch { get { return RotarySwitchBacker; } }

    public Clicker() {
      PrintSerialPortsAndConnect();
    }

    public void GetRotaryPosition()
    {
      SerialPort.WriteLine("get_position");
    }

    private void PrintSerialPortsAndConnect() {
      var ports = SerialPort.GetPortNames();
      Console.WriteLine("The following serial ports were found:");

      foreach (var port in ports) {
        Console.WriteLine(port);
      }

      if (ports.Length > 0) {
        SerialPort = new SerialPort(ports[0], 9600, Parity.None, 8, StopBits.One);
        SerialPort.Open();
        SerialPort.DataReceived += OnDataReceived;
        SerialPort.DtrEnable = true;
        Console.WriteLine("Using {0}...".FormatWith(ports[0]));
      } else {
        Console.WriteLine("Unable to locate any serial ports in use.");
      }
    }

    private void OnDataReceived(object sender, SerialDataReceivedEventArgs e) {
      if (!SerialPort.IsOpen) return;
      string readData;
      do {
        readData = SerialPort.ReadLine();
      }
      while (readData.StartsWith(Rotary.ROTARY_SWITCH + "1023"));

      if (readData.StartsWith(Rotary.ROTARY_SWITCH)) {
        RotarySwitchBacker.PopulateFromSerialReader(readData.Substring(Rotary.ROTARY_SWITCH.Length));
        if (RotaryChanged != null) RotaryChanged(sender, new RotaryChangedEventArgs(RotarySwitchBacker.Value));
      } else
        if (readData.StartsWith(Button.GREEN_BUTTON)) {
          GreenButtonBacker.PopulateFromSerialReader(readData.Substring(Button.GREEN_BUTTON.Length));
          if (GreenButtonChanged != null) GreenButtonChanged(sender, new ButtonChangedEventArgs(ButtonChangedEventArgs.ButtonType.Green, GreenButtonBacker.Pressed));
        } else
          if (readData.StartsWith(Button.RED_BUTTON)) {
            RedButtonBacker.PopulateFromSerialReader(readData.Substring(Button.RED_BUTTON.Length));
            if (RedButtonChanged != null) RedButtonChanged(sender, new ButtonChangedEventArgs(ButtonChangedEventArgs.ButtonType.Red, RedButtonBacker.Pressed));
          } else {
            throw new InvalidOperationException("Unknown data received from hardware: {0}".FormatWith(readData));
          }
    }
  }
}
