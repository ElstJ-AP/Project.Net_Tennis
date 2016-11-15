using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisDomain.Classes
{
    public class Division
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Division Name")]
        public string Name { get; set; }

        public virtual ICollection<PlayerTeamDivision> PlayerTeamDivisions { get; set; }
    }
}
