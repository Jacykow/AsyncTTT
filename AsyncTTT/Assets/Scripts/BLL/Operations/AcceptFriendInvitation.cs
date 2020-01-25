using Assets.Scripts.BLL.Models;
using Gulib.UniRx;
using System;
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
            return Observable.ReturnUnit();
        }
    }
}
