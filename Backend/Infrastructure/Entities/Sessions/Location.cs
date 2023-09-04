using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities.Sessions
{
    [Owned]
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
