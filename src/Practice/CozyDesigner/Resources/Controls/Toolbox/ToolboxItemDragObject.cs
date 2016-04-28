using System;
using System.Windows;

namespace CozyDesigner.Resources.Controls.Toolbox
{
    // Wraps info of the dragged object into a class
    public class ToolboxItemDragObject
    {
        // Xaml string that represents the serialized content
        public String Xaml { get; set; }

        // Defines width and height of the DesignerItem
        // when this DragObject is dropped on the DesignerCanvas
        public Size? DesiredSize { get; set; }
    }
}
