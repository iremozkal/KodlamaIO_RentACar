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
    public class ColorsController : ApiController
    {
        private readonly IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        // GET api/<controller>
        [System.Web.Mvc.HttpGet]
        public IHttpActionResult Get()
        {
            var result = this._colorService.GetAll();

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // GET api/<controller>/id
        [System.Web.Mvc.HttpGet]
        public IHttpActionResult Get(int id)
        {
            var result = this._colorService.GetById(id);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // POST(add) api/<controller>
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Post(Color color)
        {
            var result = this._colorService.Add(color);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // PUT(update) api/<controller>
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Put(Color color)
        {
            var result = this._colorService.Update(color);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // DELETE api/<controller>
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Delete(Color color)
        {
            var result = this._colorService.Delete(color);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }
    }
}