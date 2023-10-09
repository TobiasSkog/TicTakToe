namespace TicTakToe.ConsoleIO
{
    internal class Validation
    {
        public static int GetInteger(string prompt, int minRange)
        {
            while (true)
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out int validInt))
                {
                    if (validInt >= minRange)
                    {
                        return validInt;
                    }
                }

                Console.WriteLine($"Invalid input. Number must be an integer greater than or equal to {minRange}.");
            }
        }

        public static char GetCharFromSelection(string prompt, string allowedChars)
        {
            var charList = allowedChars.ToUpper().ToList();
            charList.AddRange(allowedChars.ToLower().ToList());
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadKey();

                if (charList.Contains(input.KeyChar))
                {
                    return input.KeyChar;
                }
                else if (input.Key == ConsoleKey.Escape)
                {
                    return 'X';
                }
                Console.WriteLine($"\nInvalid input!");
                Console.Write("Please enter one of the allowed characters: ");
                for (int i = 0; i < charList.Count / 2; i++)
                {
                    Console.Write($"{charList[i]}{(i < (charList.Count / 2) - 1 ? ", " : "")}");
                }
                Console.WriteLine("\nOr press 'Esc' to cancel and default to 'X'.");
            }

        }
    }
}
