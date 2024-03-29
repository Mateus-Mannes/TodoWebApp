﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain;

namespace TodoApp.Data.Mappings;

public class TodoMap : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable("Todo");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.DeadLine)
            .HasColumnType("DATE");


        builder.Property(x => x.CreatedAt)
            .HasColumnType("DATE")
            .HasDefaultValueSql("DATE('now')");

        builder.HasOne(x => x.TodoGroup).WithMany(x => x.Todos).HasForeignKey(x => x.TodoGroupId).OnDelete(DeleteBehavior.Cascade);
    }
}
