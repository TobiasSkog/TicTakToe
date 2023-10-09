using TicTakToe.Logic;

namespace TicTakToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleIO.UserInteraction.Welcome();
            new GameLogic();
            ConsoleIO.UserInteraction.Goodbye();
        }
    }
}