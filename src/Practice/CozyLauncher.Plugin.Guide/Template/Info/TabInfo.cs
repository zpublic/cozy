using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CozyLauncher.Plugin.Guide.Template.Info
{
    public class TabInfo : ITemplateInfo
    {
        public string Header { get; set; }
        public ITemplateInfo Content { get; set; }

        public UIElement GetInfoObject(double width)
        {
            if(string.IsNullOrEmpty(Header) || Content == null)
            {
                throw new ArgumentNullException("header and content cannot be null");
            }

            var item        = new TabItem();
            item.Header     = Header;
            item.Content    = Content.GetInfoObject(width);

            return item;
        }
    }
}
