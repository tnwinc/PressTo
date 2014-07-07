using System;
using System.Windows.Forms;

namespace Core.Extensions
{
  public static class FormExtensions
  {
    public static void SafeInvoke(this Form myForm, Action action) {
      myForm.Invoke(new MethodInvoker(action));
    }
  }
}
