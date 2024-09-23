using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.DTO
{
    public class EventView
    {
        public int? id {  get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Cost { get; set; }
        public string? Status { get; set; }
        public string? Destination { get; set; }

        //Foreign key
        public int Category { get; set; }
    }
}
