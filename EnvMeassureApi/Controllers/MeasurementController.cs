using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EnvMeassureApi.Models;

namespace EnvMeassureApi.Controllers
{
    using System.Data.SqlClient;
    using System.Data.SqlTypes;

    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private static string conn = "Server=tcp:4sem.database.windows.net,1433;Initial Catalog=Measurements;Persist Security Info=False;User ID=Specter777;Password=Ts3gzRGDcJ7YFN7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    // GET: api/Measurement
    [HttpGet]
        public ActionResult<IEnumerable<Measurement>> GetAll()
        {
            string sql = "Select * from dbo.Measurements";

            var result = new List<Measurement>();
            using (var databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (var selectCommand = new SqlCommand(sql, databaseConnection))
                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Measurement measurement = new Measurement();
                            {
                                measurement.Id = reader.GetInt32(0);
                                measurement.Pressure = reader.GetInt32(1);
                                measurement.Humidity = reader.GetInt32(2);
                                measurement.Temp = reader.GetInt32(3);
                                measurement.TimeStamp = reader.GetSqlDateTime(4).Value;
                            }

                            result.Add(measurement);
                        }
                    }
                }
                return this.Ok(result);
            }
        }

        // GET: api/Measurement/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Measurement> GetById(int id)
        {
            string sql = $"Select * from dbo.Measurement Where Id={id}";

            var meassurement = new Measurement();
            using (var databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (var selectCommand = new SqlCommand(sql, databaseConnection))
                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            meassurement.Id = reader.GetInt32(0);
                            meassurement.Pressure = reader.GetInt32(1);
                            meassurement.Humidity = reader.GetInt32(2);
                            meassurement.Temp = reader.GetInt32(3);
                            meassurement.TimeStamp = reader.GetSqlDateTime(4).Value;
                            
                        }
                    }
                }
                if (meassurement.Id == null)
                {
                    return NotFound();
                }
                return this.Ok(meassurement);
            }
        }

        // POST: api/Measurement
        [HttpPost]
        public ActionResult Add([FromBody] Measurement value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string sql = $"Insert INTO dbo.Measurement (Pressure, Humidity, Temp, TimeStamp) values ({value.Pressure}, {value.Humidity}, {value.Temp}, '{value.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss")}')";
            using (var databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (var insertCommand = new SqlCommand(sql, databaseConnection))
                {
                    insertCommand.ExecuteNonQuery();
                }
            }

            return this.Ok(value);
        }

        // PUT: api/Measurement/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            int rowsaffected;
            string sql = $"DELETE FROM dbo.Measurement WHERE Id={id};";
            using (var databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (var insertCommand = new SqlCommand(sql, databaseConnection))
                {
                    insertCommand.CommandType = System.Data.CommandType.Text;

                    rowsaffected = insertCommand.ExecuteNonQuery();

                    Console.WriteLine($"$Insert : {rowsaffected}");
                }
            }
            return Ok(rowsaffected);
        }
    }
}
