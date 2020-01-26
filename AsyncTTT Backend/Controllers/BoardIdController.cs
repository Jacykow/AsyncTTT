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

            //sprawdzanie zwyciezcy

            var tab = new int[20, 20];

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    tab[i, j] = -1;
                }
            }

            List<Move> movesList = new List<Move>();

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

            movesList = sqlCommand1.Execute();


            if(movesList.Count != 0)
            {
                //IF MOVESLIST SIZE == 40 REMIS
                if (movesList.Count == 40)
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
                }
                else
                {
                    int id_player1 = movesList[0].id_player;
                    int id_player2 = 0;
                    int winner = 0;

                    for (int i = 0; i < movesList.Count; i++)
                    {
                        if (movesList[i].id_player != id_player1)
                            id_player2 = movesList[i].id_player;
                        tab[movesList[i].XCoord, movesList[i].YCoord] = movesList[i].id_player;
                    }

                    //IF ID_PLAYER2 == 0 OD RAZU DODAJ MOVE

                    if (id_player2 != 0)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            if (winner != 0)
                                break;
                            for (int j = 0; j <= 15; j++)
                            {
                                if ((tab[i, j] == id_player1 && tab[i, j + 1] == id_player1 && tab[i, j + 2] == id_player1 && tab[i, j + 3] == id_player1 && tab[i, j + 4] == id_player1) || (tab[i, j] == id_player2 && tab[i, j + 1] == id_player2 && tab[i, j + 2] == id_player2 && tab[i, j + 3] == id_player2 && tab[i, j + 4] == id_player2))
                                {
                                    winner = tab[i,j];
                                    break;
                                }
                                if ((tab[j, i] == id_player1 && tab[j, i + 1] == id_player1 && tab[j, i + 2] == id_player1 && tab[j, i + 3] == id_player1 && tab[j, i + 4] == id_player1) || (tab[j, i] == id_player2 && tab[j, i + 1] == id_player2 && tab[j, i + 2] == id_player2 && tab[j, i + 3] == id_player2 && tab[j, i + 4] == id_player2))
                                {
                                    winner = tab[i,j];
                                    break;
                                }

                            }
                        }

                        for (int i = 0; i <= 15; i++)
                        {
                            if (winner != 0)
                                break;
                            for (int j = 0; j <= 15; j++)
                            {
                                if ((tab[i, j] == id_player1 && tab[i + 1, j + 1] == id_player1 && tab[i + 2, j + 2] == id_player1 && tab[i + 3, j + 3] == id_player1 && tab[i + 4, j + 4] == id_player1) || (tab[i, j] == id_player2 && tab[i + 1, j + 1] == id_player2 && tab[i + 2, j + 2] == id_player2 && tab[i + 3, j + 3] == id_player2 && tab[i + 4, j + 4] == id_player2))
                                {
                                    winner = tab[i, j];
                                    break;
                                }
                            }
                        }

                        for (int i = 19; i >= 4; i--)
                        {
                            if (winner != 0)
                                break;
                            for (int j = 19; j >= 4; j--)
                            {
                                if ((tab[i, j] == id_player1 && tab[i - 1, j - 1] == id_player1 && tab[i - 2, j - 2] == id_player1 && tab[i - 3, j - 3] == id_player1 && tab[i - 4, j - 4] == id_player1) || (tab[i, j] == id_player2 && tab[i - 1, j - 1] == id_player2 && tab[i - 2, j - 2] == id_player2 && tab[i - 3, j - 3] == id_player2 && tab[i - 4, j - 4] == id_player2))
                                {
                                    winner = tab[i, j];
                                    break;
                                }
                            }
                        }
                        //IF WINNER != 0 to gra do historii z wynikiem
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
                }


            }
        }
    }
}
