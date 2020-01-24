using Assets.Scripts.BLL.Enums;

namespace Assets.Scripts.BLL.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GameState State { get; set; }
    }
}
