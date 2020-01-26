using Assets.Scripts.Api.Config;
using Assets.Scripts.Api.Models;
using Assets.Scripts.Api.Operations;
using Gulib.UniRx;
using System;
using System.Collections.Generic;
using System.Net.Http;
using UniRx;

namespace Assets.Scripts.BLL.Operations
{
    public class InviteFriend : IOperation<Unit>
    {
        private readonly string _friendName;

        public InviteFriend(string friendName)
        {
            _friendName = friendName;
        }

        public IObservable<Unit> Execute()
        {
            return new AzureApiQuery<List<User>>(ApiConfig.Endpoints.AzureUser + "/nick/" + _friendName, HttpMethod.Get).Execute().SelectMany(friend =>
            {
                if (friend.Count != 1)
                {
                    throw new Exception("Niepoprawny login.");
                }
                return new AzureApiRequest(ApiConfig.Endpoints.AzureFriendsInvitation, HttpMethod.Post)
                    .SetBodyModel(new Invitation
                    {
                        Sender = 0,
                        Reciever = friend[0].Id
                    })
                    .Execute();
            });
        }
    }
}
