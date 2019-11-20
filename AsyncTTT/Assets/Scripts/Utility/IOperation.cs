using System;

namespace Assets.Scripts.Utility
{
    public interface IOperation<TResult>
    {
        IObservable<TResult> Execute();
    }
}
