namespace ProjectApi.Features.Advertisement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ProjectApi.Features.Advertisement.Models;
    using ProjectApi.Infrastructure;

    public class AdvertisementService : IAdvertisementService
    {
        private readonly ProjectDbContext _db;
        private readonly IImageSizeReducer _imageSizeReducer;

        public AdvertisementService(ProjectDbContext db, IImageSizeReducer imageSizeReducer)
        {
            _db = db;
            _imageSizeReducer = imageSizeReducer;
        }

        [HttpPost]
        public async Task<int> Create(
            string name,
            string description,
            decimal price,
            string userId,
            string[] images)
        {
            var advertisement = new Advertisement
            {
                Name = name,
                Description = description,
                Price = price,
                UserId = userId,
                CategoryId = 1,
            };

            await _db.AddAsync(advertisement);

            await _db.SaveChangesAsync();

            await _db.AddRangeAsync(images
                .Select(i => new AdvertisementImage
                {
                    AdvertisementId = advertisement.Id,
                    ImageString = i,
                    SmallImageString = _imageSizeReducer.GetReducedSizeImage(i, 150, 150),
                }));

            await _db.SaveChangesAsync();

            return advertisement.Id;
        }

        public async Task<bool> Delete(int id, string userId)
        {
            var advertisement = await GetByIdAndByUserId(id, userId);

            if (advertisement == null)
            {
                return false;
            }

            _db.Advertisements.Remove(advertisement);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(
            int id,
            string name,
            string description,
            decimal price,
            string userId,
            int advertisementCategoryId)
        {
            var advertisement = await GetByIdAndByUserId(id, userId);

            if (advertisement == null)
            {
                return false;
            }

            advertisement.Name = name;
            advertisement.Description = description;
            advertisement.Price = price;
            advertisement.CategoryId = advertisementCategoryId;
            advertisement.UserId = userId;

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<AdvertisementListingServiceModel>> ByUser(string userId)
            => await _db
                .Advertisements
                .Where(a => a.UserId == userId)
                .Select(a => new AdvertisementListingServiceModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Price = a.Price,
                    SmallImageString = a.AdvertisementImages.First().SmallImageString,
                })
                .ToListAsync();

        public async Task<AdvertisementDetailsServiceModel> Details(int id)
            => await _db
                .Advertisements
                .Where(a => a.Id == id)
                .Select(a => new AdvertisementDetailsServiceModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Price = a.Price,
                    CategoryId = a.CategoryId,
                    Images = a.AdvertisementImages
                        .Select(a => a.SmallImageString),
                })
                .FirstOrDefaultAsync();

        private async Task<Advertisement> GetByIdAndByUserId(int id, string userId)
            => await _db
                .Advertisements
                .Where(a => a.Id == id && a.UserId == userId)
                .FirstOrDefaultAsync();
    }
}
