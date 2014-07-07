using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Core;
using Core.Extensions;

namespace Switcher
{
  public partial class ConfigureAction : Form
  {
    private SwitchCalibrationItem Item { get; set; }
    public ConfigureAction(SwitchCalibrationItem item) {
      InitializeComponent();
      Item = item;

    }
    private void pictureBox1_Click(object sender, EventArgs e) {
      if (imageFileDialog.ShowDialog() == DialogResult.OK) {
        var img = FixedSize(new Bitmap(imageFileDialog.FileName), 150, 150);
        var ext = Path.GetExtension(imageFileDialog.FileName);

        var cm = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
        Item.ImageFilename = Path.Combine(Path.GetDirectoryName(cm.FilePath), "{0}{1}".FormatWith(Item.Id, ext));

        img.Save(Item.ImageFilename);        
        Item.Image = img;
        
        pictureBox1.Image = img;
      }
    }

    static Image FixedSize(Image imgPhoto, int Width, int Height) {
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

    private void btnOk_Click(object sender, EventArgs e)
    {
      Item.Name = txtName.Text;
      Item.Description = txtDescription.Text;
      Item.ProcessFilename = txtProcess.Text;
      Item.ProcessParameters = txtProcessParams.Text;
      DialogResult = DialogResult.OK;
      Close();
    }

    private void btnCancel_Click(object sender, EventArgs e) {
      Item.ImageFilename = string.Empty;
      Item.Image = null;
      Item.ProcessFilename = string.Empty;
      Item.ProcessParameters = string.Empty;

      DialogResult = DialogResult.Cancel;
      Close();
    }

    private void ConfigureAction_Load(object sender, EventArgs e)
    {
      var owner = (Owner as ActionSelector);
      if (owner == null)
        throw new InvalidOperationException("Cannot call the configure action routine except from ActionSelector.");
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
      if (comboBox1.SelectedIndex == 0 || txtProcess.Text.IsNullOrEmpty()) {
        GetExeProcess();
      }
    }

    private void GetExeProcess() {
      if (openProcessDialog.ShowDialog() == DialogResult.OK) {
        Item.ProcessFilename = openProcessDialog.FileName;
        txtProcess.Text = openProcessDialog.FileName;
        Item.SelectedAction = SwitchCalibrationItem.Actions.Process;
      }
    }

    private void txtProcess_Click(object sender, EventArgs e) {
      GetExeProcess();
    }

    private void btnSelectProcess_Click(object sender, EventArgs e) {
      GetExeProcess();
    }
  }
}
