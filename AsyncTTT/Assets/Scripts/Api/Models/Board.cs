using Assets.Scripts.Api.Enums;

namespace Assets.Scripts.Api.Models
{
    public class Board
    {
        public string Opponent { get; set; }
        public int Turn { get; set; }
        public BoardState Result { get; set; }
    }
}
