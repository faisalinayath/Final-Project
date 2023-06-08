namespace PropDealsNew.Models
{
    public class ViewModel
    {
        public List<Property> PropertyList { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public Property property { get; set; }
        public List<Ad> AdvertList { get; set; }

        public string Id { get; set; }
        public Register RegisterObj { get; set; }
    }
}
