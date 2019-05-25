using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Table("weather")]
    public class Model
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public double? MinTemp { get; set; }
        public double? MaxTemp { get; set; }
        public double? Rainfall { get; set; }
        public double? Evaporation { get; set; }
        public double? Sunshine { get; set; }
        public string WindGustDir { get; set; }
        public int? WindGustSpeed { get; set; }
        public string WindDir9am { get; set; }
        public string WindDir3pm { get; set; }
        public int? WindSpeed9am { get; set; }
        public int? WindSpeed3pm { get; set; }
        public int? Humidity9am { get; set; }
        public int? Humidity3pm { get; set; }
        public double? Pressure9am { get; set; }
        public double? Pressure3pm { get; set; }
        public int? Cloud9am { get; set; }
        public int? Cloud3pm { get; set; }
        public double? Temp9am { get; set; }
        public double? Temp3pm { get; set; }
        public string RainToday { get; set; }
        public double? Risk_mm { get; set; }
        public string RainTomorrow { get; set; }
    }
}
