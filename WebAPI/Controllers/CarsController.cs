using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = this._carService.GetAll();

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpGet("getalldetails")]
        public IActionResult GetAllCarDetails()
        {
            var result = this._carService.GetAllCarDetails();

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpGet("getallbybrand")]
        public IActionResult GetAllByBrandId(int brandId)
        {
            var result = _carService.GetAllCarsByBrandId(brandId);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpGet("getallbycolor")]
        public IActionResult GetAllByColorId(int colorId)
        {
            var result = _carService.GetAllCarsByColorId(colorId);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _carService.GetById(id);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpGet("getdetailbyid")]
        public IActionResult GetDetailById(int id)
        {
            var result = _carService.GetAllCarDetails(c=>c.Id == id);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(Car car)
        {
            var result = _carService.Add(car);

            if (result.Success) return Ok(result);
            else return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Car car)
        {
            var result = _carService.Update(car);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Car car)
        {
            var result = _carService.Delete(car);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }
    }
}
