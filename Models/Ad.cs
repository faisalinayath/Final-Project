using System.ComponentModel.DataAnnotations.Schema;

namespace PropDealsNew.Models
{
    public class Ad
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long Price { get; set; }

        public string Description { get; set; }

        public string Image_Path { get; set; } = "hello";

        public string Seller_Name { get; set; }
        public string Seller_Phone { get; set; }

        public string Seller_Email_Id { get; set; }

    }
}
