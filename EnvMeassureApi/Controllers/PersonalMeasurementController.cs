using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EnvMeassureApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvMeassureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalMeasurementController : ControllerBase
    {
        private static string conn = "Server=tcp:4sem.database.windows.net,1433;Initial Catalog=Measurements;Persist Security Info=False;User ID=Specter777;Password=Ts3gzRGDcJ7YFN7;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // GET: api/PersonalMeasurement
        [HttpGet]
        public ActionResult<IEnumerable<PersonalMeasurement>> GetAll()
        {
            string sql = "Select * from dbo.PersonalMeasurements";

            var result = new List<PersonalMeasurement>();
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
                            PersonalMeasurement personalMeasurement = new PersonalMeasurement();
                            {
                                personalMeasurement.Id = reader.GetInt32(0);
                                personalMeasurement.UId = reader.GetInt32(1);
                                personalMeasurement.Pressure = reader.GetInt32(2);
                                personalMeasurement.Humidity = reader.GetInt32(3);
                                personalMeasurement.Temp = reader.GetInt32(4);
                                personalMeasurement.TimeStamp = reader.GetSqlDateTime(5).Value;
                            }

                            result.Add(personalMeasurement);
                        }
                    }
                }
                return this.Ok(result);
            }
        }

        // GET: api/PersonalMeasurement/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<IEnumerable<PersonalMeasurement>> GetById(string id)
        {
            string sql = $"Select * from dbo.PersonalMeasurements Where UID={id}";

            var result = new List<PersonalMeasurement>();
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
                            PersonalMeasurement personalMeasurement = new PersonalMeasurement();
                            {
                                personalMeasurement.Id = reader.GetInt32(0);
                                personalMeasurement.UId = reader.GetInt32(1);
                                personalMeasurement.Pressure = reader.GetInt32(2);
                                personalMeasurement.Humidity = reader.GetInt32(3);
                                personalMeasurement.Temp = reader.GetInt32(4);
                                personalMeasurement.TimeStamp = reader.GetSqlDateTime(5).Value;
                            }

                            result.Add(personalMeasurement);
                        }
                    }
                }

                if (result.Count < 1)
                {
                    return this.NotFound();
                }
                return this.Ok(result);
            }

        }

        // POST: api/PersonalMeasurement
        [HttpPost]
        public ActionResult Add([FromBody] PersonalMeasurement value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string timeStamp = value.TimeStamp.ToString("yyyy-MM-dd hh:mm:ss").Replace(".", ":");
            string sql = $"Insert INTO dbo.PersonalMeasurements (UID, Pressure, Humidity, Temp, TimeStamp) values ({value.UId}, {value.Pressure}, {value.Humidity}, {value.Temp}, '{timeStamp}')";
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

        // PUT: api/PersonalMeasurement/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            int rowsaffected;
            string sql = $"DELETE FROM dbo.PersonalMeasurements WHERE Id={id};";
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
