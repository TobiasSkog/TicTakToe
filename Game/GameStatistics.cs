namespace TicTakToe.Game
{
    public class GameStatistics
    {
        public int PlayerMoves { get; set; }
        public int ComputerMoves { get; set; }
        public int LowestMoveCount { get; set; }
        public int HighestMoveCount { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int GamesDraw { get; set; }

        public GameStatistics()
        {
            PlayerMoves = 0;
            ComputerMoves = 0;
            LowestMoveCount = 1000;
            HighestMoveCount = -1000;
            GamesPlayed = 0;
            GamesWon = 0;
            GamesDraw = 0;
        }
    }
}
