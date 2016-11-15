using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisDomain.Classes;

namespace TennisDomain.DataModel
{
    public class TeamRepository : IEntityRepository<Team>
    {
        private TennisContext context;

        public TeamRepository(TennisContext context)
        {
            this.context = context;
        }

        public List<Team> GetAll()
        {            
            return context.Teams.AsNoTracking().ToList();            
        }

        public Team GetById(int id)
        {
            return context.Teams.Find(id);
        }

        public List<Team> GetFilteredByName(string filter)
        {
            return context.Teams.Where(p => p.Name.ToLower().Contains(filter.ToLower())).ToList();
        }

        public void Create(Team team)
        {
            context.Teams.Add(team);
        }

        public void Delete(int id)
        {
            Team team = this.GetById(id);
            context.Teams.Remove(team);
        }

        public void Update(Team team)
        {
            context.Entry(team).State = EntityState.Modified;
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
