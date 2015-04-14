using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Folding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CozySql.Exe.UserControls
{
    /// <summary>
    /// Interaction logic for SqlInput.xaml
    /// </summary>
    public partial class SqlInput : UserControl
    {
        private FoldingManager foldingManager = null;
        private AbstractFoldingStrategy foldingStrategy = null;

        public SqlInput()
        {
            InitializeComponent();
            textEditor.Options.ShowTabs = true;
            textEditor.Options.ShowSpaces = true;
            textEditor.Options.ConvertTabsToSpaces = true;
            textEditor.Options.HighlightCurrentLine = true;
            textEditor.ShowLineNumbers = true;

            foldingManager = FoldingManager.Install(textEditor.TextArea);
            foldingStrategy = new BraceFoldingStrategy();
            foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);

            textEditor.TextArea.TextEntering += textEditor_TextArea_TextEntering;
            textEditor.TextArea.TextEntered += textEditor_TextArea_TextEntered;

            DispatcherTimer foldingUpdateTimer = new DispatcherTimer();
            foldingUpdateTimer.Interval = TimeSpan.FromSeconds(2);
            foldingUpdateTimer.Tick += foldingUpdateTimer_Tick;
            foldingUpdateTimer.Start();
        }

        void foldingUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (foldingStrategy != null)
            {
                foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
            }
        }

        CompletionWindow completionWindow;
        void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == ".")
            {
                // open code completion after the user has pressed dot:
                completionWindow = new CompletionWindow(textEditor.TextArea);
                // provide AvalonEdit with the data:
                IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
                data.Add(new MyCompletionData("zapline"));
                data.Add(new MyCompletionData("is"));
                data.Add(new MyCompletionData("a"));
                data.Add(new MyCompletionData("sb"));
                completionWindow.Show();
                completionWindow.Closed += delegate
                {
                    completionWindow = null;
                };
            }
        }

        void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && completionWindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    // Whenever a non-letter is typed while the completion window is open,
                    // insert the currently selected element.
                    completionWindow.CompletionList.RequestInsertion(e);
                }
            }
            // do not set e.Handled=true - we still want to insert the character that was typed
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var sql = textEditor.Text;

        }
    }
}
