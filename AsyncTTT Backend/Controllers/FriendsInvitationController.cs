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


        [HttpGet("{id}", Name = "GetInvit")]
        public IEnumerable<FirendsInvitation> GetInvit(int id)
        {
            var sqlCommand = new SimpleSqlCommand<FirendsInvitation>()
            {
                SqlCommand = "SELECT * from invitations WHERE sender = @id OR reciever = @id",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int)
                    {
                        Value = id
                    }
                },
                ModelExtractor = reader => new FirendsInvitation
                {
                    Id = (int)reader[0],
                    Sender = (int)reader[1],
                    Reciever = (int)reader[2]
               }
            };

            return sqlCommand.Execute();
        }





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
    }
}
