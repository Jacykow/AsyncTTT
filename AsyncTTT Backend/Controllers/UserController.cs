using AsyncTTT_Backend.Models;
using AsyncTTT_Backend.SQL;
using AsyncTTT_Backend.Utility;
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
        public DefaultResponse Get()
        {
            var credentials = ControllerUtility.GetCredentials(Request.Headers);
            return new DefaultResponse
            {
                Success = true,
                Message = $"<{credentials.login}> <{credentials.password}>"
            };
        }

        // To jest testowe i do wywalenia
        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<User> Get(int id)
        {
            var sqlCommand = new SimpleSqlCommand<User>()
            {
                SqlCommand = "SELECT * FROM Players WHERE id_player > @id",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int)
                    {
                        Value = id
                    }
                },
                ModelExtractor = reader => new User
                { 
                    //user jest na odwrót w bazie i nie mogę tego zmienić bez rozwalania bazy dlatego tu jest 1 i 0, a nie 0 i 1
                    Id = (int)reader[1],
                    Id_cred = (int)reader[0]
                }
            };

            return sqlCommand.Execute();
        }


        [HttpPost]
        public void Post([FromBody] User value)
        {

        }
    }
}
