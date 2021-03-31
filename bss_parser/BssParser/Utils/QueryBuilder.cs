using System.Collections.Generic;
using System.Linq;

namespace BssParser.Utils
{
    public class QueryBuilder
    {
        private string _tableName { get; set; }
        private Dictionary<string, string> _columns { get; set; }

        public QueryBuilder(string tableName)
        {
            _tableName = tableName;
            _columns = new Dictionary<string, string>();
            // INSERT INTO "Buff_Table" ("ID", "Index", "BuffName") VALUES (1001, 3053, 'asdfsad')
        }

        public void Add(string column, object data)
        {
            _columns.Add(column, data?.ToString() ?? "");
        }

        public string Combine()
        {
            return $"INSERT INTO \"{_tableName}\" ({string.Join(",", _columns.Select(x => "\"" + x.Key + "\""))}) VALUES ({string.Join(",", _columns.Select(x => "'" + x.Value + "'"))});"; // was '
        }

        public static string Prepare(string table)
        {
            return $"DELETE FROM \"{table}\";";
        }
    }
}
