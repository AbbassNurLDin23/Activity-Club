namespace Club.Core.DataModels
{
    public class Member : User
    {
        public int MobileNumber { get; set; }
        public int? EmergencyNumber { get; set; }
        //public Byte[]? Photo { get; set; }
        public string? Profession { get; set; }
        public String? Nationality { get; set; }

        //Navigation properties
        public List<Event>? MemberEvents { get; set; } = new List<Event>();
    }
}
