using MiniProjectSql.Domain.Entities.Common;
using MiniProjectSql.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectSql.Domain.Entities
{
    public class ReservedItem :BaseEntity
    {
        public string FinCode { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndTime { get; set; }

        public Status Status   { get; set; }

        //Reletions
        public int BookId { get; set; }

        public Book Book { get; set; }



    }
}
