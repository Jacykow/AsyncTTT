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
    public class GameHistoryController : ControllerBase
    {
        
        //podajesz nick w headerze, zwraca Ci historie Twoich gier (score to id gracza ktory wygral)
        [HttpGet(Name = "GetHistory")]
        public IEnumerable<GameInHistory> GetHistory()
        {
            var credentials = ControllerUtility.GetCredentials(Request.Headers);

            var sqlCommand = new SimpleSqlCommand<GameInHistory>()
            {
                SqlCommand = "SELECT id_ghis, player1, player2, score FROM games_in_history WHERE player1 = (SELECT p.id_player FROM credentials c join players p on (c.id_cred = p.id_cred) WHERE c.nickname = @nick)  or player2 = (SELECT p.id_player FROM credentials c join players p on (c.id_cred = p.id_cred) WHERE c.nickname = @nick)",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@nick", SqlDbType.VarChar)
                    {
                        Value = credentials.login
                    }
                },
                ModelExtractor = reader => new GameInHistory
                {
                    id_game = (int)reader[0],
                    id_player1 = (int)reader[1],
                    id_player2 = (int)reader[2],
                    score = (int)reader[3]
                }
            };

            return sqlCommand.Execute();
        }

     
    }
}
