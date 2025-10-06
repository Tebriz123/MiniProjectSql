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
    internal class ReservedItemConfigration : IEntityTypeConfiguration<ReservedItem>
    {
        public void Configure(EntityTypeBuilder<ReservedItem> builder)
        {
            builder
                .HasIndex(b => b.FinCode);

            builder
                .Property(b => b.FinCode)
                .IsRequired()
                .HasColumnType("CHAR(7)");
            builder
               .HasKey(b => b.Id);
            builder
                 .Property(b => b.Id)
                .ValueGeneratedOnAdd();

        }
    }
}
