using Assets.Scripts.Api.Config;
using Assets.Scripts.Api.Models;
using Assets.Scripts.Api.Operations;
using Assets.Scripts.BLL.Models;
using Assets.Scripts.Managers;
using Gulib.UniRx;
using System;
using System.Collections.Generic;
using System.Net.Http;
using UniRx;

namespace Assets.Scripts.BLL.Operations
{
    public class GetBoard : IOperation<Game>
    {
        private readonly Game _game;

        public GetBoard(Game game)
        {
            _game = game;
        }

        public IObservable<Game> Execute()
        {
            return new AzureApiQuery<List<Move>>(ApiConfig.Endpoints.AzureBoardId + $"/{_game.Id}", HttpMethod.Get)
                .Execute().Select(moves =>
                {
                    _game.Board = new int[20, 20];
                    int playerId = AuthorizationManager.Main.Id;

                    for (int y = 0; y < 20; y++)
                    {
                        for (int x = 0; x < 20; x++)
                        {
                            _game.Board[x, y] = -1;
                        }
                    }
                    foreach (var move in moves)
                    {
                        _game.Board[move.XCoord, move.YCoord] =
                            move.id_player == 0 ? -1 :
                            move.id_player == playerId ? 0 : -1;
                    }
                    return _game;
                });
        }
    }
}
