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
    public class CredentialsController : ControllerBase
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
        [HttpGet("{nick}/{pass}", Name = "GetCred")]
        public bool GetCred(string nick, string pass)
        {

            var sqlCommand = new SimpleSqlCommand<DefaultResponse>()
            {
                SqlCommand = "SELECT COUNT(*) FROM players p JOIN credentials c ON (p.id_cred = c.id_cred) WHERE nickname = @nick AND pass = @pass",
                //SqlCommand = "SELECT COUNT(*) FROM credentials",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@nick", SqlDbType.VarChar)
                    {
                        Value = nick
                    },
                    new SqlParameter("@pass", SqlDbType.VarChar)
                    {
                        Value = pass
                    }
                },
                ModelExtractor = reader => new DefaultResponse
                {
                    Success = false,
                    Message = reader[0].ToString()
                }
            };
            //List<int> result = sqlCommand.Execute();

            List<DefaultResponse> response_list = sqlCommand.Execute();
            //return result[0];
            if (int.Parse(response_list[0].Message) > 0)
                return true;

            return false;


        }


        [HttpPost]
        public void Post([FromBody] Credentials value)
        {
            var credentials = ControllerUtility.GetCredentials(Request.Headers);

            var sqlCommand = new SimpleSqlCommand<Credentials>()
            {
                SqlCommand = "EXECUTE addPlayer @vNickname = @nick, @vPass = @pass",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@nick", SqlDbType.VarChar)
                    {
                        Value = credentials.login
                    },
                    new SqlParameter("@pass", SqlDbType.VarChar)
                    {
                        Value = credentials.password
                    }
                }
            };

            sqlCommand.Execute();
        }
    }
}
