using Assets.Scripts.BLL.Enums;
using Assets.Scripts.BLL.Models;
using Gulib.UniRx;
using System;
using System.Collections.Generic;
using UniRx;

namespace Assets.Scripts.BLL.Operations
{
    public class GetGames : IOperation<List<Game>>
    {
        public IObservable<List<Game>> Execute()
        {
            return Observable.Return(new List<Game>
            {
                new Game
                {
                    Id = 0,
                    Name = "A vs B",
                    State = GameState.Victory
                },
                new Game
                {
                    Id = 1,
                    Name = "A vs C",
                    State = GameState.Loss
                },
                new Game
                {
                    Id = 2,
                    Name = "A vs D",
                    State = GameState.Invited
                },
                new Game
                {
                    Id = 3,
                    Name = "A vs E",
                    State = GameState.YourTurn
                },
                new Game
                {
                    Id = 4,
                    Name = "A vs F",
                    State = GameState.TheirTurn
                }
            });
        }
    }
}
