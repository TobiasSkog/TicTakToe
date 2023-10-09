using TicTakToe.Logic.Enums;

namespace TicTakToe.Game
{
    internal class Piece
    {
        public (int XPos, int YPos) Position { get; private set; }
        public char PieceStyle { get; private set; }
        public PieceState PieceState { get; private set; }
        public int PlayerNeighbours { get; private set; }
        public int ComputerNeighbours { get; private set; }

        public Piece((int XPos, int YPos) position)
        {
            PlayerNeighbours = 0;
            ComputerNeighbours = 0;
            Position = position;
            PieceStyle = ' ';
            PieceState = PieceState.NotPlaced;
        }

        public MoveResult PlacePiece(PlayerType player, char pieceStyle)
        {
            if (PieceState == PieceState.NotPlaced)
            {
                PieceState = (player == PlayerType.Player) ? PieceState.PlayerPlaced : PieceState.ComputerPlaced;
                PieceStyle = pieceStyle;
                return MoveResult.Success;
            }
            else
            {
                return MoveResult.Denied;
            }
        }
        public void UpdateState(PlayerType player, char pieceStyle)
        {
            PieceState = (player == PlayerType.Player) ? PieceState.PlayerPlaced : PieceState.ComputerPlaced;
            PieceStyle = pieceStyle;
        }



    }
}
