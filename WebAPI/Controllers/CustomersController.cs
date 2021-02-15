using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/<controller>
        [System.Web.Mvc.HttpGet]
        public IHttpActionResult Get()
        {
            var result = this._customerService.GetAll();

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // GET api/<controller>/id
        [System.Web.Mvc.HttpGet]
        public IHttpActionResult Get(int id)
        {
            var result = this._customerService.GetById(id);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // POST(add) api/<controller>
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Post(Customer customer)
        {
            var result = this._customerService.Add(customer);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // PUT(update) api/<controller>
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Put(Customer customer)
        {
            var result = this._customerService.Update(customer);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // DELETE api/<controller>
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Delete(Customer customer)
        {
            var result = this._customerService.Delete(customer);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }
    }
}