using System.Diagnostics;
using System.Diagnostics.Metrics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public int TimesUsed { get; set; }
        private List<Calculation> previousOperations = new List<Calculation>();
        public IReadOnlyList<Calculation> PreviousCalculations => previousOperations;

        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double DoOperation(double num1, double num2, string op, Calculator calculator)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    op = "+";
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    op = "-";
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    op = "*";
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    op = "/";
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            previousOperations.Add(new Calculation() { FirstOperand = num1, SecondOperand = num2, Operation = op, Result = result });
            TimesUsed++;
            PrintResult(result, calculator, num1, num2, op);
            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public void RemoveLastCalculation() => previousOperations.RemoveAt(previousOperations.Count - 1);

        public void DisplayCalculations()
        {
            foreach (var (calculation, index) in previousOperations.Select((value, i) => (value, i)))
            {
                Console.WriteLine($"{index + 1}) {calculation}");
            }
        }

        public static void PrintResult(double result, Calculator calculator, double num1, double num2, string op)
        {
            Console.WriteLine($"{num1} {op} {num2} = {result:0.##}");
            Console.WriteLine($"Calulator used {calculator.TimesUsed} {(calculator.TimesUsed == 1 ? "time." : "times.")}");
        }

        public double PerformOperationWithCheck(double num1, double num2, string? op)
        {
            double result = DoOperation(num1, num2, op, this);
            if (double.IsNaN(result))
                Console.WriteLine("This operation will result in a mathematical error.\n");
            return result;
        }
    }
}
