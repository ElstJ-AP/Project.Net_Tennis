using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisDomain.Classes;

namespace TennisDomain.DataModel
{
    public class GameRepository : IEntityRepository<Game>
    {
        private TennisContext context;

        public GameRepository(TennisContext context)
        {
            this.context = context;
        }

        public List<Game> GetAll()
        {            
            return context.Games.ToList();            
        }

        public List<Game> GetAllWithPlayers()
        {
            return context.Games.Include(g => g.Player1).Include(g => g.Player2).ToList();
        }

        public Game GetById(int id)
        {
            return context.Games.Find(id);
        }

        public Game GetByIdWithPlayers(int id)
        {
            return context.Games.Include(g => g.Player1).Include(g => g.Player2).FirstOrDefault(x => x.Id == id);
        }

        public List<Game> GetByPlayerId(int id)
        {
            return context.Games.Where(x => x.Player1Id == id || x.Player2Id == id).ToList();
        }

        public void Create(Game game)
        {
            context.Games.Add(game);
        }

        public void Delete(int id)
        {
            Game game = this.GetById(id);
            context.Games.Remove(game);
            context.Entry(game).State = EntityState.Deleted;
        }

        public void Update(Game game)
        {
            context.Entry(game).State = EntityState.Modified;
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
