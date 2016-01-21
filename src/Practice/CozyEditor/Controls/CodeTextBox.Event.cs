using CozyEditor.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyEditor.Controls
{
    public partial class CodeTextBox
    {
        public event EventHandler<ValueChangedArgs<string>> TextChangedEventHandler;

        public event EventHandler<ValueChangedArgs<Text.Place>> CurrPlaceChangedEventHandler;

        public event EventHandler<ValueChangedArgs<int>> LineHeightChangedEventHandler;
    }
}
