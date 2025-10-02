using MiniProjectSql.Domain.Entities.Common;
using MiniProjectSql.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectSql.Domain.Entities
{
    public class Author:BaseEntity
    {
        public string Name { get; set; }
        public string? Surname { get; set; }

        public Gender Gender { get; set; }

        //Reletions
        public List<Book> Books { get; set; }

    }
}
