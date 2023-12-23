using System.ComponentModel.DataAnnotations;

namespace startup.Models.products
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }

       public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

       public int Quantity { get; set; }

        public string Slug { get; set; }

        public string source { get; set; }

        public string category { get; set; }

        public DateTime hsd { get; set; }

        public string Image { get; set; }

        public DateTime DateCreated { get; set; }
    }

}
