using TicTakToe.Logic;

namespace TicTakToe.Game
{
    internal class Piece
    {
        public Tuple<int, int> Position { get; private set; }
        public char PieceStyle { get; private set; }
        public bool Placed { get; private set; }
        public PlayerType Owner { get; private set; }

        public Piece(Tuple<int, int> position)
        {
            Position = position;
            PieceStyle = ' ';
            Placed = false;
            Owner = PlayerType.NotClaimed;
        }

        public void PlacePiece(char pieceStyle)
        {
            PieceStyle = pieceStyle;


        }


    }
}
