using UnityEngine;

namespace Assets.Scripts.BLL.Models
{
    public class MakeMoveResponse
    {
        public Vector2Int MoveCoords { get; set; }
        public bool GameEnded { get; set; }
    }
}
