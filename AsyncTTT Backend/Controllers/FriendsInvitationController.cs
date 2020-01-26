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
    public class FriendsInvitationController : ControllerBase
    {

        //podajesz w headerze nick i dostajesz zaproszenia do siebie albo od siebie
        [HttpGet(Name = "GetInvit")]
        public IEnumerable<User> GetInvit()
        {

            var credentials = ControllerUtility.GetCredentials(Request.Headers);

            var sqlCommand = new SimpleSqlCommand<User>()
            {
                SqlCommand = "SELECT sender, c.id_cred, nickname from friends_invitations join players p on (sender =  id_player) join credentials c on (c.id_cred = p.id_cred) WHERE reciever = (SELECT id_player FROM credentials c join players p on(c.id_cred =  p.id_cred) WHERE nickname = @nick)",
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




        //podajesz w headerze nick, body opisane w api na trello
        [HttpPost]
        public void Post([FromBody] Invitation value)
        {
            var credentials = ControllerUtility.GetCredentials(Request.Headers);

            var sqlCommand = new SimpleSqlCommand<Credentials>()
            {
                SqlCommand = "INSERT INTO friends_invitations(sender, reciever) values((SELECT id_player FROM credentials c join players p on(c.id_cred =  p.id_cred) WHERE nickname = @nick),@rec)",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@nick", SqlDbType.VarChar)
                    {
                        Value = credentials.login
                    },
                    new SqlParameter("@rec", SqlDbType.Int)
                    {
                        Value = value.Reciever
                    }
                }
            };

            sqlCommand.Execute();
        }
        
        [HttpPut]
        public void Put([FromBody] Invitation value)
        {

            var credentials = ControllerUtility.GetCredentials(Request.Headers);

            var sqlCommand = new SimpleSqlCommand<Credentials>()
            {
                SqlCommand = "EXECUTE addFriendship @vPlayer1 = @nick, @vPlayer2 = @rec",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@nick", SqlDbType.VarChar)
                    {
                        Value = credentials.login
                    },
                    new SqlParameter("@rec", SqlDbType.Int)
                    {
                        Value = value.Reciever
                    }
                }
            };

            sqlCommand.Execute();
        }
    }
}
