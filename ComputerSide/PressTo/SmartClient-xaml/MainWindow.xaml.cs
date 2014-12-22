using System;
using System.Configuration;
using System.Threading;
using System.Windows;
using Core;
using Core.Extensions;
using Hardware;
using PressTo.Controls;

namespace PressTo
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow
  {
    public Clicker Selector;
    private SelectAction DisplayAction;

    private System.Threading.Timer HideTimer;
    private long HideFormTimeout { get { return Properties.Settings.Default.HideFormTimeout; } }

    private SwitchCalibration CalibrationBacker;
    private SwitchCalibration Calibration { get { return CalibrationBacker ?? Properties.Settings.Default.SwitchCalibration ?? new SwitchCalibration(); } set { CalibrationBacker = value; } }
    private int RotaryPosition { get { return Calibration.GetSwitchPositionForValue(Selector.RotarySwitch.Value, Properties.Settings.Default.switchTolerance).Id; } }

    public MainWindow()
    {
      //Properties.Settings.Default.Reset();
      InitializeComponent();
      Selector = new Clicker();
      if (Calibration == null || Calibration.Count == 0)
      {
        var calibrate = new Calibrate {Selector = Selector};
        calibrate.CalibrationCompleted += CompleteCalibrationRoutine;
        Content = calibrate;
        calibrate.StartCalibrationRoutine();
      } else {
        CompleteCalibrationRoutine(this, new EventArgs());
      }
    }

    private SwitchCalibrationItem Current
    {
      get { return Calibration[RotaryPosition - 1]; }
    }

    private SwitchCalibrationItem Next
    {
      get
      {
        if (RotaryPosition >= Calibration.Count || RotaryPosition < 0) return null;
        return Calibration[RotaryPosition];
      }
    }

    private SwitchCalibrationItem Previous
    {
      get
      {
        if (RotaryPosition - 2 > Calibration.Count || RotaryPosition - 2 < 0) return null;
        return Calibration[RotaryPosition - 2];
      }
    }

    private void RegisterInputs()
    {
      Selector.RotaryChanged += SelectorOnRotaryChanged;
      Selector.GreenButtonChanged += SelectorOnGreenButtonChanged;
      Selector.RedButtonChanged += SelectorOnRedButtonChanged;
    }
    private void UnregisterInputs()
    {
      Selector.RotaryChanged -= SelectorOnRotaryChanged;
      Selector.GreenButtonChanged -= SelectorOnGreenButtonChanged;
      Selector.RedButtonChanged -= SelectorOnRedButtonChanged;
    }

    private void CompleteCalibrationRoutine(object sender, EventArgs eventArgs)
    {
      RegisterInputs();
      Calibration = Properties.Settings.Default.SwitchCalibration;
      Dispatcher.Invoke(() => Content = DisplayAction = new SelectAction());
      Dispatcher.Invoke(ShowForm);
    }

    private void SelectorOnGreenButtonChanged(object sender, ButtonChangedEventArgs buttonChangedEventArgs)
    {
      if (Visibility == Visibility.Visible)
      {
        Dispatcher.Invoke(HideForm);
        if (Calibration[RotaryPosition - 1].Image == null)
        {
          Dispatcher.Invoke(ConfigureSwitchPosition);
        }
        else
        {
          MessageBox.Show("This is what spawns a new action for position {0}.".FormatWith(RotaryPosition));
        }
      }
      else
      {
        Dispatcher.Invoke(ShowForm);
      }
    }

    private void ConfigureSwitchPosition()
    {
      UnregisterInputs();
      var configAction = new ConfigureAction(Current);
      Content = configAction;
      configAction.ActionConfigured += (sender, args) => {
        ConfigureActionCleanup(sender);
        Properties.Settings.Default.Save();
        Calibration = Properties.Settings.Default.SwitchCalibration;
      };
      configAction.ConfigurationCancelled += (sender, args) => ConfigureActionCleanup(sender);

      Visibility = Visibility.Visible;
      HideTimer.Change(Timeout.Infinite, Timeout.Infinite);
    }

    private void ConfigureActionCleanup(object sender) {
      Content = DisplayAction;
      RegisterInputs();
      Dispatcher.Invoke(HideForm);
    }

    private void SelectorOnRedButtonChanged(object sender, ButtonChangedEventArgs buttonChangedEventArgs)
    {
      if (Visibility == Visibility.Visible)
      {
        Dispatcher.Invoke(HideForm);
        MessageBox.Show("This should probably terminate the process for position {0}".FormatWith(RotaryPosition));
      }
      else
      {
        Dispatcher.Invoke(ShowForm);
      }
    }

    private void SelectorOnRotaryChanged(object sender, RotaryChangedEventArgs rotaryChangedEventArgs)
    {
      if (Calibration.Count > 0)
      {
        Dispatcher.Invoke(() => DisplayAction.txtCurrent.Text = Current != null ? Current.Description ?? string.Empty : string.Empty);
        Dispatcher.Invoke(() => DisplayAction.txtPrevious.Text = Previous != null ? Previous.Name ?? string.Empty : string.Empty);
        Dispatcher.Invoke(() => DisplayAction.txtNext.Text = Next != null ? Next.Name ?? string.Empty : string.Empty);
      }

      Dispatcher.Invoke(ShowForm);
    }

    private void ShowForm()
    {
      ResetHideTimer();
      Visibility = Visibility.Visible;
    }

    private void HideForm()
    {
      Visibility = Visibility.Hidden;
    }

    private void ResetHideTimer()
    {
      if (HideTimer == null)
      {
        HideTimer = new Timer(state => Dispatcher.Invoke(HideForm), null, HideFormTimeout, -1);
      }
      else
      {
        HideTimer.Change(HideFormTimeout, -1);
      }
    }

  }
}
