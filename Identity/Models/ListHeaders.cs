using System.ComponentModel.DataAnnotations;

namespace Shopping_List.Models
{
    public class ListHeaders
    {
        [Key]
        public int HeaderId { get; set; }
        public string UserId { get; set; }
    }
}
