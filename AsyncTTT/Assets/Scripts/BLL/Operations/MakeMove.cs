using Assets.Scripts.BLL.Models;
using Gulib.UniRx;
using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.BLL.Operations
{
    public class MakeMove : IOperation<MakeMoveResponse>
    {
        private readonly Vector2Int _moveCoords;

        public MakeMove(Vector2Int moveCoords)
        {
            _moveCoords = moveCoords;
        }

        public IObservable<MakeMoveResponse> Execute()
        {
            return Observable.Return(new MakeMoveResponse { MoveCoords = _moveCoords, GameEnded = Random.value > 0.5f });
        }
    }
}
