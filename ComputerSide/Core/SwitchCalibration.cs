using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
  public class SwitchCalibration : List<SwitchCalibrationItem>
  {
    public bool IsInitialized { get { return Count > 0; } }

    public SwitchCalibrationItem GetSwitchPositionForValue(int value, int tolerance) {
      return this.SingleOrDefault(v => Math.Abs(v.Value -value) <= tolerance);
    }
  }
}
