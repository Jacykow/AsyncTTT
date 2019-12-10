using System;
using UniRx;

namespace Gulib.Abstraction
{
    public abstract class Operation<TResult> : ObservableOperation<TResult>
    {
        protected sealed override IObservable<TResult> ExecuteOnce()
        {
            return Observable.Return(ExecuteImmediate());
        }

        protected abstract TResult ExecuteImmediate();
    }
}
