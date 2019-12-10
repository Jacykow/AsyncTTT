using System;
using UniRx;

namespace Gulib.Abstraction
{
    public abstract class ObservableOperation<TResult>
    {
        private bool _executed = false;

        private readonly Subject<TResult> _subject = new Subject<TResult>();

        protected abstract IObservable<TResult> ExecuteOnce();

        public IObservable<TResult> Execute()
        {
            if (_executed == false)
            {
                ExecuteOnce().Subscribe(
                   result => _subject.OnNext(result),
                   error => _subject.OnError(error),
                   () => _subject.OnCompleted());
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