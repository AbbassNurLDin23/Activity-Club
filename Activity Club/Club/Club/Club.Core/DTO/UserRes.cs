using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.DTO
{
    public class UserRes
    {
        public string? email {  get; set; }
        public String? Password { get; set; }
        public String? Name { get; set; }
        public DateTime? DOB { get; set; }
        public String? Gender { get; set; }
    }
}
