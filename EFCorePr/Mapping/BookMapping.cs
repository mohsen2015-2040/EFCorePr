using EFCorePr.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePr.Mapping
{
    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(x => x.Id);

            builder.Property(b => b.Title).HasMaxLength(255);

            builder.Property(b => b.Title).HasColumnName("Title");

            builder.Property("Author").HasMaxLength(255);

            builder.Property("Description").HasMaxLength(255);
        }
    }
}
