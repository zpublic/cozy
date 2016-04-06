using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MMS.UI.Default
{
    public class Explorer : Control
    {
        private TreeView mExplorerTree;

        public Explorer()
        {
            this.Style = (Style)Application.Current.Resources["ExplorerStyle"];
        }

        public void UpdateSource(List<ExplorerItem> source)
        {
            this.mExplorerTree.ItemsSource = source;
        }

        public override void OnApplyTemplate()
        {
            this.mExplorerTree = (TreeView)this.GetTemplateChild("explorerTree");
            base.OnApplyTemplate();
        }
    }
}
