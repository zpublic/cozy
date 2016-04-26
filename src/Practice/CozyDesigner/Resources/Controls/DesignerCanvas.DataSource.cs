using CozyDesigner.Logic.Select;
using System.Collections.Generic;
using System.Linq;

namespace CozyDesigner.Resources.Controls
{
    public partial class DesignerCanvas : IDataSource
    {
        public IEnumerable<IGroupable> GetGroupableList()
        {
            return Children.OfType<IGroupable>();
        }

        public IEnumerable<ISelectable> GetSelectableList()
        {
            return Children.OfType<ISelectable>();
        }
    }
}
