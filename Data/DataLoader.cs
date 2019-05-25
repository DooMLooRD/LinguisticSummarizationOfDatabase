using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataLoader
    {
        public List<Model> LoadData()
        {
            using (WeatherDB weatherContext = new WeatherDB())
            {
                IQueryable<Model> dataSets = weatherContext.WeatherData;
                return dataSets.ToList();
            }
        }
    }
}
