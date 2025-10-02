using Microsoft.EntityFrameworkCore;
using MiniProjectSql.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectSql.Persistance
{
    internal class AppDbContex:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("server=Tebriz;database=onionproject;trusted_connection=true;integrated security=true;TrustServerCertificate=true;");
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public DbSet<ReservedItem> ReservedItems { get; set; }
    }
}
