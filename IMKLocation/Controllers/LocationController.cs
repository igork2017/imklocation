using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IMKLocation.Models;
using IMKLocation.Services;

namespace IMKLocation.Controllers
{
    public class LocationController : ApiController
    {
        private readonly IDataService _dataService;

        public LocationController(IDataService dataservice)
        {
            _dataService = dataservice;
        }
        public IHttpActionResult GetUserLocation(Guid userId, DateTime date)
        {
            var location = _dataService.GetUserLocations(userId, date);
            if (location == null)
                return NotFound();
            return Ok(location);
        }
        public IHttpActionResult GetUser(string username, string password)
        {
            var user = _dataService.GetUser(username,password);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        public IHttpActionResult CreateUser(string name, string surname, string email, string password)
        {
            var userId=_dataService.CreateUser(name, surname, email, password);
            return Ok(userId);
        }
        public IHttpActionResult AddLocation(Guid userId, Location location)
        {
            _dataService.AddLocation(userId,location);
            return Ok();
        }
    }
}
