// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using Microsoft.AspNetCore.Mvc;
using SharingService.Core.Services.Token;
using System.Threading.Tasks;

namespace SharingService.Web.Core.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppTokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AppTokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // GET api/apptoken
        [HttpGet]
        public Task<string> GetAsync()
        {
            return _tokenService.RequestToken();
        }
    }
}
