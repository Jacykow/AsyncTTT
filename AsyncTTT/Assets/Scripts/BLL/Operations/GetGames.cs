using Assets.Scripts.Api.Config;
using Assets.Scripts.Api.Operations;
using Assets.Scripts.BLL.Enums;
using Assets.Scripts.BLL.Models;
using Gulib.UniRx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using UniRx;
using ApiGame = Assets.Scripts.Api.Models.Game;

namespace Assets.Scripts.BLL.Operations
{
    public class GetGames : IOperation<List<Game>>
    {
        public IObservable<List<Game>> Execute()
        {
            var games = new List<Game>();
            var gamesHistoryOperation = new AzureApiQuery<List<ApiGame>>(ApiConfig.Endpoints.AzureGameHistory, HttpMethod.Get)
                .Execute().Select(historyGames =>
                {
                    games.AddRange(historyGames.Select(apiGame => new Game
                    {
                        Board = null,
                        Id = apiGame.id_game,
                        State = GameState.Victory
                    }));
                    return Unit.Default;
                });
            return Observable.WhenAll(gamesHistoryOperation)
                .Select(_ => games);
        }
    }
}
