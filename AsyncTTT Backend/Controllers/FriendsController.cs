using AsyncTTT_Backend.Models;
using AsyncTTT_Backend.Models.Internal;
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
    public class FriendsController : ControllerBase
    {

        //podajesz w headerze credentiale i dostajesz swoich znajomych
        [HttpGet(Name = "GetFriends")]
        public IEnumerable<User> GetFriends()
        {
            var credentials = ControllerUtility.GetCredentials(Request.Headers);

            var sqlCommand = new SimpleSqlCommand<User>()
            {
                SqlCommand = "SELECT id_cred, id_player FROM friendships JOIN players on(player2 = id_player) WHERE player1 = ((SELECT id_player FROM credentials c join players p on(c.id_cred =  p.id_cred) WHERE nickname = @nick))",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@nick", SqlDbType.VarChar)
                    {
                        Value = credentials.login
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
        public void Post([FromBody] Invitation value)
        {

        }
    }
}
