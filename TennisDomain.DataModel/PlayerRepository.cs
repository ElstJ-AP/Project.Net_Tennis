using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisDomain.Classes;

namespace TennisDomain.DataModel
{
    public class PlayerRepository : IEntityRepository<Player>
    {
        private TennisContext context;

        public PlayerRepository(TennisContext context)
        {
            this.context = context;
        }

        public List<Player> GetAll()
        {            
            return context.Players.ToList();            
        }

        public Player GetById(int id)
        {
            return context.Players.Find(id);
        }

        public Player GetByIdWithRoles(int id)
        {
            return context.Players.Include(p => p.Roles).FirstOrDefault(x => x.Id == id);
        }

        public Player GetByIdWithEverything(int id)
        {
            return context.Players.Include(p => p.Roles).Include(p => p.Fines).Include(p => p.Player1Games).Include(p => p.Player2Games).FirstOrDefault(x => x.Id == id);
        }

        public Player GetByIdWithGames(int id)
        {
            return context.Players.Include(p => p.Player1Games).Include(p => p.Player2Games).FirstOrDefault(x => x.Id == id);
        }

        public List<Player> GetFilteredByNameNumber(string filter)
        {
            return context.Players.Where(p => (p.LastName + " " + p.FirstName + " " + p.PlayerNr).ToLower().Contains(filter.ToLower())).ToList();
        }

        public void Create(Player player)
        {
            context.Players.Add(player);
        }

        public void Delete(int id)
        {
            Player player = this.GetById(id);
            context.Players.Remove(player);
        }

        public void Update(Player player)
        {
            context.Entry(player).State = EntityState.Modified;
        }

        public void UpdateRoles(int playerId, List<Role> roleList)
        {
            Player existingPlayer = GetByIdWithRoles(playerId);

            var deletedRoles = Except(existingPlayer.Roles, roleList).ToList();
            var addedRoles = Except(roleList, existingPlayer.Roles).ToList();

            deletedRoles.ForEach(c => existingPlayer.Roles.Remove(c));

            //5- Add new courses
            foreach (Role p in addedRoles)
            {
                if (context.Entry(p).State == EntityState.Detached)
                {
                    context.Roles.Attach(p);
                }
                existingPlayer.Roles.Add(p);
            }
        }

        public IEnumerable<Role> Except(IEnumerable<Role> items, IEnumerable<Role> other)
        {
            return from item in items
                   join otherItem in other on item.Id
                   equals otherItem.Id into tempItems
                   from temp in tempItems.DefaultIfEmpty()
                   where ReferenceEquals(null, temp) || temp.Equals(default(Player))
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
