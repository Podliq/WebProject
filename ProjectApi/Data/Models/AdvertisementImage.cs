namespace ProjectApi.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AdvertisementImage
    {
        public int Id { get; set; }

        [Required]
        public int AdvertisementId { get; set; }

        [Required]
        public string ImageString { get; set; }

        [Required]
        public string SmallImageString { get; set; }

        public Advertisement Advertisement { get; set; }
    }
}
