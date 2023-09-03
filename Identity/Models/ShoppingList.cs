using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping_List.Models
{
    public class ShoppingList
    {
        [Key]
        public int ListId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string ListTitle { get; set; }
        public string Status { get; set; }

        public int HeaderId { get; set; }

        [ForeignKey("HeaderId")]
        public virtual ListHeaders ListHeaders { get; set; }
    }
}
