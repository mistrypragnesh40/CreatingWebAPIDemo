using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CRUDOperationUsingWEBAPI.Data
{
    public class Users : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [MaxLength(50)]
        public string LastName { get; set; } = null!;
     

        [MaxLength(6)]
        public string Gender { get; set; } = null!;
        public string? Address { get; set; }
        public string? RefreshToken { get; set; }

    }
}
