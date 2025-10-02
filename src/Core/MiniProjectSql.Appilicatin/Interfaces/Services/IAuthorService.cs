using MiniProjectSql.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectSql.Appilicatin.Interfaces.Services
{
    public interface IAuthorService
    {
        void Create();
        void Delete();
        List<Author> GetAll();
        List<Author> GetById();

    }
}
