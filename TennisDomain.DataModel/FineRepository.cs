using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisDomain.Classes;

namespace TennisDomain.DataModel
{
    public class FineRepository : IEntityRepository<Fine>
    {
        private TennisContext context;

        public FineRepository(TennisContext context)
        {
            this.context = context;
        }

        public List<Fine> GetAll()
        {            
            return context.Fines.AsNoTracking().ToList();            
        }

        public Fine GetById(int id)
        {
            return context.Fines.Find(id);
        }

        public Fine GetByIdWithPlayer(int id)
        {
            return context.Fines.Include(x => x.Player).FirstOrDefault(x => x.Id == id);
        }

        public void Create(Fine fine)
        {
            context.Fines.Add(fine);
        }

        public void Delete(int id)
        {
            Fine fine = this.GetById(id);
            context.Fines.Remove(fine);
        }

        public void Update(Fine fine)
        {
            context.Entry(fine).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
