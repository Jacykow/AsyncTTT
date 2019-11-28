using System;

namespace Gulib.Abstraction
{
    public abstract class ObservableOperation<TResult> : IObservableOperation<TResult>
    {
        public abstract IObservable<TResult> ExecuteAsObservable();

        public IDisposable Subscribe(IObserver<TResult> observer)
        {
            return ExecuteAsObservable().Subscribe(observer);
        }
    }
}
