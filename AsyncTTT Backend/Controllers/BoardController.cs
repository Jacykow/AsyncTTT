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
    public class BoardController : ControllerBase
    {
        //podajesz nick w headerze, dostajesz liste gier, w ktorych bierzesz udzial
        //dostajesz w modelu id aktualnej tury (ona moze byc rowna 0 i wtedy oznacza to, ze jest 0 ruchow w grze albo obydwaj gracze wykonali ruchy)
        //za pomoca tego mozesz okreslic czyja jest tura: jesi rowna 0 to gracza ktory zaczynal czyli player1, jak inne to player2
        [HttpGet(Name = "GetGame")]
        public IEnumerable<Game> Get()
        {
            var credentials = ControllerUtility.GetCredentials(Request.Headers);

            var sqlCommand = new SimpleSqlCommand<Game>()
            {
                SqlCommand = "SELECT id_game, player1, player2, coalesce(id_turncur,0), CONCAT(CONVERT(varchar(10), (SELECT nickname from players p join credentials c on (c.id_cred = p.id_cred) WHERE id_player = player1)),' vs ',CONVERT(varchar(10), (SELECT nickname from players p join credentials c on (c.id_cred = p.id_cred) WHERE id_player = player2))), CASE WHEN id_turncur IS NULL THEN 0 WHEN id_turncur IS NOT NULL THEN CASE WHEN (SELECT id_player FROM moves m JOIN current_turns c on (m.id_move = c.id_move) WHERE c.id_turncur = g.id_turncur) = g.player1 THEN 2 WHEN (SELECT id_player FROM moves m JOIN current_turns c on (m.id_move = c.id_move) WHERE c.id_turncur = g.id_turncur) = g.player2 THEN 1 END END from games g WHERE player1 = (SELECT p.id_player FROM credentials c join players p on (c.id_cred = p.id_cred) WHERE c.nickname = @nick) or player2 = (SELECT p.id_player FROM credentials c join players p on (c.id_cred = p.id_cred) WHERE c.nickname = @nick)",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@nick", SqlDbType.VarChar)
                    {
                        Value = credentials.login
                    }
                },
                ModelExtractor = reader => new Game
                {
                    id_game = (int)reader[0],
                    id_player1 = (int)reader[1],
                    id_player2 = (int)reader[2],
                    id_current_turn = (int)reader[3], //zamiast tego ktora tura z kolei
                    name = (string)reader[4],
                    who_move = (int)reader[5]
                    
                }
            };

            return sqlCommand.Execute();
        }

        //podajesz nick w headerze, a body (w api na trello opisane)
        [HttpPost]
        public void Post([FromBody] Invitation value)
        {
            var credentials = ControllerUtility.GetCredentials(Request.Headers);

            var sqlCommand = new SimpleSqlCommand<Credentials>()
            {
                SqlCommand = "INSERT INTO games_invitations(sender, reciever) values((SELECT id_player FROM credentials c join players p on(c.id_cred =  p.id_cred) WHERE nickname = @nick),@rec)",
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
