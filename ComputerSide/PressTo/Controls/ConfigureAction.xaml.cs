using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Core;
using Core.Extensions;
using Microsoft.Win32;
using Color = System.Drawing.Color;
using Image = System.Drawing.Image;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Rectangle = System.Drawing.Rectangle;


namespace PressTo.Controls
{
  /// <summary>
  /// Interaction logic for ConfigureAction.xaml
  /// </summary>
  public partial class ConfigureAction
  {
    private SwitchCalibrationItem Item { get; set; }
    public event EventHandler ActionConfigured;
    public event EventHandler ConfigurationCancelled;

    public ConfigureAction(SwitchCalibrationItem item)
    {
      InitializeComponent();
      Item = item;
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e) {
      if (ConfigurationCancelled != null) ConfigurationCancelled(this, new EventArgs());
    }

    private void btnOk_Click(object sender, RoutedEventArgs e) {
      if (ActionConfigured != null) ActionConfigured(this, new EventArgs());
    }

    private void Image_MouseDown(object sender, MouseButtonEventArgs e) {
      var file = new OpenFileDialog {Filter = "(*.bmp, *.jpg, *.png, *.gif)|*.bmp;*.jpg;*.png;*.gif", Title = "Choose Image For Action..."};
      var result = file.ShowDialog();
      if (result == true) {
        pictureBox1.Source = null;
        var img = FixedSize(new Bitmap(file.FileName), 150, 150);
        var ext = Path.GetExtension(file.FileName);

        var cm = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
        Item.ImageFilename = Path.Combine(Path.GetDirectoryName(cm.FilePath), "{0}{1}".FormatWith(Item.Id, ext));

        img.Save(Item.ImageFilename);
        Item.Image = img;

        pictureBox1.Source = new BitmapImage(new Uri(Item.ImageFilename));
      }
    }

    static Bitmap FixedSize(Image imgPhoto, int Width, int Height) {
      var sourceWidth = imgPhoto.Width;
      var sourceHeight = imgPhoto.Height;
      var sourceX = 0;
      var sourceY = 0;
      var destX = 0;
      var destY = 0;

      float nPercent = 0;
      float nPercentW = 0;
      float nPercentH = 0;

      nPercentW = ((float)Width / (float)sourceWidth);
      nPercentH = ((float)Height / (float)sourceHeight);
      if (nPercentH < nPercentW) {
        nPercent = nPercentH;
        destX = Convert.ToInt16((Width -
                      (sourceWidth * nPercent)) / 2);
      } else {
        nPercent = nPercentW;
        destY = Convert.ToInt16((Height -
                      (sourceHeight * nPercent)) / 2);
      }

      var destWidth = (int)(sourceWidth * nPercent);
      var destHeight = (int)(sourceHeight * nPercent);

      var bmPhoto = new Bitmap(Width, Height,
                        PixelFormat.Format24bppRgb);
      bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                       imgPhoto.VerticalResolution);

      var grPhoto = Graphics.FromImage(bmPhoto);
      grPhoto.Clear(Color.Transparent);
      grPhoto.InterpolationMode =
              InterpolationMode.HighQualityBicubic;

      grPhoto.DrawImage(imgPhoto,
          new Rectangle(destX, destY, destWidth, destHeight),
          new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
          GraphicsUnit.Pixel);

      grPhoto.Dispose();
      return bmPhoto;
    }
  }
}
