using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = this._customerService.GetAll();

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpGet("getalldetails")]
        public IActionResult GetAllCustomerDetails()
        {
            var result = this._customerService.GetAllCustomerDetails();

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = this._customerService.GetById(id);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(Customer customer)
        {
            var result = this._customerService.Add(customer);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(Customer customer)
        {
            var result = this._customerService.Update(customer);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Customer customer)
        {
            var result = this._customerService.Delete(customer);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }
    }
}
