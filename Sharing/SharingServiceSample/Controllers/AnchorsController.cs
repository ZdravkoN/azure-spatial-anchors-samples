// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using Microsoft.AspNetCore.Mvc;
using SharingService.Core.Services.Anchors;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SharingService.Controllers
{
    [Route("api/anchors")]
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
        [HttpGet("{anchorNumber}")]
        public async Task<ActionResult<string>> GetAsync(long anchorNumber)
        {
            // Get the key if present
            try
            {
                return await _anchorService.GetAnchorKeyAsync(anchorNumber);
            }
            catch(KeyNotFoundException)
            {
                return this.NotFound();
            }
        }

        // GET api/anchors/last
        [HttpGet("last")]
        public async Task<ActionResult<string>> GetAsync()
        {
            // Get the last anchor
            string anchorKey = await _anchorService.GetLastAnchorKeyAsync();

            if (anchorKey == null)
            {
                return "";
            }

            return anchorKey;
        }

        // POST api/anchors
        [HttpPost]
        public async Task<ActionResult<long>> PostAsync()
        {
            string anchorKey;
            using (StreamReader reader = new StreamReader(this.Request.Body, Encoding.UTF8))
            {
                anchorKey = await reader.ReadToEndAsync();
            }

            if (string.IsNullOrWhiteSpace(anchorKey))
            {
                return this.BadRequest();
            }

            // Set the key and return the anchor number
            return await _anchorService.SetAnchorKeyAsync(anchorKey);
        }
    }
}
