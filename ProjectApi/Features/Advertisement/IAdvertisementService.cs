namespace ProjectApi.Features.Advertisement
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectApi.Features.Advertisement.Models;

    public interface IAdvertisementService
    {
        Task<int> Create(
            string name,
            string description,
            decimal price,
            string userId,
            string[] images);

        Task<bool> Delete(int id, string userId);

        Task<bool> Update(
            int id,
            string name,
            string description,
            decimal price,
            string userId,
            int advertisementCategoryId);

        Task<IEnumerable<AdvertisementListingServiceModel>> ByUser(string userId);

        Task<AdvertisementDetailsServiceModel> Details(int id);
    }
}
