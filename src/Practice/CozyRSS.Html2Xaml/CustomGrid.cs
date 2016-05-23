using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CozyRSS.Html2Xaml
{
    public class CustomGrid : Grid
    {
        public string GetXaml(Dictionary<string, string> TextBlockAttributes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<Viewbox StretchDirection=\"DownOnly\"><Grid>");

            sb.Append("<Grid.ColumnDefinitions>");
            foreach (var column in this.ColumnDefinitions)
                sb.Append("<ColumnDefinition/>");
            sb.Append("</Grid.ColumnDefinitions>");

            sb.Append("<Grid.RowDefinitions>");
            foreach (var row in this.RowDefinitions)
                sb.Append("<RowDefinition/>");
            sb.Append("</Grid.RowDefinitions>");

            string tbAttr = string.Empty;
            if (TextBlockAttributes.Count > 0)
                tbAttr = string.Join(string.Empty, TextBlockAttributes.Select(tb => string.Format(" {0}=\"{1}\"", tb.Key, tb.Value)).ToArray());

            List<RowAdjustment> adjustments = new List<RowAdjustment>();
            foreach (var item in this.Children.OfType<TextBlock>())
            {
                int column = Grid.GetColumn(item);
                int row = Grid.GetRow(item);
                int colSpan = Grid.GetColumnSpan(item);
                int rowSpan = Grid.GetRowSpan(item);

                if (adjustments.Count > 0)
                    column += adjustments.Sum(a => a.Row == row && a.StartAt <= column ? a.AdjAmt : 0);

                bool isBold = item.FontWeight.Equals(FontWeights.Bold);
                sb.Append(string.Format("<TextBlock TextTrimming=\"WordEllipsis\" TextWrapping=\"Wrap\"{0}", tbAttr));
                if (column > 0)
                    sb.Append(string.Format(" Grid.Column=\"{0}\" Margin=\"4,0,0,0\"", column));
                if (colSpan > 1)
                    sb.Append(string.Format(" Grid.ColumnSpan=\"{0}\" TextAlignment=\"Center\"", colSpan));
                if (row > 0)
                    sb.Append(string.Format(" Grid.Row=\"{0}\"", row));
                if (rowSpan > 1)
                {
                    for (int xx = 1; xx < rowSpan; xx++)
                    {
                        // To manually track pushing columns over in the Xaml.
                        adjustments.Add(new RowAdjustment() { Row = row + xx, StartAt = column, AdjAmt = colSpan });
                    }
                    sb.Append(string.Format(" VerticalAlignment=\"Center\" Grid.RowSpan=\"{0}\"", rowSpan));
                }
                if (isBold)
                    sb.Append(" FontWeight=\"Bold\"");
                sb.Append(string.Format(" Text=\"{0}\" />", item.Text));
            }
            sb.Append("</Grid></Viewbox>");
            return sb.ToString();
        }

        /// <summary>
        /// Because html can't overlap, it treats rowspans differently than Xaml. That means we have to track
        /// how far over to push subsequent columns manually. This class allows us to keep track of potentially
        /// multiple rowspan adjustments.
        /// </summary>
        private class RowAdjustment
        {
            public int Row { get; set; }
            public int StartAt { get; set; }
            public int AdjAmt { get; set; }
        }
    }
}
