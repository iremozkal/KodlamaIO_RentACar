using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = this._rentalService.GetAll();

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }


        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = this._rentalService.GetById(id);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(Rental rental)
        {
            var result = this._rentalService.Add(rental);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(Rental rental)
        {
            var result = this._rentalService.Update(rental);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Rental rental)
        {
            var result = this._rentalService.Delete(rental);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }
    }
}
