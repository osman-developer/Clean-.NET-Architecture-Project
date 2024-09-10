using ClearnArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(p => new { p.BookId });
            builder.HasIndex(p => new { p.BookId }).IsUnique();
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.CreationDate).IsRequired();
        }

    }
}
