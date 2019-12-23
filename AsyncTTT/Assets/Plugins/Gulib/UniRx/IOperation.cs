using System;

namespace Gulib.UniRx
{
    public interface IOperation<TResult>
    {
        IObservable<TResult> Execute();
    }
}
