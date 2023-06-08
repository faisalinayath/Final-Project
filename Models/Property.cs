using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropDealsNew.Models
{
    public class Property
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long Price { get; set; }

        public string Description { get; set; }

        public string Image_Path { get; set; } = "hello";


        public string Seller_Name { get; set; } = "dummy_data";

        public string Seller_Phone { get; set; } = "dummy_data";

        public string Seller_Email_Id { get; set; } = "dummy_data";

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
