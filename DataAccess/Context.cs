using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class Context : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
    }
}