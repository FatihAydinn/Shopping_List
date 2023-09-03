using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping_List.Models
{
    public class ListDetails
    {
        [Key]
        public int DetailsId { get; set; }

        public int HeaderId { get; set; }

        //[ForeignKey("HeaderId")]
        //public virtual ListHeaders ListHeaders { get; set; }
        public int ProductId { get; set; }
        public int ListId { get; set; }

        [ForeignKey("ListId")]
        public virtual ShoppingList ShoppingLists { get; set; }
        public string ProductTitle { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
