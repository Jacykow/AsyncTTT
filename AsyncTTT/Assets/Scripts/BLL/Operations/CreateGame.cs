using Assets.Scripts.BLL.Models;
using Gulib.UniRx;
using System;
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
            return Observable.ReturnUnit();
        }
    }
}
