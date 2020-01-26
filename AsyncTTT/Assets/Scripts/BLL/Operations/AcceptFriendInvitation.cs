using Assets.Scripts.Api.Config;
using Assets.Scripts.Api.Models;
using Assets.Scripts.Api.Operations;
using Assets.Scripts.BLL.Models;
using Gulib.UniRx;
using System;
using System.Net.Http;
using UniRx;

namespace Assets.Scripts.BLL.Operations
{
    public class AcceptFriendInvitation : IOperation<Unit>
    {
        private readonly Friend _friend;

        public AcceptFriendInvitation(Friend friend)
        {
            _friend = friend;
        }

        public IObservable<Unit> Execute()
        {
            return new AzureApiRequest(
                    ApiConfig.Endpoints.AzureFriendsInvitation,
                    HttpMethod.Put)
                .SetBodyModel(new Invitation
                {
                    Reciever = _friend.Id
                })
                .Execute();
        }
    }
}
