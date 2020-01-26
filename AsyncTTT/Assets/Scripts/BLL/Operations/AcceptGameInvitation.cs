using Assets.Scripts.BLL.Models;
using Gulib.UniRx;
using System;
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
            return Observable.ReturnUnit();
        }
    }
}
