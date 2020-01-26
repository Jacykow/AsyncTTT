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
    public class BoardIdController : ControllerBase
    {

        //podajesz id gry i dostajesz wszystkie ruchy na planszy
        [HttpGet("{id}", Name = "GetMoves")]
        public IEnumerable<Move> GetMoes(int id)
        {
            var sqlCommand = new SimpleSqlCommand<Move>()
            {
                SqlCommand = "EXECUTE returnMoves @vIdGame = @Id",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int)
                    {
                        Value = id
                    }
                },
                ModelExtractor = reader => new Move
                {
                    XCoord = (int)reader[0],
                    YCoord = (int)reader[1],
                    id_player = (int)reader[2],
                    IdGame = id
                }
            };

            return sqlCommand.Execute();
        }

        //tutaj dojdajesz ruch
        //jezeli przed tym ruchem gra sie zakonczyla remisem, wygrana przegrana to wtedy gra zostaje usunieta i przeniesiona do historii
        //score to id wygranego gracza, 0 oznacza remis
        //mozesz sprawdzic dokladnie tego posta, bo nie mialem zbioru do testow, przesledz algorytm, zwlaszcza szukania po skosie planszy
        //jesli gra sie nie konczy to wtedy ruch aktumatycznie sie dodaje
        //podajesz x, y, id gracza i gry
        //jesli wolisz zeby sprawdzalo czy gra jest wygrana po tym ruchu to jesli ci wygodniej to mozesz to latwo przerobic
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

            var sqlCommand1 = new SimpleSqlCommand<Move>()
            {
                SqlCommand = "EXECUTE returnMoves @vIdGame = @Id",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", SqlDbType.Int)
                    {
                        Value = value.IdGame
                    }
                },
                ModelExtractor = reader => new Move
                {
                    XCoord = (int)reader[0],
                    YCoord = (int)reader[1],
                    id_player = (int)reader[2],
                    IdGame = value.IdGame
                }
            };

            var movesList = sqlCommand1.Execute();

            if (movesList.Count == 400)
            {
                var sqlCommandR = new SimpleSqlCommand<User>()
                {
                    SqlCommand = "EXECUTE addResult @vId_game = @idgame, @vId_Score = 0",
                    Parameters = new SqlParameter[]
                    {
                            new SqlParameter("@idgame", SqlDbType.Int)
                            {
                                Value = value.IdGame
                            }
                    }
                };

                sqlCommandR.Execute();
                return;
            }

            int[,] board = new int[20, 20];
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    board[x, y] = -1;
                }
            }
            foreach (var move in movesList)
            {
                board[move.XCoord, move.YCoord] = move.id_player;
            }

            int winner = 0;
            for (int y = 2; y < 18 && winner == 0; y++)
            {
                for (int x = 2; x < 18 && winner == 0; x++)
                {
                    if (board[x, y] == -1)
                    {
                        continue;
                    }
                    if (CheckVictory(board, x, y, 1, 0) ||
                        CheckVictory(board, x, y, 1, 1) ||
                        CheckVictory(board, x, y, 0, 1) ||
                        CheckVictory(board, x, y, -1, 1))
                    {
                        winner = board[x, y];
                    }
                }
            }

            if (winner != 0)
            {
                var sqlCommandW = new SimpleSqlCommand<User>()
                {
                    SqlCommand = "EXECUTE addResult @vId_game = @idgame, @vId_Score = @idwinner",
                    Parameters = new SqlParameter[]
                    {
                            new SqlParameter("@idgame", SqlDbType.Int)
                            {
                                Value = value.IdGame
                            },
                            new SqlParameter("@idwinner", SqlDbType.Int)
                            {
                                Value = winner
                            }
                    }
                };
                sqlCommandW.Execute();
            }
        }

        private bool CheckVictory(int[,] board, int x, int y, int dx, int dy)
        {
            for (int d = 1; d <= 3; d++)
            {
                if (board[x + dx * d, y + dy * d] != board[x, y])
                {
                    return false;
                }
                if (board[x - dx * d, y - dy * d] != board[x, y])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
