﻿namespace ProjectApi.Features.Identity
{
    public interface IIdentityService
    {
        public string GenerateJwtToken(string userId, string userName, string secret);
    }
}
