using System.Text;
using TicTakToe.Game;

namespace TicTakToe.ConsoleIO
{

    public class Validation
    {
        public static Dictionary<string, bool> ValidAnswers = new Dictionary<string, bool>()
            {
                { "Y", true },
                { "YE", true },
                { "YES", true },
                { "J", true },
                { "JA", true },
                { "N", false },
                { "NO", false },
                { "NE", false },
                { "NEJ", false }
            };
        public static int MaxCellLength = 0;
        public static int GetInteger(string prompt, int minRange)
        {
            while (true)
            {
                Console.Write(prompt);


                if (int.TryParse(Console.ReadLine(), out int validInt))
                {
                    if (validInt >= minRange && validInt < 10)
                    {
                        return validInt;
                    }

                    if (validInt > 9)
                    {
                        Console.WriteLine($"Are you sure? {validInt * validInt} is a large grid size!");
                        var answer = Console.ReadLine().ToUpper();
                        if (ValidAnswers.ContainsKey(answer) && ValidAnswers[answer])
                        {
                            return validInt;
                        }
                    }
                }

                Console.WriteLine($"\nInvalid input. Number must be an integer greater than or equal to {minRange}.");
            }
        }
        public static bool PlayAgain(string prompt)
        {


            while (true)
            {
                Console.Write(prompt);
                var answer = Console.ReadLine().ToUpper();

                if (ValidAnswers.ContainsKey(answer))
                {
                    return ValidAnswers[answer];
                }

                Console.WriteLine($"\nInvalid input. Please answer Yes or No.");
            }
        }
        public static string GetCharFromSelection(string prompt, string allowedChars)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            var charList = allowedChars.ToUpper().ToList();
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine().ToUpper();

                if (!string.IsNullOrEmpty(input) && charList.Contains(input[0]))
                {
                    return input[0].ToString() == "O" ? "❍" : "🗙";
                }
                Console.WriteLine($"\nInvalid input!");
                Console.Write("Please enter one of the allowed characters: ");
                for (int i = 0; i < charList.Count; i++)
                {
                    Console.Write($"{charList[i]}{(i < charList.Count - 1 ? ", " : "")}");
                }
            }

        }

        public static (int X, int Y) IsValidMove(string prompt, Board gameBoard)
        {
            Console.Clear();
            var board = gameBoard.GameGrid;
            var x = gameBoard.GridSizeX;
            var y = gameBoard.GridSizeY;
            var maxCellValue = x * y;
            var maxCellValueLength = maxCellValue.ToString().Length;
            MaxCellLength = MaxCellLength < maxCellValueLength ? maxCellValueLength : MaxCellLength;

            var validPlacementPositions = new SortedDictionary<int, (int, int)>();
            int counter = 0;
            var gridChoice = new StringBuilder();
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (board[i, j].PieceStyle == " " && board[i, j].PieceState == Logic.Enums.PieceState.NotPlaced)
                    {
                        counter++;
                        var counterAdjusted = counter.ToString().PadLeft(maxCellValueLength);
                        gridChoice.Append($"[ {counterAdjusted} ]");
                        validPlacementPositions.Add(counter, (i, j));
                    }
                    else
                    {
                        gridChoice.Append($"[ {(i * y + j > 9 ? $" {board[i, j].PieceStyle.PadLeft(maxCellValueLength)}" : $"{board[i, j].PieceStyle.PadLeft(maxCellValueLength)}")} ]");
                    }
                    if (j == gameBoard.GridSizeX - 1)
                    {
                        gridChoice.Append(Environment.NewLine);
                    }
                }
            }

            while (true)
            {

                Console.WriteLine(gridChoice.ToString());
                Console.Write(prompt);
                var input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int validInt))
                {
                    if (validPlacementPositions.ContainsKey(validInt))
                    {
                        var validPos = validPlacementPositions[validInt];
                        validPlacementPositions.Remove(validInt);
                        return validPos;
                    }
                }
                if (validPlacementPositions.Count > 0)
                {
                    var lowestValid = validPlacementPositions.Keys.Min();
                    var highestValid = validPlacementPositions.Keys.Max();
                    Console.WriteLine($"Please enter a value from ({lowestValid} - {highestValid})");
                }
            }
        }
    }
}

