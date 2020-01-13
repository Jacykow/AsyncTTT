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
    public class BoardIdController: ControllerBase
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
        [HttpGet("{id}", Name = "GetBoardId")]
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
                    Id = (int)reader[0],
                    Id_cred = (int)reader[1]
                }
            };

            return sqlCommand.Execute();
        }


        [HttpPost]
        public void Post([FromBody] Move value)
        {
            var credentials = ControllerUtility.GetCredentials(Request.Headers);

            var sqlCommand = new SimpleSqlCommand<User>()
            {
                SqlCommand = "EXECUTE addMove @mNick = @nick, @mX_coord = @xcoord, @mY_coord = @ycoord, @mId_game = @idgame",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@idgame", SqlDbType.Int)
                    {
                        Value = value.IdGame
                    },
                    new SqlParameter("@xcoord", SqlDbType.Int)
                    {
                        Value = value.XCoord
                    },
                    new SqlParameter("@ycoord", SqlDbType.Int)
                    {
                        Value = value.YCoord
                    },
                    new SqlParameter("@nick", SqlDbType.VarChar)
                    {
                        Value = credentials.login
                    }
                }
            };

            sqlCommand.Execute();
        }
    }
}
