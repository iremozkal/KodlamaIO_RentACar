using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = this._userService.GetAll();

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = this._userService.GetById(id);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(User user)
        {
            var result = this._userService.Add(user);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(User user)
        {
            var result = this._userService.Update(user);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(User user)
        {
            var result = this._userService.Delete(user);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }
    }
}
