namespace Shopping_List.Models
{
    public class ListDto
    {
        public IEnumerable<ShoppingList> ShoppingList { get; set; }
        public IEnumerable<ListDetails> listDetails { get; set; }
        public ListDetails listDetail { get; set; }
    }
}
