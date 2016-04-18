using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.UI.Default
{
    public static class MessageBox
    {
        private const string ERROR_ICON = "/MMS.UI;Component/Default/Message/Images/error.png";
        private const string ERROR_TITLE = "错误";

        public static void Error(string text)
        {
            Message message = new Message(ERROR_ICON,ERROR_TITLE,text);
            message.Show();
        }
    }
}
