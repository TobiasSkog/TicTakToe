namespace TicTakToe.Logic.Enums
{
    public enum MoveResult
    {
        Success,
        Denied
    }

    public enum PieceState
    {
        PlayerPlaced,
        ComputerPlaced,
        NotPlaced
    }

    public enum PlayerType
    {
        Invalid,
        Player,
        Computer,
        Draw
    }

}
