namespace ProjectApi.Features.Advertisement.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.Validation.Advertisement;

    public class CreateAdvertisementRequestModel
    {
        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string[] Images { get; set; }
    }
}
