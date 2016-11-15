using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisDomain.Classes;

namespace TennisDomain.DataModel
{
    public class PlayerTeamDivisionRepository
    {
        private TennisContext context;

        public PlayerTeamDivisionRepository(TennisContext context)
        {
            this.context = context;
        }

        public PlayerTeamDivision GetByIds(int playerId, int teamId, int divisionId)
        {
            return context.PlayerTeamDivisions.Find(playerId, teamId, divisionId);
        }

        public void Create(PlayerTeamDivision ptd)
        {
            context.PlayerTeamDivisions.Add(ptd);
        }

        public void Delete(int playerId, int teamId, int divisionId)
        {
            PlayerTeamDivision ptd = GetByIds(playerId, teamId, divisionId);
            context.PlayerTeamDivisions.Remove(ptd);
        }

        public void Update(PlayerTeamDivision ptd)
        {
            context.Entry(ptd).State = EntityState.Modified;
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
