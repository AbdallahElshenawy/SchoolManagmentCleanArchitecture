using Microsoft.AspNetCore.Identity;

namespace SchoolManagment.Data.Entities.Identity
{
    public class User : IdentityUser
    {
        public string Address { get; set; }
    }
}
