using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CozyLauncher.Infrastructure.Template.Info
{
    public class TabInfo : ITemplateInfo
    {
        public string Header { get; set; }
        public ITemplateInfo Content { get; set; }

        public FrameworkElement GetInfoObject()
        {
            if(string.IsNullOrEmpty(Header) || Content == null)
            {
                throw new ArgumentNullException("header and content cannot be null");
            }

            var item = new TabItem();

            item.Header = Header;
            item.Content = Content.GetInfoObject();

            return item;
        }
    }
}
