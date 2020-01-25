using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace AsyncTTT_Backend.SQL
{
    public class SimpleSqlCommand<TModel>
    {
        public string SqlCommand { get; set; }
        public Func<SqlDataReader, TModel> ModelExtractor { get; set; }
        public SqlParameter[] Parameters { get; set; }
        public bool Query { get; set; } = true;

        public List<TModel> Execute()
        {
            return AsyncTTTSqlConnection.ExecuteSql(this);
        }
    }
}
