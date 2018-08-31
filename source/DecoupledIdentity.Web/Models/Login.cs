using System.ComponentModel.DataAnnotations;

namespace DecoupledIdentity.Web.Models
{
    public class Login
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
