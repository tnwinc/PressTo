
namespace Hardware
{
  public class Button : IButton
  {
    public bool Pressed { get; set; }

    public void PopulateFromSerialReader(string value) {
      Pressed = !Pressed;
    }

    public const string GREEN_BUTTON = "BUTTON_GREEN";
    public const string RED_BUTTON = "BUTTON_RED";
  }

  public interface IButton
  {
    bool Pressed { get; }
  }
}
