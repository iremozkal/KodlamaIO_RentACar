using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = this._colorService.GetAll();

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = this._colorService.GetById(id);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("Add")]
        public IActionResult Add(Color color)
        {
            var result = this._colorService.Add(color);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(Color color)
        {
            var result = this._colorService.Update(color);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Color color)
        {
            var result = this._colorService.Delete(color);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }
    }
}
