using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProjectSql.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectSql.Persistance.Configrations
{
    public class AuthorConfigration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);

            builder
                .Property(a => a.Name)
                .IsRequired();
            builder
                .Property(a => a.Surname)
                .IsRequired(false);
            builder
               .HasKey(b => b.Id);
            builder
                 .Property(b => b.Id)
                .ValueGeneratedOnAdd();





        }
    }
}
