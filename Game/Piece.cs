using TicTakToe.Logic.Enums;

namespace TicTakToe.Game
{
    public class Piece
    {
        public (int XPos, int YPos) Position { get; private set; }
        public string PieceStyle { get; private set; }
        public PieceState PieceState { get; private set; }

        public Piece((int XPos, int YPos) position)
        {
            Position = position;
            PieceStyle = " ";
            PieceState = PieceState.NotPlaced;

        }

        public Piece(int XPos, int YPos, PieceState pieceState, string pieceStyle)
        {
            Position = (XPos, YPos);
            PieceState = pieceState;
            PieceStyle = pieceStyle;
        }

        public MoveResult GetPlacementResult() => PieceState == PieceState.NotPlaced ? MoveResult.Success : MoveResult.Denied;

        public void UpdatePiece(PlayerType player, string pieceStyle)
        {
            PieceState = (player == PlayerType.Player) ? PieceState.PlayerPlaced : PieceState.ComputerPlaced;
            PieceStyle = pieceStyle;
        }
        public void AIEvaluation(PlayerType player, string pieceStyle)
        {
            PieceState = (player == PlayerType.Player) ? PieceState.PlayerPlaced : PieceState.ComputerPlaced;
            PieceStyle = pieceStyle;
        }

        public void ResetPiece()
        {
            PieceState = PieceState.NotPlaced;
            PieceStyle = " ";
        }

    }
}
