using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Club.Resources
{
    public class UserResource
    {
        [EmailAddress]
        public string Email { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public DateTime? DOB { get; set; }
        public String Gender { get; set; }
    }
}
