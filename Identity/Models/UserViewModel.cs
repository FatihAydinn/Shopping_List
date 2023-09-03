using Microsoft.AspNetCore.Identity;

namespace Shopping_List.Models
{
    public class UserViewModel
    {
        public CustomUser customUser { get; set; }
        public Product product { get; set; }
    }
}
