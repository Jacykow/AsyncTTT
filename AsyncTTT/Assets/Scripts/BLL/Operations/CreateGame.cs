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
    public class CreateGame : IOperation<Unit>
    {
        private readonly Friend _opponent;

        public CreateGame(Friend opponent)
        {
            _opponent = opponent;
        }

        public IObservable<Unit> Execute()
        {
            return new AzureApiRequest(ApiConfig.Endpoints.AzureBoard, HttpMethod.Post)
                .SetBodyModel(new Invitation
                {
                    Sender = AuthorizationManager.Main.Id,
                    Reciever = _opponent.Id
                })
                .Execute();
        }
    }
}
