using AsyncTTT_Backend.Models;
using AsyncTTT_Backend.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace AsyncTTT_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var sqlCommand = new SimpleSqlCommand<User>()
            {
                SqlCommand = "SELECT * FROM Persons WHERE id > @id",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int)
                    {
                        Value = 1
                    }
                },
                ModelExtractor = reader => new User
                {
                    Id = (int)reader[0],
                    Name = (string)reader[1]
                }
            };

            return sqlCommand.Execute();
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] User value)
        {
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
