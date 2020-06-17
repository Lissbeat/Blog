using Microsoft.AspNetCore.Identity;

namespace assignment_4.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Nickname { set; get;  }
        
        public int Age { set; get; }
    }
}