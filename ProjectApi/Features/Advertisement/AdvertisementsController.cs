namespace ProjectApi.Features.Advertisement
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using ProjectApi.Features.Advertisement.Models;
    using ProjectApi.Infrastructure.Extensions;

    using static Infrastructure.WebConstants;

    public class AdvertisementsController : ApiController
    {
        private readonly IAdvertisementService _advertisementService;

        public AdvertisementsController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpGet]
        public async Task<IEnumerable<AdvertisementListingServiceModel>> Mine()
        {
            var userId = User.GetId();

            return await _advertisementService.ByUser(userId);
        }

        [HttpGet]
        [Route(Id)]
        public async Task<AdvertisementDetailsServiceModel> Details(int id)
            => await _advertisementService.Details(id);

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(CreateAdvertisementRequestModel model)
        {
            var userId = User.GetId();

            var advertisementId = await _advertisementService.Create(
                model.Name,
                model.Description,
                model.Price,
                userId,
                model.Images);

            return Created(nameof(Create), advertisementId);
        }

        [HttpPut]
        [Route(Id)]
        public async Task<ActionResult> Update()
        {
            //userid
            //update: bool
            //!update badrequest
            //ok
            return null;
        }

        [HttpDelete]
        [Route(Id)]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = User.GetId();

            var isDeleted = await _advertisementService.Delete(id, userId);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
