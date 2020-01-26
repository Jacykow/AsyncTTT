namespace AsyncTTT_Backend.Models
{
    public class Game
    {
        public int id_game { get; set; }
        public int id_player1 { get; set; }
        public int id_player2 { get; set; }
        public int count_turns { get; set; }
        public string name { get; set; }
        public int who_move { get; set; } //0 - obydwaj, 1 - player1, 2 - player2
    }
}
