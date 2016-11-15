using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisDomain.Classes
{
    public class Role
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [DisplayName("Role Name")]
        public string Name { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
