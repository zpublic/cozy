using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;

namespace MMS.UI.Default
{
    public class Edit : System.Windows.Controls.Control
    {
        private ScrollViewer mPanel;
        private RichTextBox mTextBox;
        private bool mIsChange = false;

        public delegate void TextChangedDelegate(ref FlowDocument doc, Edit edit);
        public event TextChangedDelegate TextChanged;
        public delegate void TextLineChangedDelegate(int line, string context);
        public event TextLineChangedDelegate TextLineChanged;
        public delegate void OnEditLineDelegate(Run line,Edit edit);
        public event OnEditLineDelegate OnEditLine;

        public Edit()
        {
            this.Style = (Style)Application.Current.Resources["EditStyle"];
        }

        void mPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Keyboard.Focus(this.mTextBox);
        }

        public override void OnApplyTemplate()
        {
            this.mPanel = (ScrollViewer)this.GetTemplateChild("panel");
            this.mTextBox = (RichTextBox)this.GetTemplateChild("textBox");
            this.mPanel.MouseDown += mPanel_MouseDown;
            this.mTextBox.TextChanged += mTextBox_TextChanged;
            base.OnApplyTemplate();
        }

        void mTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.mIsChange)
            {
                this.mIsChange = true;
                FlowDocument doc = this.mTextBox.Document;
                Run line = (Run)Keyboard.FocusedElement;
                //this.TextChanged(ref doc, this);
                this.OnEditLine(line, this);
                Task.Run(() => { this.SetDocument(doc); });
                this.mIsChange = false;
            }
        }

        public void Highlight(int line, int start, int length, GrammarType type)
        {
            //this.mTextBox
            //richTextBox1.SelectionColor = Color.Red;
        }

        private void SetDocument(FlowDocument doc)
        {
            //this.mTextBox.Document = doc;
        }

        private Color GetColorByGrammerType(GrammarType type)
        {
            switch (type)
            {
                case GrammarType.Class:
                    {
                        return Colors.Red;
                    }
                case GrammarType.Function:
                    {
                        return Colors.Blue;
                    }
                case GrammarType.Keyword:
                    {
                        return Colors.BurlyWood;
                    }
            }
            return Colors.Black;
        }
    }
}
