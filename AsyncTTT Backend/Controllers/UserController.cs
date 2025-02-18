﻿using AsyncTTT_Backend.Models;
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
    public class UserController : ControllerBase
    {

        //zwraca id i id_credentiali usera po podaniu nicku ; sluzy do konwersji nick na id
        [HttpGet("nick/{login}", Name = "GetId")]
        public IEnumerable<User> GetId(string login)
        {

            var sqlCommand = new SimpleSqlCommand<User>()
            {
                SqlCommand = "SELECT id_player, c.id_cred, nickname FROM players p join credentials c on(c.id_cred = p.id_cred) WHERE nickname = @nick",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@nick", SqlDbType.VarChar)
                    {
                        Value = login
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

        //podajesz id i dostajesz nick ; sluzy do konwersji id na nick
        [HttpGet("id/{id}", Name = "GetNick")]
        public IEnumerable<User> GetNick(int id)
        {

            var sqlCommand = new SimpleSqlCommand<User>()
            {
                SqlCommand = "SELECT id_player, c.id_cred, nickname FROM players p join credentials c on(c.id_cred = p.id_cred) WHERE id_player = @id",
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
                    Id_cred = (int)reader[1],
                    nickname = (string)reader[2]
                }
            };

            return sqlCommand.Execute();
        }

    }
}
