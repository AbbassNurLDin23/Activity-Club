using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Club.Core.DataModels
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int id { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set;   }
        public string   Name { get; set; }
        public string? Description { get; set; }
        public int Cost { get; set; }
        public string? Status { get; set; }
        public string? Destination { get; set; }

        //Foreign key
        public int Category { get; set; }


        //Navigation properties
        public List<User>? Users { get; set; } = new List<User>();
        public List<Guide>? Guides { get; set; } = new List<Guide>();
        public List<Member>? Members { get; set; } = new List<Member>();
        public Lookup? lookup { get; set; }
    }
}
