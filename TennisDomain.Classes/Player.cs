using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using TennisDomain.Classes.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TennisDomain.Classes
{
    public class Player
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Player Number")]
        public int PlayerNr { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Last Name")]
        public String LastName { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("First Name")]
        public String FirstName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }
        public Gender Gender { get; set; }
        [Range(1800, 9999)]
        [DisplayName("Year of joining")]
        public int? JoinYear { get; set; }
        [MaxLength(30)]
        public string Street { get; set; }
        [Range(0, 1000)]
        [DisplayName("House Number")]
        public int? HouseNr { get; set; }
        [MaxLength(10)]
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        [MaxLength(30)]
        public string City { get; set; }
        [MaxLength(15)]
        [DisplayName("Phone Number")]
        public string PhoneNr { get; set; }
        [DisplayName("Federation Number")]
        public int? FederationNr { get; set; }

        public ICollection<Role> Roles { get; set; }
        public ICollection<Fine> Fines { get; set; }
        public ICollection<Game> Player1Games { get; set; }
        public ICollection<Game> Player2Games { get; set; }
        public virtual ICollection<PlayerTeamDivision> PlayerTeamDivisions { get; set; }

        [NotMapped]
        [DisplayName("Player")]
        public string NameNumber
        {
            get
            {
                return LastName + " " + FirstName + " " + PlayerNr;
            }
        }
    }
}
