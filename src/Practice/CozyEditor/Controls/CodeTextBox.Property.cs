using CozyEditor.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CozyEditor.Text;

namespace CozyEditor.Controls
{
    public partial class CodeTextBox
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(CodeTextBox),
           new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnTextChanged)));

        public static readonly DependencyProperty CurrPlaceProperty = DependencyProperty.Register("CurrPlace", typeof(Place), typeof(CodeTextBox),
           new FrameworkPropertyMetadata(Place.Zero, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnCurrPlaceChanged)));

        public static readonly DependencyProperty LineHeigthProperty = DependencyProperty.Register("LineHeigth", typeof(int), typeof(CodeTextBox),
          new FrameworkPropertyMetadata(14, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnLineHeightChanged)));

        public Place CurrPlace
        {
            get
            {
                return (Place)this.GetValue(CurrPlaceProperty);
            }
            set
            {
                this.SetValue(CurrPlaceProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)this.GetValue(TextProperty);
            }
            set
            {
                this.SetValue(TextProperty, value);
            }
        }

        public int LineHeigth
        {
            get
            {
                return (int)this.GetValue(LineHeigthProperty);
            }
            set
            {
                this.SetValue(LineHeigthProperty, value);
            }
        }

        public LineCollection Lines { get; set; } = new LineCollection();
    }
}
