// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using Microsoft.AspNetCore.Mvc;
using SharingService.Core.Services.Anchors;
using SharingService.Data.Model;
using SharingService.Web.Core.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnchorsController : ControllerBase
    {
        private readonly IAnchorService _anchorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnchorsController"/> class.
        /// </summary>
        /// <param name="anchorKeyCache">The anchor key cache.</param>
        public AnchorsController(IAnchorService anchorService)
        {
            _anchorService = anchorService;
        }

        // GET api/anchors/5
        [HttpGet("{anchorId}")]
        public async Task<ActionResult<AnchorResponse>> GetAsync(int anchorId)
        {
            var result = await _anchorService.GetAsync(anchorId);
            if (result != null)
            {
                return FromDataModel(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<AnchorResponse>>> GetAsync()
        {
            var result = await _anchorService.GetAllAsync();
            return result.Select(FromDataModel).ToList();
        }

        // POST api/anchors
        [HttpPost]
        public async Task<ActionResult<AnchorResponse>> PostAsync([FromBody] CreateAnchorRequest request)
        {
            // TODO: Handle model validation with filter
            var model = ToDataModel(request);

            var result = await _anchorService.SaveAsync(model);
            return FromDataModel(result);
        }

        private AnchorResponse FromDataModel(Anchor anchor)
        {
            return new AnchorResponse
            {
                Id = anchor.Id,
                Name = anchor.Name,
                Key = anchor.Key,
                Longitude = anchor.Longitude,
                Latitude = anchor.Latitude
            };
        }

        private Anchor ToDataModel(CreateAnchorRequest anchor)
        {
            // TODO: Change this with automapper
            return new Anchor
            {
                Name = anchor.Name,
                Key = anchor.Key,
                Longitude = anchor.Longitude,
                Latitude = anchor.Latitude
            };
        }
    }
}
