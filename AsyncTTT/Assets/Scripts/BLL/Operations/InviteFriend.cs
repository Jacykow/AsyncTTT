using Gulib.UniRx;
using System;
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
            return Observable.ReturnUnit();
        }
    }
}
