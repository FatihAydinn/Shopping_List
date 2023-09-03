using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Shopping_List.Models
{
    public class CustomUser : IdentityUser
    {
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Display(Name = "Üyelik")]
        public string Role { get; set; }
    }
}
