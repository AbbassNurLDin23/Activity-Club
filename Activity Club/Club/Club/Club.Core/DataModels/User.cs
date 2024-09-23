using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Club.Core.DataModels
{
    public class User
    {
        [Key]
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public DateTime? DOB { get; set; }
        public String Gender { get; set; }
        public List<String>? Roles { get; set; } = new List<string>();

        //Navigation properties
        //public List<User>? Admins { get; set; } = new List<User>();
        //public List<User>? Users { get; set; } = new List<User>();
        public List<Lookup>? Lookups { get; set; } = new List<Lookup>();
        public List<Event>? Events { get; set; } = new List<Event>();
    }
}
