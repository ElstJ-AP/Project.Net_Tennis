using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisDomain.Classes
{
    public class Game
    {
        public int Id { get; set; }
        [DisplayName("Player 1")]
        public virtual Player Player1 { get; set; }
        [Required]
        [DisplayName("Player 1")]
        public int Player1Id { get; set; }
        [DisplayName("Player 2")]
        public virtual Player Player2 { get; set; }
        [Required]
        [DisplayName("Player 2")]
        public int Player2Id { get; set; }
        [DisplayName("Player 1 Score")]
        public string Player1Score { get; private set; }
        [NotMapped]
        public int[] Player1ScoreArray
        {
            get
            {
                return Array.ConvertAll(Player1Score.Split(';'), int.Parse);
            }
            set
            {
                var _data = value;
                Player1Score = String.Join(";", _data.Select(p => p.ToString()).ToArray());
            }
        }
        [DisplayName("Player 2 Score")]
        public string Player2Score { get; private set; }
        [NotMapped]
        public int[] Player2ScoreArray
        {
            get
            {
                return Array.ConvertAll(Player2Score.Split(';'), int.Parse);
            }
            set
            {
                var _data = value;
                Player2Score = String.Join(";", _data.Select(p => p.ToString()).ToArray());
            }
        }
    }
}
