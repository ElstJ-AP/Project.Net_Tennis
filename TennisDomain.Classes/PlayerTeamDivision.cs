using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisDomain.Classes
{
    public class PlayerTeamDivision
    {
        [Key, Column(Order = 0)]
        public int PlayerId { get; set; }
        [Key, Column(Order = 1)]
        public int TeamId { get; set; }
        [Key, Column(Order = 2)]
        public int DivisionId { get; set; }

        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
        public virtual Division Division { get; set; }

        public PlayerTeamDivision()
        {
        }

        public PlayerTeamDivision(int playerId, int teamId, int divisionId)
        {
            this.PlayerId = playerId;
            this.TeamId = teamId;
            this.DivisionId = divisionId;
        }
    }
}
