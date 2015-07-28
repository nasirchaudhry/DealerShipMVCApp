using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DealerShipMVCApp.Models
{
    public class Vehicle
    {
        public int ID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public double MilePerGallon { get; set; }
        public decimal MSRP { get; set; }
    }

    public class VehicleDBContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}