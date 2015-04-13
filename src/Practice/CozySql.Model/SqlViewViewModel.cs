using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySql.Model
{
    public class SqlViewViewModel : BaseViewModel
    {
        private object _data;
        public object Data
        {
            get { return _data; }
            set
            {
                Set(ref _data, value, "Data");
            }
        }

        public void TestData()
        {
            var table = new DataTable();

            table.Columns.Add("aaa");
            table.Columns.Add("bb");
            table.Columns.Add("cccc");

            table.Rows.Add(new string[] { "data1", "data2", "data3" });
            table.Rows.Add(new string[] { "data1", "data2", "data3" });
            table.Rows.Add(new string[] { "data1", "data2", "data3" });
            table.Rows.Add(new string[] { "data1", "data2", "data3" });

            Data = table.DefaultView;
        }
    }
}
