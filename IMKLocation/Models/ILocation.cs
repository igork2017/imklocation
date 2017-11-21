using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMKLocation.Models
{
    public interface ILocation
    {
        string Lattitude { get; set; }
        string Longitude { get; set; }
        Guid UserId { get; set; }
        DateTime Date { get; set; }
    }
}
