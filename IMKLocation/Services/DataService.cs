using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using IMKLocation.Models;

namespace IMKLocation.Services
{
    public class DataService: IDataService
    {
        private const string userFilePath = "User.txt";
        private const string userLocationPath = "Location.txt";
        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        private string rootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"))+"IMKLocation\\";
        public async Task AddLocation(Guid userId, Location location)
        {
            var year = DateTime.Today.Year;
            var month = DateTime.Today.Month;
            var day = DateTime.Today.Day;
            var date = $"{year}{month}{day}";
            var userLocationFileName = $"{userId}_{date}_{userLocationPath}";
            var fullPath = rootPath + "Data\\" + userLocationFileName;
            var l = serializer.Serialize(location);
            if (!(File.Exists(fullPath)))
            {
                using (var writer = File.CreateText(fullPath))
                {
                    await writer.WriteLineAsync(l);
                    await writer.FlushAsync();
                }
            }
            else
            {
                using (var file = File.Open(fullPath, FileMode.Append, FileAccess.Write))
                using (var writer = new StreamWriter(file))
                {
                    await writer.WriteLineAsync(l);
                    await writer.FlushAsync();
                }
            }
        }

        public List<Location> GetUserLocations(Guid userId, DateTime date)
        {
            var locations = new List<Location>();
            var year = date.Year;
            var month =date.Month;
            var day = date.Day;
            var d = $"{year}{month}{day}";
            var userLocationFileName = $"{userId}_{d}_{userLocationPath}";
            var fullPath = rootPath + "Data\\" + userLocationFileName;

            if (File.Exists(fullPath))
            {
                string[] lines = System.IO.File.ReadAllLines(fullPath);
                foreach (string line in lines)
                {
                    var l = serializer.Deserialize<Location>(line);
                    locations.Add(l);
                }
            }
            return locations;
        }

        public List<User> GetUsers()
        {
            var users = new List<User>();
            var fullPath = rootPath + "Data\\" + userFilePath;
           
            if (File.Exists(fullPath))
            {
                string[] lines = System.IO.File.ReadAllLines(fullPath);
                foreach (string line in lines)
                {
                    var u = serializer.Deserialize<User>(line);
                    users.Add(u);
                }
            }
            return users;
        }
        public async Task<User> CreateUser(string name, string surname, string email, string password)
        {
            var us = GetUser(email, password);
            if (us != null)
                return us;

            var user = new User();
            user.Name = name;
            user.Surname = surname;
            user.Email = email;
            user.Password = password;
            user.Id = Guid.NewGuid();

            var fullPath = rootPath + "Data\\" + userFilePath;
            var userString = serializer.Serialize(user);
            if (!(File.Exists(fullPath)))
            {
                File.CreateText(fullPath);
            }
            using (var file = File.Open(fullPath, FileMode.Append, FileAccess.Write))
            using (var writer = new StreamWriter(file))
            {
                await writer.WriteLineAsync(userString);
                await writer.FlushAsync();
            }
            return user;
        }

        public User GetUser(string username, string password)
        {
            var fullPath = rootPath + "Data\\" + userFilePath;
            if (File.Exists(fullPath))
            {
                string[] lines = System.IO.File.ReadAllLines(fullPath);
                foreach (string line in lines)
                {
                    var u = serializer.Deserialize<User>(line);
                    if (u.Email == username && u.Password == password)
                    {
                        return u;
                    }
                }
            }
            return null;
        }
    }

    public interface IDataService
    {
        Task AddLocation(Guid userId, Location location);
        List<Location> GetUserLocations(Guid userId, DateTime date);
        User GetUser(string username,string password);
        List<User> GetUsers();
        Task<User> CreateUser(string name, string surname, string email, string password);

    }
}