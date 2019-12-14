using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvMeassureApi.Models
{
    public class Measurement
    {
        public Measurement(int pressure, int humidity, int temp)
        {
            Pressure = pressure;
            Humidity = humidity;
            Temp = temp;
            TimeStamp = DateTime.Now;
        }

        public Measurement()
        {
        }

        public int? Id { get; set; }

        public int Pressure { get; set; }

        public int Humidity { get; set; }

        public int Temp { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
