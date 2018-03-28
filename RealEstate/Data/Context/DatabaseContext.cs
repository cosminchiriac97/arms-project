using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class DatabaseContext: DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base((DbContextOptions)options)
        {
         
        }

        public DbSet<House> Houses { get; set; }
    }
}
