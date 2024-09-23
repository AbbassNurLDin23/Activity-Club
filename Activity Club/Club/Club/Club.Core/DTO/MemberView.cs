using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.DTO
{
    public class MemberView : UserView
    {
        public int MobileNumber { get; set; }
        public int? EmergencyNumber { get; set; }
        //public Byte[]? Photo { get; set; }
        public string? Profession { get; set; }
        public String? Nationality { get; set; }
    }
}
