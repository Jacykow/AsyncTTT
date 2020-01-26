using Assets.Scripts.Api.Config;
using Assets.Scripts.Api.Models;
using Assets.Scripts.Api.Operations;
using Assets.Scripts.BLL.Enums;
using Gulib.UniRx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using UniRx;
using ApiGame = Assets.Scripts.Api.Models.Game;
using BLLGame = Assets.Scripts.BLL.Models.Game;

namespace Assets.Scripts.BLL.Operations
{
    public class GetGames : IOperation<List<BLLGame>>
    {
        public IObservable<List<BLLGame>> Execute()
        {
            var games = new List<BLLGame>();
            var gamesHistoryOperation = new AzureApiQuery<List<ApiGame>>(ApiConfig.Endpoints.AzureGameHistory, HttpMethod.Get)
                .Execute().Select(historyGames =>
                {
                    games.AddRange(historyGames.Select(historyGame => new BLLGame
                    {
                        Board = null,
                        Id = historyGame.id_game,
                        State = GameState.Victory
                    }));
                    return Unit.Default;
                });
            var gamesInvitedOperation = new AzureApiQuery<List<User>>(ApiConfig.Endpoints.AzureGame, HttpMethod.Get)
                .Execute().Select(invitingUsers =>
                {
                    games.AddRange(invitingUsers.Select(invitingUser => new BLLGame
                    {
                        Board = null,
                        Id = -1,
                        Name = "Zaproszenie od " + invitingUser.nickname,
                        State = GameState.Invited,
                        OpponentId = invitingUser.Id
                    })
                        .GroupBy(game => game.Name)
                        .Select(group => group.First()));
                    return Unit.Default;
                });
            return Observable.WhenAll(gamesHistoryOperation, gamesInvitedOperation)
                .Select(_ => games);
        }
    }
}
