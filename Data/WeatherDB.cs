using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class WeatherDB : DbContext
    {
        public DbSet<Model> WeatherData { get; set; }
        private const string connectionString = @"Data Source=LENOVO-Y700\SQLEXPRESS;Initial Catalog=weather_AUS;Integrated Security=True";
        public WeatherDB() : base(connectionString)
        {
        }
    }
}
