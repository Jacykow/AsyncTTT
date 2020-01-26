using Assets.Scripts.BLL.Models;
using Gulib.UniRx;
using System;
using UniRx;

namespace Assets.Scripts.BLL.Operations
{
    public class GetBoard : IOperation<Game>
    {
        private readonly Game _game;

        public GetBoard(Game game)
        {
            _game = game;
        }

        public IObservable<Game> Execute()
        {
            _game.Board = new int[20, 20];
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    _game.Board[y, x] =
                        (y * 2 + x) % 5 == 0 ? 0
                        : (y + x * 3) % 7 == 0 ? 1
                        : -1;
                }
            }
            return Observable.Return(_game);
        }
    }
}
