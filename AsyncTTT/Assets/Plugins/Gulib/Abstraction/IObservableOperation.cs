using System;

namespace Gulib.Abstraction
{
    public interface IObservableOperation<TResult> : IObservable<TResult>
    {
        IObservable<TResult> ExecuteAsObservable();
    }
}
