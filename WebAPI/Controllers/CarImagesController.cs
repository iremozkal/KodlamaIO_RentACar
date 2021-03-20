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
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();

            if (result.Success) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _carImageService.GetById(id);

            if (result.Success) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpGet("getimagesbycarid")]
        public IActionResult GetImagesByCarId(int carId)
        {
            var result = _carImageService.GetAllCarImagesByCarId(carId);

            if (result.Success) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("image"))] IFormFile file, [FromForm] CarImage carImage)
        {
            var result = _carImageService.Add_WebAPI(file, carImage);

            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm(Name = ("image"))] IFormFile file, [FromForm(Name = ("id"))] int id)
        {
            if (_carImageService.IsExistById(id).Success) { 
                var carImage = _carImageService.GetById(id).Data;

                var result = _carImageService.Update_WebAPI(file, carImage);

                if (result.Success) return Ok(result);
                return BadRequest(result);
            }
            return BadRequest("Invalid Id");
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromForm(Name = ("id"))] int id)
        {
            var carImage = _carImageService.GetById(id).Data;

            var result = _carImageService.Delete(carImage);

            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}

