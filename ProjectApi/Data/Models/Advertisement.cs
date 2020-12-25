namespace ProjectApi.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Validation.Advertisement;

    public class Advertisement
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public int Status { get; set; } = 1;

        public User User { get; set; }

        public AdvertisementCategory AdvertisementCategory { get; set; }

        public IEnumerable<AdvertisementImage> AdvertisementImages { get; } = new HashSet<AdvertisementImage>();
    }
}
