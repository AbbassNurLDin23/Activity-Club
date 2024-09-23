namespace Club.Resources
{
    public class MemberResource : UserResource
    {
        public int MobileNumber { get; set; }
        public int? EmergencyNumber { get; set; }
        //public Byte[]? Photo { get; set; }
        public string? Profession { get; set; }
        public String? Nationality { get; set; }
    }
}
