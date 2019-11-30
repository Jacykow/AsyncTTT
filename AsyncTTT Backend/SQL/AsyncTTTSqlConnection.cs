using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace AsyncTTT_Backend.SQL
{
    public static partial class AsyncTTTSqlConnection
    {
        public static string ConnectionString => "Server=tcp:async-ttt-server.database.windows.net,1433;Initial Catalog=AsyncTTT-DB;Persist Security Info=False;User ID=tttadmin;Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;".Replace("{password}", DB_PASSWORD);

        public static List<TModel> ExecuteSql<TModel>(SimpleSqlCommand<TModel> simpleSqlCommand)
        {
            var modelList = new List<TModel>();
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                using var cmd = new SqlCommand()
                {
                    CommandText = simpleSqlCommand.SqlCommand,
                    CommandType = CommandType.Text,
                    Connection = sqlConnection
                };
                if (simpleSqlCommand.Parameters != null)
                {
                    cmd.Parameters.AddRange(simpleSqlCommand.Parameters);
                }
                sqlConnection.Open();

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    modelList.Add(simpleSqlCommand.ModelExtractor(reader));
                }
            }
            return modelList;
        }
    }
}
