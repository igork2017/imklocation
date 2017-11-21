using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMKLocation.Models;
using IMKLocation.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IMKLocation.Tests.Integration.Service
{
    [TestClass]
    public class DataServiceIntegrationTests
    {
        [TestMethod]
        public void CreateUser1()
        {
            var service=new DataService();
            var user = service.CreateUser("Igor", "Kiselyov", "i@gmai.com", "password").Result;
            var user1 = service.GetUser("i@gmai.com", "password");
            Assert.IsNotNull(user);
            Assert.IsNotNull(user1);
            Assert.AreEqual(user.Id,user1.Id);

        }

        [TestMethod]
        public void AddUser()
        {
            var service = new DataService();
            var user1 = service.GetUsers();
            var user=service.CreateUser("Test", "Test2", "i2@gmai.com", "password2").Result;
            var newUser2 = service.GetUsers();
            Assert.AreEqual(user1.Count()+1, newUser2.Count());
        }

        [TestMethod]
        public async Task AddLocation()
        {
            var service =new DataService();
            var location = new Location();
            var userId = Guid.NewGuid();
            location.Lattitude = 1.ToString();
            location.Longitude = 2.ToString();
            location.UserId=userId;
            location.Date = DateTime.Now;
            await service.AddLocation(userId, location);
            var locations = service.GetUserLocations(userId, DateTime.Now);
            Assert.IsNotNull(locations);
            Assert.IsTrue(locations.Count()==1);
            Assert.IsTrue(locations[0].Lattitude==location.Lattitude);
        }
    }
}
