using Assets.Scripts.Api.Config;
using Assets.Scripts.Api.Models;
using Assets.Scripts.Api.Operations;
using Assets.Scripts.BLL.Enums;
using Assets.Scripts.BLL.Models;
using Assets.Scripts.Managers;
using Gulib.UniRx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using UniRx;

namespace Assets.Scripts.BLL.Operations
{
    public class GetGames : IOperation<List<Game>>
    {
        public IObservable<List<Game>> Execute()
        {
            var games = new List<Game>();
            var gamesHistoryOperation = new AzureApiQuery<List<GameInHistory>>(ApiConfig.Endpoints.AzureGameHistory, HttpMethod.Get)
                .Execute().Select(historyGames =>
                {
                    games.AddRange(historyGames.Select(historyGame => new Game
                    {
                        Board = null,
                        Id = historyGame.id_game,
                        Name = historyGame.name,
                        State =
                            historyGame.score == 0 ? GameState.Draw :
                            historyGame.score == AuthorizationManager.Main.Id ? GameState.Victory :
                            GameState.Loss
                    }));
                    return Unit.Default;
                });
            var gamesOngoingOperation = new AzureApiQuery<List<ApiGame>>(ApiConfig.Endpoints.AzureBoard, HttpMethod.Get)
                .Execute().Select(ongoingGames =>
                {
                    int playerId = AuthorizationManager.Main.Id;
                    games.AddRange(ongoingGames.Select(ongoingGame =>
                    {
                        return new Game
                        {
                            Board = null,
                            Id = ongoingGame.id_game,
                            Name = ongoingGame.name + " - Tura " + ongoingGame.count_turns,
                            State =
                                (ongoingGame.id_player1 == playerId && ongoingGame.who_move == 1)
                                || (ongoingGame.id_player2 == playerId && ongoingGame.who_move == 2)
                                || ongoingGame.who_move == 0
                                ? GameState.YourTurn : GameState.TheirTurn,
                            TurnOddity =
                                (ongoingGame.count_turns
                                + (ongoingGame.id_player1 == playerId ? 1 : 0)) % 2,
                            TurnCount = ongoingGame.count_turns
                        };
                    }));
                    return Unit.Default;
                });
            var gamesInvitedOperation = new AzureApiQuery<List<User>>(ApiConfig.Endpoints.AzureGame, HttpMethod.Get)
                .Execute().Select(invitingUsers =>
                {
                    games.AddRange(invitingUsers.Select(invitingUser => new Game
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
            return Observable.WhenAll(
                gamesHistoryOperation,
                gamesInvitedOperation,
                gamesOngoingOperation)
                .Select(_ => games);
        }
    }
}
