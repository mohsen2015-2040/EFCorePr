using EFCorePr.Mapping;
using EFCorePr.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EFCorePr.DatabaseContext
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
