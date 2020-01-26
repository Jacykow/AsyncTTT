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
    public class GameController : ControllerBase
    {

        //zaproszenia do gier od kogoś. podajesz nick i zwraca Ci zaproszenia do gier, ktorych jestes odbiorca
        [HttpGet(Name = "GetGameInvit")]
        public IEnumerable<User> GetGameInvit()
        {

            var credentials = ControllerUtility.GetCredentials(Request.Headers);

            var sqlCommand = new SimpleSqlCommand<User>()
            {
                SqlCommand = "SELECT sender, c.id_cred, nickname from games_invitations join players p on (sender =  id_player) join credentials c on (c.id_cred = p.id_cred) WHERE reciever = (SELECT id_player FROM credentials c join players p on(c.id_cred =  p.id_cred) WHERE nickname = @nick)",
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


      /*  //podajesz nick i sie robi gra bez drugiego playera
        [HttpPost]
        public void Post()
        {
            var credentials = ControllerUtility.GetCredentials(Request.Headers);

            var sqlCommand = new SimpleSqlCommand<Credentials>()
            {
                SqlCommand = "EXECUTE addGame @vPlayer1_game = @nick, @vId_version = 1",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@nick", SqlDbType.VarChar)
                    {
                        Value = credentials.login
                    }
                }
            };

            sqlCommand.Execute();
        }*/
        

        //podajesz id gry i jsona o strukturze usera (dowolne wartosci oprocz Id) i gra posiada drugiego gracza (co oznacza, ze zaakceptowal zaproszenie)
        [HttpPut]
        public void Put([FromBody] GameAccept value)
        {

            var sqlCommand = new SimpleSqlCommand<Credentials>()
            {
                SqlCommand = "EXECUTE addGame @vPlayer1 = @id1, @vPlayer2 = @id2, @vId_version = 1",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@id1", SqlDbType.Int)
                    {
                        Value = value.IdPlayer1
                    },
                    new SqlParameter("@id2", SqlDbType.Int)
                    {
                        Value = value.IdPlayer2
                    }
                }
            };

            sqlCommand.Execute();
        }
    }
}
