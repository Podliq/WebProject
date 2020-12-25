namespace ProjectApi.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string ProfilePicture { get; set; }

        public IEnumerable<Advertisement> Advertisements { get; } = new HashSet<Advertisement>();
    }
}
