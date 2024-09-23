namespace Club.Core.DataModels
{
    public class Guide : User
    {
        //public byte[]? Photo { get; set; }
        public string Profession { get; set; }


        //Navigation properties
        public List<Event>? GuideEvents { get; set; } = new List<Event>();
    }
}
