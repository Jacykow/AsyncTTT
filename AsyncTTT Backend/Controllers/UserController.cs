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

        //zwraca id i id_credentiali usera po podaniu nicku ; sluzy do konwersji nick na id
        [HttpGet(Name = "GetId")]
        public IEnumerable<User> Get()
        {
            var credentials = ControllerUtility.GetCredentials(Request.Headers);

            var sqlCommand = new SimpleSqlCommand<User>()
            {
                SqlCommand = "SELECT id_player, c.id_cred, nickname FROM players p join credentials c on(c.id_cred = p.id_cred) WHERE nickname = @nick",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@nick", SqlDbType.VarChar)
                    {
                        Value = credentials.login
                    }
                },
                ModelExtractor = reader => new User
                {
                    Id = (int)reader[0],
                    Id_cred = (int)reader[1],
                    nickname = (string)reader[2]
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
