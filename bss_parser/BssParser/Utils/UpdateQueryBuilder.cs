using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BssParser.Utils
{
    public class UpdateQueryBuilder
    {
        private string _tableName { get; set; }
        private Dictionary<string, object> _columns { get; set; }

        public UpdateQueryBuilder(string tableName)
        {
            _tableName = tableName;
            _columns = new Dictionary<string, object>();
        }

        public void Add(string column, object data)
        {
            _columns.Add(column, data);
        }

        public string Combine(string primaryKey, string primaryValue)
        {
            return $"UPDATE \"{_tableName}\" SET {string.Join(",", _columns.Select(x => x.Value == null ? "\"" + x.Key + "\"= NULL" : ("\"" + x.Key + "\"=\"" + x.Value + "\"")))} WHERE \"{primaryKey}\"=\"{primaryValue}\";"; // was '
        }

        public string MultiCombine(string[] keys, object[] values)
        {
            var sb = new StringBuilder();
            var idx = 0;
            foreach (var x in keys)
            {
                if (sb.Length != 0)
                    sb.Append(" AND ");
                sb.Append($"\"{x}\"=\"{values[idx++]}\"");
            }

            return $"UPDATE \"{_tableName}\" SET {string.Join(",", _columns.Select(x => x.Value == null ? "\"" + x.Key + "\"= NULL" : ("\"" + x.Key + "\"=\"" + x.Value + "\"")))} " +
                   $"WHERE {sb};"; // was '
        }
    }
}
