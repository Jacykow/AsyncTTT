using Assets.Scripts.Api.Operations;
using Assets.Scripts.Managers;
using Gulib.UniRx;
using System;
using UniRx;

namespace Assets.Scripts.BLL.Operations
{
    public class CheckLogin : IOperation<Unit>
    {
        public IObservable<Unit> Execute()
        {
            return new CheckCredentials(
                AuthorizationManager.Main.Login,
                AuthorizationManager.Main.Password
                ).Execute();
        }
    }
}
