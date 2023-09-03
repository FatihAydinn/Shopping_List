using System.ComponentModel.DataAnnotations;

namespace Shopping_List.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string ProductTilte { get; set; }
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string ImageUrl { get; set; }
        public string ProductCategory { get; set; }
    }
}
