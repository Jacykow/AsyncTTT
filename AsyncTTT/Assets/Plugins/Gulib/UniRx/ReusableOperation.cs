using System;
using UniRx;

namespace Gulib.UniRx
{
    public class ReusableOperation<TResult>
    {
        private readonly IOperation<TResult> _operation;
        private readonly Subject<TResult> _subject = new Subject<TResult>();

        private bool _executed = false;

        public ReusableOperation(IOperation<TResult> operation)
        {
            _operation = operation;
        }

        public IObservable<TResult> ExecuteOnce()
        {
            if (_executed == false)
            {
                _operation.Execute().Subscribe(
                   result => _subject.OnNext(result),
                   error => _subject.OnError(error));
                _executed = true;
            }
            return _subject;
        }

        public IObservable<TResult> Observe()
        {
            return _subject;
        }
    }
}