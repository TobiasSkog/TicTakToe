using TicTakToe.Logic;

namespace TicTakToe
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleIO.UserInteraction.Welcome();
            var game = new GameLogic();
            ConsoleIO.UserInteraction.Goodbye(game.Stats);
        }
    }
}