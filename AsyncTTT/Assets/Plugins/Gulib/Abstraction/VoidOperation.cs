using System;
using UniRx;

namespace Gulib.Abstraction
{
    public abstract class VoidOperation : IObservableOperation<Unit>
    {
        public abstract void Execute();

        public IObservable<Unit> ExecuteAsObservable()
        {
            Execute();
            return Observable.ReturnUnit();
        }

        public IDisposable Subscribe(IObserver<Unit> observer)
        {
            return ExecuteAsObservable().Subscribe(observer);
        }
    }
}
