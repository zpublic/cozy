using System.Collections.Generic;

namespace CozyDesigner.Logic.Select
{
    public interface IDataSource
    {
        IEnumerable<IGroupable> GetGroupableList();
        IEnumerable<ISelectable> GetSelectableList();
    }
}
