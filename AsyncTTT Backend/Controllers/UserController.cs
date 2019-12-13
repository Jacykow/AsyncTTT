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
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var sqlCommand = new SimpleSqlCommand<User>()
            {
                SqlCommand = "SELECT * FROM Users WHERE id > @id",
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

        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("credentials", Name = "Get")]
        public string CheckCredentials()
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] User value)
        {
        }
    }
}
