using System;
using System.Globalization;
using System.Linq;
using Core;
using Hardware;

namespace PressTo.Controls
{
  /// <summary>
  /// Interaction logic for Calibrate.xaml
  /// </summary>
  public partial class Calibrate
  {
    private SwitchCalibration calibration { get; set; }

    public event EventHandler CalibrationCompleted;

    public Clicker Selector { get; set; }

    private int SwitchTolerance{ get { return Properties.Settings.Default.switchTolerance;}}

    public Calibrate()
    {
      calibration= new SwitchCalibration();
      InitializeComponent();
    }

    private void RegisterInputs()
    {
      Selector.RotaryChanged += SelectorOnRotaryChanged;
    }

    private void SelectorOnRotaryChanged(object sender, RotaryChangedEventArgs rotaryChangedEventArgs)
    {
      if (calibration.All(item => Math.Abs(item.Value - rotaryChangedEventArgs.Value) > SwitchTolerance))
      {
        calibration.Add(new SwitchCalibrationItem { Value = rotaryChangedEventArgs.Value });
        if (calibration.Count == 12)
        {
          CompleteCalibrationStep1();
        }
        else
        {
          Dispatcher.Invoke(() => UpdateSwitchPositionCount(calibration.Count));
        }
      }
    }
    private void UpdateSwitchPositionCount(int value)
    {
      switchPositionCount.Content = value.ToString(CultureInfo.InvariantCulture);
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
      OnCalibrationCompleted();
    }
    private void OnCalibrationCompleted()
    {
      if (CalibrationCompleted != null) CalibrationCompleted(this, new EventArgs());
    }
    private void btnNext_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      CompleteCalibrationStep1();
    }

    public void StartCalibrationRoutine()
    {
      RegisterInputs();
      Selector.GetRotaryPosition();
    }
  }
}
