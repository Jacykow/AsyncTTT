using Assets.Scripts.Api.Config;
using Assets.Scripts.Api.Models;
using Assets.Scripts.Api.Operations;
using Assets.Scripts.BLL.Enums;
using Assets.Scripts.BLL.Models;
using Gulib.UniRx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using UniRx;

namespace Assets.Scripts.BLL.Operations
{
    public class GetFriends : IOperation<List<Friend>>
    {
        public IObservable<List<Friend>> Execute()
        {
            var friends = new List<Friend>();
            var friendInvitationsOperation = new AzureApiQuery<List<User>>(
                    ApiConfig.Endpoints.AzureFriendsInvitation,
                    HttpMethod.Get)
                .Execute()
                .Select(invitingUsers =>
                {
                    friends.AddRange(invitingUsers.Select(invitingUser => new Friend
                    {
                        Name = invitingUser.nickname,
                        State = FriendState.Invitation,
                        Id = invitingUser.Id
                    }));
                    return Unit.Default;
                });
            var friendOperation = new AzureApiQuery<List<User>>(
                    ApiConfig.Endpoints.AzureFriends,
                    HttpMethod.Get)
                .Execute()
                .Select(invitingUsers =>
                {
                    friends.AddRange(invitingUsers.Select(invitingUser => new Friend
                    {
                        Name = invitingUser.nickname,
                        State = FriendState.Accepted,
                        Id = invitingUser.Id
                    }));
                    return Unit.Default;
                });
            return Observable
                .WhenAll(friendInvitationsOperation, friendOperation)
                .Select(_ => friends);
        }
    }
}
