using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain;

namespace TodoApp.Data.Mappings
{
    public class TodoGroupMap : IEntityTypeConfiguration<TodoGroup>
    {
        public void Configure(EntityTypeBuilder<TodoGroup> builder)
        {
            builder.ToTable("TodoGroup");
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .HasColumnType("NVARCHAR");

            builder.Property(x => x.Slug)
                .HasMaxLength(100)
                .HasColumnType("VARCHAR");

            builder.HasIndex(x => x.Slug).IsUnique();

        }
    }
}
