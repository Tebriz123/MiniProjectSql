using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProjectSql.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectSql.Persistance.Configrations
{
    public class BookConfigration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
               .Property(b => b.Name)
               .HasMaxLength(100)
               .IsRequired();
            builder
                 .HasKey(b => b.Id);
            builder
                 .Property(b => b.Id)
                .ValueGeneratedOnAdd();

        }
    }
}
