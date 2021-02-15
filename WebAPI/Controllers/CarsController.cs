using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class CarsController : ApiController
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        // GET api/<controller>
        [System.Web.Mvc.HttpGet]
        public IHttpActionResult Get()
        {
            var result = this._carService.GetAll();

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // GET api/<controller>/id
        [System.Web.Mvc.HttpGet]
        public IHttpActionResult Get(int id)
        {
            var result = this._carService.GetById(id);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // POST(add) api/<controller>
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Post(Car car)
        {
            var result = this._carService.Add(car);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // PUT(update) api/<controller>
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Put(Car car)
        {
            var result = this._carService.Update(car);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // DELETE api/<controller>
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Delete(Car car)
        {
            var result = this._carService.Delete(car);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }
    }
}

/*  Alternative : HttpResponseMessage 
*   return Request.CreateResponse(HttpStatusCode.OK, result.Data);
*   return Request.CreateResponse(HttpStatusCode.BadRequest, result.Message);
*/