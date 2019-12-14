using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvMeassureApi.Models
{
    public class PersonalMeasurement
    {
        public PersonalMeasurement(int uid, int pressure, int humidity, int temp)
        {
            UId = uid;
            Pressure = pressure;
            Humidity = humidity;
            Temp = temp;
            TimeStamp = DateTime.Now;
        }

        public PersonalMeasurement()
        {
        }

        public int? Id { get; set; }

        public int UId { get; set; }
        
        public int Pressure { get; set; }

        public int Humidity { get; set; }

        public int Temp { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
