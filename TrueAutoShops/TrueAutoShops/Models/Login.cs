using PropertyChanged;

namespace TrueAutoShops.Models
{
  
    public class Login : BaseNotificationHandler
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
