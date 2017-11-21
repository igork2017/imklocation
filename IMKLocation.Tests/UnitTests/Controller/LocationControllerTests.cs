using System;
using IMKLocation.Controllers;
using IMKLocation.Models;
using IMKLocation.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Web.Http;
using NSubstitute.Extensions;

namespace IMKLocation.Tests.UnitTests.Controller
{
    [TestClass]
    public class LocationControllerTests
    {
        private IDataService _dataService;
        private LocationController _controller;
        [TestInitialize]
        public void Init()
        {
            _dataService = Substitute.For<IDataService>();
            _controller =new LocationController(_dataService);
            
        }

        [TestMethod]
        public void GetUser_Returns()
        {
            var user = new User();
            _dataService.GetUser("igor", "password").ReturnsForAnyArgs(user);

            var response = _controller.GetUser("igor", "passowrd") as System.Web.Http.Results.OkNegotiatedContentResult<Models.User>;

            Assert.AreEqual(response.Content,user);
        }
    }
}
