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
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/<controller>
        [System.Web.Mvc.HttpGet]
        public IHttpActionResult Get()
        {
            var result = this._userService.GetAll();

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // GET api/<controller>/id
        [System.Web.Mvc.HttpGet]
        public IHttpActionResult Get(int id)
        {
            var result = this._userService.GetById(id);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // POST(add) api/<controller>
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Post(User user)
        {
            var result = this._userService.Add(user);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // PUT(update) api/<controller>
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Put(User user)
        {
            var result = this._userService.Update(user);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }

        // DELETE api/<controller>
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Delete(User user)
        {
            var result = this._userService.Delete(user);

            if (result.Success == true) return Ok(result);
            else return BadRequest(result.Message);
        }
    }
}