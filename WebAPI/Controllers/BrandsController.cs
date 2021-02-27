using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = this._brandService.GetAll();

            if (result.Success) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = this._brandService.GetById(id);

            if (result.Success) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(Brand brand)
        {
            var result = this._brandService.Add(brand);

            if (result.Success) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(Brand brand)
        {
            var result = this._brandService.Update(brand);

            if (result.Success) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Brand brand)
        {
            var result = this._brandService.Delete(brand);

            if (result.Success) return Ok(result);
            else return BadRequest(result.Message);
        }
    }
}
