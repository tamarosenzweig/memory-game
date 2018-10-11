using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class User
    {
        [Required]
        [MinLength(2, ErrorMessage = "user name must be at least 2 characters")]
        [MaxLength(10, ErrorMessage = "user name can be maximum 10 characters")]
        public string UserName { get; set; }
        [Range(18, 120)]
        public int Age { get; set; }
        public string PartnerUserName { get; set; }
        public int Score { get; set; }
    }
}