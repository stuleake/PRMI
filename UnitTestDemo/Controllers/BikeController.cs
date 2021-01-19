using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTestDemo.Dto;
using UnitTestDemo.Services;

namespace UnitTestDemo.Controllers
{
    public class BikeController : Controller
    {
        private readonly IBikeService bikeService;

        public BikeController(IBikeService bikeService)
        {
            this.bikeService = bikeService;
        }

        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var model = this.bikeService.GetById(id);

            if (model == null)
            {
                return BadRequest();
            }

            return Ok(model);
        }

        [HttpGet]
        public async Task<List<BikeDto>> GetAsync()
        {
            return await this.bikeService.GetAsync();
        }

        [HttpGet]
        public async Task<List<BikeDto>> GetByWhere()
        {
            return await this.bikeService.GetByWhere(bike => bike.Id == Guid.NewGuid());
        }
    }
}
