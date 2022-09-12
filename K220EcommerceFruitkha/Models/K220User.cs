using Microsoft.AspNetCore.Identity;

namespace K220EcommerceFruitkha.Models
{
    public class K220User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
