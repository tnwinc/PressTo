using System.Diagnostics;
using System.Windows.Forms;
using Core;
using Core.Extensions;
using Hardware;

namespace Switcher
{
  public partial class ActionSelector : Form
  {
    public Clicker Selector;
    //private SelectAction DisplayAction;

    private System.Threading.Timer HideTimer;
    private const int HideFormTimeout = 5000;

    private SwitchCalibration CalibrationBacker;
    //private ElementHost Host;
    private SwitchCalibration Calibration { get { return CalibrationBacker ?? Properties.Settings.Default.SwitchCalibration ?? new SwitchCalibration(); } set { CalibrationBacker = value; } }
    private int RotaryPosition { get { return Calibration.GetSwitchPositionForValue(Selector.RotarySwitch.Value, Properties.Settings.Default.switchTolerance).Id; } }

    private SwitchCalibrationItem Current {
      get { return Calibration[RotaryPosition - 1]; }
    }

    private SwitchCalibrationItem Next {
      get {
        if (RotaryPosition >= Calibration.Count || RotaryPosition < 0) return null;
        return Calibration[RotaryPosition];
      }
    }

    private SwitchCalibrationItem Previous {
      get {
        if (RotaryPosition - 2 > Calibration.Count || RotaryPosition - 2 < 0) return null;
        return Calibration[RotaryPosition - 2];
      }
    }

    public ActionSelector()
    {
      //Properties.Settings.Default.Reset();
      InitializeComponent();
      SetStyle(ControlStyles.SupportsTransparentBackColor, true);

      Selector = new Clicker();
      if (!Calibration.IsInitialized)
      {
        RunSwitchCalibrationRoutine();
      }
      //DisplayAction = new SelectAction();
      //Host = new ElementHost {Dock = DockStyle.Fill, Child = DisplayAction, BackColor = Color.Transparent, BackColorTransparent = true};
      //Controls.Add(Host);
      RegisterInputs();
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

    private void RunSwitchCalibrationRoutine()
    { 
      using (var calibrationRoutine = new CalibrationRoutine())
      {
        calibrationRoutine.ShowDialog(this);
      }
      Calibration = Properties.Settings.Default.SwitchCalibration;
      this.SafeInvoke(HideForm);
    }

    private void SelectorOnGreenButtonChanged(object sender, ButtonChangedEventArgs buttonChangedEventArgs) {
      if (Visible) {
        this.SafeInvoke(HideForm);
        if (Calibration[RotaryPosition - 1].Image == null) {
          this.SafeInvoke(ConfigureSwitchPosition);
        }
        else if (Current.SelectedAction == SwitchCalibrationItem.Actions.Process && !Current.ProcessFilename.IsNullOrEmpty()) {
          Process.Start(Current.ProcessFilename, Current.ProcessParameters);

        } else {
          MessageBox.Show("This is what spawns a new action for position {0}...".FormatWith(RotaryPosition));
        }
      }
      else {
        this.SafeInvoke(ShowForm);
      }
    }

    private void ConfigureSwitchPosition() {
      using (var config = new ConfigureAction(Calibration[RotaryPosition - 1])) {
        if (config.ShowDialog(this) == DialogResult.OK) {
          Properties.Settings.Default.SwitchCalibration = Calibration;
          Properties.Settings.Default.Save();
        }
      }
      Calibration = Properties.Settings.Default.SwitchCalibration;
    }

    private void SelectorOnRedButtonChanged(object sender, ButtonChangedEventArgs buttonChangedEventArgs)
    {
      if (Visible) {
        this.SafeInvoke(HideForm);
        MessageBox.Show("This should probably terminate the previously spawned action for position {0} if it is running...".FormatWith(RotaryPosition));
      }
      else this.SafeInvoke(ShowForm);
    }

    private void SelectorOnRotaryChanged(object sender, RotaryChangedEventArgs rotaryChangedEventArgs) {
      if (Calibration.Count > 0) {
        this.SafeInvoke(() => {
          lblCurrent.Text = Current != null ? Current.Description ?? string.Empty : string.Empty;
          imgCurrent.ImageLocation = Current == null ? null : Current.ImageFilename;
        });
        this.SafeInvoke(() => {
          lblPrevious.Text = Previous != null ? Previous.Name ?? string.Empty : string.Empty;
          imgPrevious.ImageLocation = Previous == null ? null : Previous.ImageFilename;
        });
        this.SafeInvoke(() => {
          lblNext.Text = Next != null ? Next.Name ?? string.Empty : string.Empty;
          imgNext.ImageLocation = Next == null ? null : Next.ImageFilename;
        });
      }

      this.SafeInvoke(ShowForm);
    }

    private void ShowForm()
    {
      ResetHideTimer();
      Visible = true;
    }

    private void HideForm()
    {
      Visible = false;
    }

    private void ResetHideTimer()
    {
      if (HideTimer == null)
      {
        HideTimer = new System.Threading.Timer(state => this.SafeInvoke(HideForm), null, HideFormTimeout, -1);
      }
      else
      {
        HideTimer.Change(HideFormTimeout, -1);
      }
    }

    private void ActionSelector_FormClosing(object sender, FormClosingEventArgs e) {
      Properties.Settings.Default.Save();
    }

    private void ActionSelector_Load(object sender, System.EventArgs e)
    {
    }
  }
}
