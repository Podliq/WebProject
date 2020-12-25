namespace ProjectApi.Data.Models
{
    using System.Collections.Generic;

    public class AdvertisementCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Advertisement> Advertisements { get; } = new HashSet<Advertisement>();
    }
}
