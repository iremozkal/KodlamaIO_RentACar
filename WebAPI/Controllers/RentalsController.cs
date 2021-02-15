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
    public class RentalsController : ApiController
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        // GET api/<controller>
        [System.Web.Mvc.HttpGet]
        public IHttpActionResult Get()
        {
            var result = this._rentalService.GetAll();

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // GET api/<controller>/id
        [System.Web.Mvc.HttpGet]
        public IHttpActionResult Get(int id)
        {
            var result = this._rentalService.GetById(id);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // POST(add) api/<controller>
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Post(Rental rental)
        {
            var result = this._rentalService.Add(rental);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // PUT(update) api/<controller>
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Put(Rental rental)
        {
            var result = this._rentalService.Update(rental);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // DELETE api/<controller>
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Delete(Rental rental)
        {
            var result = this._rentalService.Delete(rental);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }
    }
}