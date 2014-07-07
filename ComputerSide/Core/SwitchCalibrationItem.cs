
using System.Drawing;
using System.Xml.Serialization;
using Core.Extensions;

namespace Core
{
  public class SwitchCalibrationItem
  {
    public int Id { get; set; }
    public int Value { get; set; }
    public string Name { get; set; }
    public string ImageFilename{ get; set; }

    public SwitchCalibrationItem() {
      Name = string.Empty;
      Description = string.Empty;
    }

    [XmlIgnore]
    private Image ImageBacker;
    [XmlIgnore]
    public Image Image {
      get {
        if (ImageFilename.IsNullOrWhitespace() && ImageBacker == null) return null;
        return ImageBacker ?? new Bitmap(ImageFilename);
      }
      set { ImageBacker = value; }
    }

    public string Description { get; set; }
    //public Action Launch { get; set; }

    public string ProcessFilename { get; set; }
    public string ProcessParameters { get; set; }
    public Actions SelectedAction { get; set; }
    public enum Actions
    {
      Process
    }
  }
}
