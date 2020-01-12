using Assets.Scripts.Api.Enums;
using Assets.Scripts.Api.Models;
using Gulib.UniRx;
using System;
using System.Collections.Generic;
using UniRx;

namespace Assets.Scripts.Api.Operations
{
    public class GetBoards : IOperation<List<Board>>
    {
        public IObservable<List<Board>> Execute()
        {
            var boards = new List<Board>
            {
                new Board
                {
                    Opponent = "Kappaman",
                    Result = BoardState.Ongoing,
                    Turn = 10
                }
            };
            return Observable.Return(boards);
        }
    }
}
