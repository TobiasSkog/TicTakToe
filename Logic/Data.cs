namespace TicTakToe.Logic
{
    public class Data
    {
        public (int X, int Y) Pos { get; set; }
        public string Piece { get; set; }
        public Data((int X, int Y) pos, string piece)
        {
            Pos = pos;
            Piece = piece;
        }
    }
}
