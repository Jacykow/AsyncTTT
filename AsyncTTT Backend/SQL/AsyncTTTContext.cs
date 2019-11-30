using Microsoft.EntityFrameworkCore;

namespace AsyncTTT_Backend.SQL
{
    public partial class AsyncTTTContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:async-ttt-server.database.windows.net,1433;Initial Catalog=AsyncTTT-DB;Persist Security Info=False;User ID=tttadmin;Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;".Replace("{password}", DB_PASSWORD));
        }
    }
}
