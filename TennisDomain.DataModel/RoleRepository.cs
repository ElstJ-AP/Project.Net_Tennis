using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisDomain.Classes;

namespace TennisDomain.DataModel
{
    public class RoleRepository : IEntityRepository<Role>
    {
        private TennisContext context;

        public RoleRepository(TennisContext context)
        {
            this.context = context;
        }

        public List<Role> GetAll()
        {            
            return context.Roles.AsNoTracking().ToList();            
        }

        public List<Role> GetAllWithPlayers()
        {
            return context.Roles.Include(g => g.Players).ToList();
        }

        public Role GetById(int id)
        {
            return context.Roles.Find(id);
        }

        public Role GetByIdWithPlayers(int id)
        {
            return context.Roles.Include(g => g.Players).FirstOrDefault(x => x.Id == id);
        }

        public List<Role> GetFilteredByName(string filter)
        {
            return context.Roles.Where(p => p.Name.ToLower().Contains(filter.ToLower())).ToList();
        }

        public void Create(Role role)
        {
            context.Roles.Add(role);
        }

        public void Delete(int id)
        {
            Role role = this.GetById(id);
            context.Roles.Remove(role);
        }

        public void Update(Role role)
        {
            context.Entry(role).State = EntityState.Modified;
        }

        public void UpdatePlayers(int roleId, List<Player> playerList)
        {
            Role existingRole = GetByIdWithPlayers(roleId);

            var deletedPlayers = Except(existingRole.Players, playerList).ToList();
            var addedPlayers = Except(playerList, existingRole.Players).ToList();

            deletedPlayers.ForEach(c => existingRole.Players.Remove(c));

            //5- Add new courses
            foreach (Player p in addedPlayers)
            {
                if (context.Entry(p).State == EntityState.Detached)
                {
                    context.Players.Attach(p);
                }
                existingRole.Players.Add(p);
            }
        }

        public IEnumerable<Player> Except(IEnumerable<Player> items, IEnumerable<Player> other)
        {
            return from item in items
                   join otherItem in other on item.Id
                   equals otherItem.Id into tempItems
                   from temp in tempItems.DefaultIfEmpty()
                   where ReferenceEquals(null, temp) || temp.Equals(default(Role))
                   select item;
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
