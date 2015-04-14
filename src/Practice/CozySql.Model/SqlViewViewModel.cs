using Simple.Data;
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

        private static readonly string _ConnectionString =
            "server=10.20.221.119;user=defend;password=defend;database=defendcloud;";

        private static readonly string _ProviderName = "MySql.Data.MySqlClient";

        public void TestData()
        {
            var db = Database.Opener.OpenConnection(_ConnectionString, _ProviderName);
            IEnumerable<dynamic> list = db.t20150201file.All()
                .Where(db.t20150201file.id < 100)
                .ToList();
            var firstRow = list.FirstOrDefault() as IDictionary<string, object>;
            var table = new DataTable();
            foreach (var kvp in firstRow)
            {
                table.Columns.Add(kvp.Key);
            }
            foreach (var row in list.Cast<IDictionary<string, object>>())
            {
                table.Rows.Add(row.Values.ToArray());
            }
            Data = table.DefaultView;
        }
    }
}
