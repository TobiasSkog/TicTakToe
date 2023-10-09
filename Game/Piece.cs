using TicTakToe.Logic.Enums;

namespace TicTakToe.Game
{
    internal class Piece
    {
        public Tuple<int, int> Position { get; private set; }
        public char PieceStyle { get; private set; }
        public PieceState PieceState { get; private set; }
        public int PlayerNeighbours { get; private set; }
        public int ComputerNeighbours { get; private set; }

        public Piece(Tuple<int, int> position)
        {
            PlayerNeighbours = 0;
            ComputerNeighbours = 0;
            Position = position;
            PieceStyle = ' ';
            PieceState = PieceState.NotPlaced;
        }

        public MoveResult PlacePiece(char pieceStyle, PlayerType player)
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

        public void UpdatePlayerNeighbours(int playerNeighbours, int computerNeighbours, PlayerType playerType)
        {
            if (playerType == PlayerType.Computer)
            {
                PlayerNeighbours = playerNeighbours;
                ComputerNeighbours = computerNeighbours;
            }
        }


    }
}
