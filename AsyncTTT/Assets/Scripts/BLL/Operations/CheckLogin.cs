﻿using Gulib.UniRx;
using System;
using UniRx;

namespace Assets.Scripts.BLL.Operations
{
    public class CheckLogin : IOperation<Unit>
    {
        public IObservable<Unit> Execute()
        {
            return Observable.ReturnUnit();
        }
    }
}
