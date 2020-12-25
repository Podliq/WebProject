using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Features.Identity
{
    public class UserDetailsResponse
    {
        public string ProfilePicture { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
