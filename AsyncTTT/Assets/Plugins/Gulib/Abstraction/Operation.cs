using System;
using UniRx;

namespace Gulib.Abstraction
{
    public abstract class Operation<TResult> : IObservableOperation<TResult>
    {
        public abstract TResult Execute();

        public IObservable<TResult> ExecuteAsObservable()
        {
            return Observable.Return(Execute());
        }

        public IDisposable Subscribe(IObserver<TResult> observer)
        {
            return ExecuteAsObservable().Subscribe(observer);
        }
    }
}
