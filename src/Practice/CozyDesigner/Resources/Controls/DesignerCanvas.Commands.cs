using System.Windows.Input;

namespace CozyDesigner.Resources.Controls
{
    public partial class DesignerCanvas
    {
        public DesignerCanvas()
        {
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.New, New_Executed));
            this.AllowDrop = true;
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Children.Clear();
        }
    }
}
