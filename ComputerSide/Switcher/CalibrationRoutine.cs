using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Core;
using Core.Extensions;
using Hardware;

namespace Switcher
{
  public partial class CalibrationRoutine : Form
  {
    private Clicker Selector;
    public CalibrationRoutine()
    {
      InitializeComponent();
      calibration = new SwitchCalibration();
      SwitchTolerance = Properties.Settings.Default.switchTolerance;
    }

    private void CalibrationRoutine_Load(object sender, EventArgs e)
    {
      var owner = (Owner as ActionSelector);
      if (owner == null)
        throw new InvalidOperationException("Cannot call the calibration routine except from ActionSelector.");

      Selector = owner.Selector;
      Selector = owner.Selector ?? new Clicker();
      Selector.RotaryChanged += SelectorOnRotaryChanged;
      Selector.GetRotaryPosition();
    }

    private void SelectorOnRotaryChanged(object sender, RotaryChangedEventArgs rotaryChangedEventArgs)
    {
      if (calibration.All(item => Math.Abs(item.Value - rotaryChangedEventArgs.Value) > SwitchTolerance))
      {
        calibration.Add(new SwitchCalibrationItem{Value = rotaryChangedEventArgs.Value});
        if (calibration.Count == 12) {
          CompleteCalibrationStep1();
        }
        else {
          this.SafeInvoke(() => UpdateSwitchPositionCount(calibration.Count));
        }
      }
    }

    private void CompleteCalibrationStep1()
    {
      calibration.Sort((item, calibrationItem) =>
        {
          if (item.Value > calibrationItem.Value) return 1;
          if (item.Value < calibrationItem.Value) return -1;
          return 0;
        });
      for (var i = 0; i < calibration.Count; i++)
      {
        calibration[i].Id = i + 1;
      }
      Properties.Settings.Default.SwitchCalibration = calibration;
      Properties.Settings.Default.Save();
      Selector.RotaryChanged -= SelectorOnRotaryChanged;
      this.SafeInvoke(Close);
    }

    private void UpdateSwitchPositionCount(int value)
    {
        switchPositionCount.Text = value.ToString(CultureInfo.InvariantCulture);
    }

    private void btnNext_Click(object sender, EventArgs e) {
      CompleteCalibrationStep1();
    }
  }
}
