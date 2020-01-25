namespace AsyncTTT_Backend.Models
{
    public class Move
    {
        public int IdGame { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public int id_player { get; set; }   //podawane w gecie w boardid, nie ma go w poscie, bo nie jest tam potrzebny

    }
}
