
using System.Text.RegularExpressions;

namespace CalculatorLibrary
{
    public static class Helpers
    {
        public static bool GetYesOrNo(string message)
        {
            while (true)
            {
                Console.WriteLine(message + " (Y/N)");
                string? answer = Console.ReadLine().ToUpper();

                if (answer == "Y") return true;
                if (answer == "N") return false;

                Console.Write("Please enter 'Y' or 'N'");
            }
        }

        public static double GetNumber(string message)
        {
            Console.Write(message);
            string? numInput = Console.ReadLine();

            double cleanNum = 0;
            while (string.IsNullOrWhiteSpace(numInput) || !double.TryParse(numInput, out cleanNum))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput = Console.ReadLine();
            }
            return cleanNum;
        }

        public static int GetListNumber(string message, Calculator calculator)
        {
            int cleanNum;
            var length = calculator.PreviousCalculations.Count;
            
            Console.Write(message);
            string? numInput = Console.ReadLine();

            while (!int.TryParse(numInput, out cleanNum) || cleanNum <= 0 || cleanNum > length)
            {
                Console.Write("This is not valid input. Please enter a numeric value displayed in the list: ");
                numInput = Console.ReadLine();
            }
            return cleanNum;
        }

        public static string? GetOperator()
        {
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            string? op = Console.ReadLine()?.Trim().ToLower();

            while (op is null || !"asmd".Contains(op))
            {
                Console.WriteLine("Error: Unrecognized input. Choose an operator from the list.");
                op = Console.ReadLine();
            }
            return op;
        }

        public static string GetInput(string message)
        {
            string? answer;
            do
            {
                Console.WriteLine(message);
                answer = Console.ReadLine();
            } while (answer != "1" && answer != "2");
            return answer;
        }
    }
}
