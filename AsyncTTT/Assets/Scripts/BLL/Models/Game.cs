using Assets.Scripts.BLL.Enums;

namespace Assets.Scripts.BLL.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GameState State { get; set; }
        public int[,] Board { get; set; }
        public int TurnOddity { get; set; } = 1;

        public int Order =>
            State == GameState.Invited ? 3 :
            State == GameState.YourTurn ? 2 :
            State == GameState.TheirTurn ? 1 :
            0;
    }
}
