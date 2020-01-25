using Gulib.UniRx;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.BLL.Operations
{
    public class MakeMove : IOperation<Vector2Int>
    {
        private readonly Vector2Int _moveCoords;

        public MakeMove(Vector2Int moveCoords)
        {
            _moveCoords = moveCoords;
        }

        public IObservable<Vector2Int> Execute()
        {
            return Observable.Return(_moveCoords);
        }
    }
}
