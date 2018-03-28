using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public interface IDatabaseContext
    {
        DbSet<House> Houses { get; set; }
        int SaveChanges();
    }
}
