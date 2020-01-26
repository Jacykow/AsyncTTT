using Assets.Scripts.Api.Config;
using Assets.Scripts.Api.Models;
using Assets.Scripts.Api.Operations;
using Assets.Scripts.BLL.Models;
using Gulib.UniRx;
using System;
using System.Net.Http;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.BLL.Operations
{
    public class MakeMove : IOperation<Vector2Int>
    {
        private readonly Game _game;
        private readonly Vector2Int _moveCoords;

        public MakeMove(Game game, Vector2Int moveCoords)
        {
            _game = game;
            _moveCoords = moveCoords;
        }

        public IObservable<Vector2Int> Execute()
        {
            return new AzureApiRequest(ApiConfig.Endpoints.AzureBoardId, HttpMethod.Post)
                .SetBodyModel(new Move
                {
                    IdGame = _game.Id,
                    XCoord = _moveCoords.x,
                    YCoord = _moveCoords.y
                })
                .Execute()
                .Select(_ => _moveCoords);
        }
    }
}
