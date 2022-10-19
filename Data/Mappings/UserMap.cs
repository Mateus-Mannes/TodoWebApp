using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain;

namespace TodoApp.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .HasColumnType("NVARCHAR");

            builder.Property(x => x.Slug)
               .HasMaxLength(100)
               .HasColumnType("VARCHAR");

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(300)
                .HasColumnType("NVARCHAR");
        }
    }
}
