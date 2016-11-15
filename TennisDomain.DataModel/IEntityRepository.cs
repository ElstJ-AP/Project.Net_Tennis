using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TennisDomain.Classes;

namespace TennisDomain.DataModel
{
    public interface IEntityRepository<T> : IDisposable
    {
        List<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Delete(int id);
        void Update(T entity);
        void Save();
    }
}
