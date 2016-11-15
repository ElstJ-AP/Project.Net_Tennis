using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisDomain.Classes
{
    public class Fine
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime FineDate { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float Amount { get; set; }

    }
}
