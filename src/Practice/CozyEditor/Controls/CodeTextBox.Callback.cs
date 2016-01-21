using CozyEditor.EventArguments;
using CozyEditor.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CozyEditor.Controls
{
    public partial class CodeTextBox
    {
        protected virtual void OnTextChanged(object sender, ValueChangedArgs<string> e)
        {
            ReAnalysis();

            TextChangedEventHandler?.Invoke(sender, e);
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as CodeTextBox;
            if (obj != null)
            {
                obj.OnTextChanged(obj, new ValueChangedArgs<string>() { OldValue = (string)e.OldValue, NewValue = (string)e.NewValue });
            }
        }

        protected virtual void OnCurrPlaceChanged(object sender, ValueChangedArgs<Place> e)
        {
            CurrPlaceChangedEventHandler?.Invoke(sender, e);
        }

        private static void OnCurrPlaceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as CodeTextBox;
            if (obj != null)
            {
                obj.OnCurrPlaceChanged(obj, new ValueChangedArgs<Place>() { OldValue = (Place)e.OldValue, NewValue = (Place)e.NewValue });
            }
        }

        protected virtual void OnLineHeightChanged(object sender, ValueChangedArgs<int> e)
        {
            LineHeightChangedEventHandler?.Invoke(sender, e);
        }

        private static void OnLineHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as CodeTextBox;
            if (obj != null)
            {
                obj.OnLineHeightChanged(obj, new ValueChangedArgs<int>() { OldValue = (int)e.OldValue, NewValue = (int)e.NewValue });
            }
        }
    }
}
