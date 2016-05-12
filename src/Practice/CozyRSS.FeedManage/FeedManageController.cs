using System.Collections.Generic;
using System.IO;

namespace CozyRSS.FeedManage
{
    public class FeedManageController
    {
        FeedCategory _feedTree;

        public bool SaveToFile(string filePath)
        {
            if (_feedTree != null)
            {
                var json = JsonUtil.Obj2Json(_feedTree);
                if (json?.Length > 0)
                {
                    using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        using (var writer = new StreamWriter(fs))
                        {
                            writer.Write(json);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool ReadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(fs))
                    {
                        var feeds = JsonUtil.Json2Obj(reader.ReadToEnd());
                        if (feeds?.subCategories?.Count > 0 || feeds?.subNodes?.Count > 0)
                        {
                            _feedTree = feeds;
                            resetParent(_feedTree);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        void resetParent(FeedCategory category)
        {
            if (category.subCategories != null)
            {
                foreach (var c in category.subCategories)
                {
                    c.parent = category;
                    resetParent(c);
                }
            }
            if (category.subNodes != null)
            {
                foreach (var n in category.subNodes)
                {
                    n.parent = category;
                }
            }
        }

        public FeedCategory Feeds { get { return _feedTree; } }

        public void AddCategory(string categoryName)
        {
            if (_feedTree == null)
                _feedTree = new FeedCategory() { name = "root" };
            AddCategory(_feedTree, categoryName);
        }
        public void AddCategory(FeedCategory parentCategory, string categoryName)
        {
            if (parentCategory.subCategories == null)
                parentCategory.subCategories = new List<FeedCategory>();
            parentCategory.subCategories.Add(new FeedCategory() { name = categoryName, parent = parentCategory });
        }

        public FeedNode AddFeed(string FeedName, string FeedUrl)
        {
            if (_feedTree == null)
                _feedTree = new FeedCategory() { name = "root" };
            return AddFeed(_feedTree, FeedName, FeedUrl);
        }
        public FeedNode AddFeed(FeedCategory parentCategory, string FeedName, string FeedUrl)
        {
            if (parentCategory.subNodes == null)
                parentCategory.subNodes = new List<FeedNode>();
            FeedNode i = new FeedNode() { name = FeedName, url = FeedUrl, parent = parentCategory };
            parentCategory.subNodes.Add(i);
            return i;
        }

        public void RemoveCategory(FeedCategory category)
        {
            category?.parent?.subCategories?.Remove(category);
        }
        public void RemoveNode(FeedNode node)
        {
            node?.parent?.subNodes?.Remove(node);
        }
    }
}
