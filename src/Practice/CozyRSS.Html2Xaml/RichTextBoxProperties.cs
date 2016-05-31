using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;

namespace CozyRSS.Html2Xaml
{
    public class Properties : DependencyObject
    {
        public static readonly DependencyProperty HtmlProperty =
            DependencyProperty.RegisterAttached("Html", typeof(string), typeof(Properties), new PropertyMetadata(null, HtmlChanged));

        public static void SetHtml(DependencyObject obj, string value)
        {
            obj.SetValue(HtmlProperty, value);
        }

        public static string GetHtml(DependencyObject obj)
        {
            return (string)obj.GetValue(HtmlProperty);
        }

        public static Dictionary<string, Dictionary<string, string>> TagAttributes = null;
        private static void HtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RichTextBox)
            {
                string html = e.NewValue as string;
                RichTextBox richText = d as RichTextBox;
                if (richText == null) return;
                richText.Document.Blocks.Clear();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<?xml version=\"1.0\"?>");
                sb.AppendLine("<FlowDocument xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">");
                sb.AppendLine(Html2XamlConverter.Convert2Xaml(html, TagAttributes));
                sb.AppendLine("</FlowDocument>");

                try
                {
                    StringReader strreader = new StringReader(sb.ToString());
                    XmlTextReader xmlreader = new XmlTextReader(strreader);
                    FlowDocument document = (FlowDocument)XamlReader.Load(xmlreader);
                    richText.Document = document;
                }
                catch (Exception)
                {
                    //Convert faild
                }
            }
        }
    }
}
