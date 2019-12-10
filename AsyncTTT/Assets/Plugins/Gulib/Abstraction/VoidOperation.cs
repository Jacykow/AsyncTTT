using System;
using UniRx;

namespace Gulib.Abstraction
{
    public abstract class VoidOperation : ObservableOperation<Unit>
    {
        protected sealed override IObservable<Unit> ExecuteOnce()
        {
            ExecuteImmediate();
            return Observable.ReturnUnit();
        }

        protected abstract void ExecuteImmediate();
    }
}
