using Gulib.UniRx;
using System;
using UniRx;

namespace Assets.Scripts.BLL.Operations
{
    public class Register : IOperation<Unit>
    {
        public IObservable<Unit> Execute()
        {
            return Observable.ReturnUnit();
        }
    }
}
