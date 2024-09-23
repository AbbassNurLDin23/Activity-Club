using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Club.Core.DataModels
{
    public class Lookup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Order { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }

        //Navigation properties
        public List<User>? Admins { get; set; } = new List<User>();
        public List<Event>? Events { get; set; } = new List<Event>();
    }
}
