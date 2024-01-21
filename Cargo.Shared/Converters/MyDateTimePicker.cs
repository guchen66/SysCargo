using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Shared.Converters
{
    public class MyDateTimePicker : DateTimePicker
    {
       /* protected override string GetValueForTextBox()
        {
            return SelectedDateTime?.ToString("yyyy-MM-dd HH:mm:ss")!;
        }*/


        /*  protected override string GetValueForTextBox()
          {
              return string.Empty;
          }*/

        protected override string GetValueForTextBox()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
