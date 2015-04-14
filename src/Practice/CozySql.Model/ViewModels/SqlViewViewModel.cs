using Simple.Data;
using Simple.Data.Interop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySql.Model.ViewModels
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
            var queryText = "db.t20150201file.All().Where(db.t20150201file.id < 100);";
            var executor = new QueryExecutor(queryText);
            object result;
            executor.CompileAndRun(db, out result);
            Data = FormatResult(result);

//             IEnumerable<dynamic> list = db.t20150201file.All()
//                 .Where(db.t20150201file.id < 100)
//                 .ToList();
//             var firstRow = list.FirstOrDefault() as IDictionary<string, object>;
//             var table = new DataTable();
//             foreach (var kvp in firstRow)
//             {
//                 table.Columns.Add(kvp.Key);
//             }
//             foreach (var row in list.Cast<IDictionary<string, object>>())
//             {
//                 table.Rows.Add(row.Values.ToArray());
//             }
//             Data = table.DefaultView;
        }

        private static object FormatResult(object result)
        {
            if (result == null)
            {
                return "No results found.";
            }

            if (result is SimpleRecord)
            {
                return FormatDictionary(result as IDictionary<string, object>);
            }

            if (result is SimpleQuery)
            {
                return FormatQuery(result as SimpleQuery);
            }
            return result.ToString();
        }

        private static object FormatQuery(SimpleQuery simpleQuery)
        {
            bool hasCount = simpleQuery.Clauses.OfType<WithCountClause>().Any();

            var list = simpleQuery.ToList();
            if (list.Count == 0)
                return "No matching records.";

            var firstRow = list.FirstOrDefault() as IDictionary<string, object>;
            if (firstRow == null) throw new InvalidOperationException();

            var table = new DataTable();
            foreach (var kvp in firstRow)
            {
                table.Columns.Add(kvp.Key);
            }
            foreach (var row in list.Cast<IDictionary<string, object>>())
            {
                table.Rows.Add(row.Values.ToArray());
            }
            return table.DefaultView;
        }

        private static object FormatDictionary(IEnumerable<KeyValuePair<string, object>> dictionary)
        {
            var table = new DataTable();
            table.Columns.Add("Property");
            table.Columns.Add("Value");

            foreach (var kvp in dictionary)
            {
                table.Rows.Add(kvp.Key, kvp.Value);
            }
            return table.DefaultView;
        }
    }
}
