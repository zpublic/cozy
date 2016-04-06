using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace MMS.UI.Default
{
    public class Table : System.Windows.Controls.Control
    {
        private object mSource;
        public object Source { get { return this.mSource; } set { this.mSource = value; } }
        private Grid mainTable;

        public Table()
        {
            this.Style = (Style)Application.Current.Resources["TableStyle"];
        }

        public override void OnApplyTemplate()
        {
            this.mainTable = (Grid)this.GetTemplateChild("mainTable");
            if (this.Source != null)
            {
                this.UpdateContext();
            }
            base.OnApplyTemplate();
        }

        public void UpdateContext()
        {
            if (this.Source is string)
            {
                this.UpateContextByJson();
            }
        }

        private void UpateContextByJson()
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(this.Source as string);
            //添加<RowDefinition Height="22" />
            for (int i = 0; i < dt.Rows.Count + 1; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(22);
                this.mainTable.RowDefinitions.Add(rowDefinition);
            }
            //添加表头
            Grid headerGrid = new Grid();
            headerGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FCFCFC"));
            Grid.SetRow(headerGrid, 0);
            ColumnDefinition headerColumnDefinition = new ColumnDefinition();
            headerColumnDefinition.Width = new GridLength(40);
            headerGrid.ColumnDefinitions.Add(headerColumnDefinition);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                headerGrid.ColumnDefinitions.Add(columnDefinition);
            }
            Grid headerFirstGrid = new Grid();
            Border headerBorder = new Border();
            headerBorder.BorderThickness = new Thickness(1);
            headerBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E1E1E1"));
            headerFirstGrid.Children.Add(headerBorder);
            headerFirstGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FCFCFC"));
            Grid.SetColumn(headerFirstGrid, 0);
            headerGrid.Children.Add(headerFirstGrid);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Grid dataGrid = new Grid();
                Grid.SetColumn(dataGrid, i + 1);
                dataGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FCFCFC"));
                Border dataBorder = new Border();
                dataBorder.BorderThickness = new Thickness(1);
                dataBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E1E1E1"));
                //添加<TextBlock Text="1" VerticalAlignment="Center" HorizontalAlignment="Center" />
                TextBlock text = new TextBlock();
                text.Text = dt.Columns[i].ColumnName;
                text.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                text.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                dataGrid.Children.Add(dataBorder);
                dataGrid.Children.Add(text);
                headerGrid.Children.Add(dataGrid);
            }
            this.mainTable.Children.Add(headerGrid);
            //添加<Grid Grid.Row="0" Background="#FCFCFC">
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Grid grid = new Grid();
                Grid.SetRow(grid, i + 1);
                //添加<ColumnDefinition Width="40"/>
                ColumnDefinition columnDefinitionFirst = new ColumnDefinition();
                columnDefinitionFirst.Width = new GridLength(40);
                grid.ColumnDefinitions.Add(columnDefinitionFirst);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ColumnDefinition columnDefinition = new ColumnDefinition();
                    grid.ColumnDefinitions.Add(columnDefinition);
                }
                //添加默认列
                Grid firstGrid = new Grid();
                firstGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FCFCFC"));
                Border border = new Border();
                border.BorderThickness = new Thickness(1);
                border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E1E1E1"));
                //添加<TextBlock Text="1" VerticalAlignment="Center" HorizontalAlignment="Center" />
                TextBlock textFirst = new TextBlock();
                textFirst.Text = (i + 1).ToString();
                textFirst.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                textFirst.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                firstGrid.Children.Add(border);
                firstGrid.Children.Add(textFirst);
                Grid.SetColumn(firstGrid, 0);
                grid.Children.Add(firstGrid);
                //添加数据列
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Grid dataGrid = new Grid();
                    Grid.SetColumn(dataGrid, j + 1);
                    dataGrid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    Border dataBorder = new Border();
                    dataBorder.BorderThickness = new Thickness(1);
                    dataBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F0F0F0"));
                    //添加<TextBlock Text="1" VerticalAlignment="Center" HorizontalAlignment="Center" />
                    TextBlock text = new TextBlock();
                    text.Text = dt.Rows[i][j].ToString();
                    text.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    text.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    dataGrid.Children.Add(dataBorder);
                    dataGrid.Children.Add(text);
                    grid.Children.Add(dataGrid);
                }
                this.mainTable.Children.Add(grid);
            }
        }
    }
}
