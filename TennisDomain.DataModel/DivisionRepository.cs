using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisDomain.Classes;

namespace TennisDomain.DataModel
{
    public class DivisionRepository : IEntityRepository<Division>
    {
        private TennisContext context;

        public DivisionRepository(TennisContext context)
        {
            this.context = context;
        }

        public List<Division> GetAll()
        {            
            return context.Divisions.AsNoTracking().ToList();            
        }

        public Division GetById(int id)
        {
            return context.Divisions.Find(id);
        }

        public List<Division> GetFilteredByName(string filter)
        {
            return context.Divisions.Where(p => p.Name.ToLower().Contains(filter.ToLower())).ToList();
        }

        public void Create(Division division)
        {
            context.Divisions.Add(division);
        }

        public void Delete(int id)
        {
            Division division = this.GetById(id);
            context.Divisions.Remove(division);
        }

        public void Update(Division division)
        {
            context.Entry(division).State = EntityState.Modified;
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
