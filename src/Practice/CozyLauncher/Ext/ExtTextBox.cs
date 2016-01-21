using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CozyLauncher.Ext
{
    public class ExtTextBox : TextBox
    {
        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
        }
    }
}
