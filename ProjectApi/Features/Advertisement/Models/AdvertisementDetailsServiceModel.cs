namespace ProjectApi.Features.Advertisement.Models
{
    using System.Collections.Generic;

    public class AdvertisementDetailsServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        
        public IEnumerable<string> Images { get; set; }
    }
}
