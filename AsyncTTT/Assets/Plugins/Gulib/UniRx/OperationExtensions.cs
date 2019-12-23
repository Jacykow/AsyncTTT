namespace Gulib.UniRx
{
    public static class OperationExtensions
    {
        public static ReusableOperation<TResult> AsReusable<TResult>(this IOperation<TResult> observableMethod)
        {
            return new ReusableOperation<TResult>(observableMethod);
        }
    }
}
