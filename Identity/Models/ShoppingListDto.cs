using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping_List.Models
{
    public class ShoppingListDto
    {
        public IEnumerable<ShoppingList> ShoppingList { get; set; }
        public Product product { get; set; }
        public ListDetails listDetails { get; set; }
        public IEnumerable<ListDetails> listDetails2 { get; set; }

        public ListHeaders listHeaders { get; set; }
    }
}
