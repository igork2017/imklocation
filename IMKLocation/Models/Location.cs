using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMKLocation.Models
{
    public class Location: ILocation
    {
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
    }
}