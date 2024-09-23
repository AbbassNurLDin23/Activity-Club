using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.DTO
{
    public class UserView
    {
        [EmailAddress]
        public string? Email { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public DateTime? DOB { get; set; }
        public String Gender { get; set; }
        public List<String>? Roles { get; set; }
    }
}
