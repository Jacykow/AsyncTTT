using Assets.Scripts.Api.Config;
using Assets.Scripts.Api.Models;
using Assets.Scripts.Api.Operations;
using Assets.Scripts.BLL.Models;
using Assets.Scripts.Managers;
using Gulib.UniRx;
using System;
using System.Net.Http;
using UniRx;

namespace Assets.Scripts.BLL.Operations
{
    public class AcceptGameInvitation : IOperation<Unit>
    {
        private readonly Game _game;

        public AcceptGameInvitation(Game game)
        {
            _game = game;
        }

        public IObservable<Unit> Execute()
        {
            return new AzureApiRequest(ApiConfig.Endpoints.AzureGame, HttpMethod.Put)
                .SetBodyModel(new GameAccept
                {
                    IdPlayer1 = _game.OpponentId,
                    IdPlayer2 = AuthorizationManager.Main.Id
                })
                .Execute();
        }
    }
}
