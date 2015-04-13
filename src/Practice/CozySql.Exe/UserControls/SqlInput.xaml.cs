using ICSharpCode.AvalonEdit;
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
    }
}
