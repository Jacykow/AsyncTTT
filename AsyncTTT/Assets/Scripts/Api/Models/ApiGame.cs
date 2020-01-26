namespace Assets.Scripts.Api.Models
{
    public class ApiGame
    {
        public int id_game { get; set; }
        public int id_player1 { get; set; }
        public int id_player2 { get; set; }
        public int id_current_turn { get; set; }
        public string name { get; set; }
    }
}
