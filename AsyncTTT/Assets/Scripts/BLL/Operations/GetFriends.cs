using Assets.Scripts.BLL.Enums;
using Assets.Scripts.BLL.Models;
using Gulib.UniRx;
using System;
using System.Collections.Generic;
using UniRx;

namespace Assets.Scripts.BLL.Operations
{
    public class GetFriends : IOperation<List<Friend>>
    {
        public IObservable<List<Friend>> Execute()
        {
            return Observable.Return(new List<Friend>
            {
                new Friend
                {
                    Name = "Marko",
                    State = FriendState.Accepted
                },
                new Friend
                {
                    Name = "Polo",
                    State = FriendState.Invitation
                }
            });
        }
    }
}
