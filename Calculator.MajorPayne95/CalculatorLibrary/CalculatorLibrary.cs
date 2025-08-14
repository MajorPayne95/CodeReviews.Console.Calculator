// CalculatorLibrary.cs
using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {

        JsonWriter writer;

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


        // CalculatorLibrary.cs
        public double DoOperation(double num1, double num2, string op)
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
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

    }
    public class UsageLogger
    {
        JsonWriter writer;

        public UsageLogger()
        {
            StreamWriter logFile = File.CreateText("usagelog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("CalculationsPerformed");
            writer.WriteStartArray();
        }
        public void LogUsage(int usageCount)
        {
            // Log the usage count to a file or database
            Console.WriteLine($"Calculators performed: {usageCount}.");
        }

        public void Finish(int usageCount)
        {
            writer.WriteValue(usageCount);

            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
    public class UIManager
    {
        public void DisplayTitle()
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
        }

        public void DisplayMenu()
        {
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Welcome to the Calculator App!");
            Console.WriteLine("Please choose an option from the menu below:");
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. Basic Calculator");
            Console.WriteLine("2. Advanced Calculator");
            Console.WriteLine("3. History");
            Console.WriteLine("4. Exit");

            //Console.WriteLine("Choose an operator from the following list:");
            // Console.WriteLine("\ta - Add");
            // Console.WriteLine("\ts - Subtract");
            // Console.WriteLine("\tm - Multiply");
            // Console.WriteLine("\td - Divide");
            //Console.Write("Your option? ");

            //return Console.ReadLine();
        }
    }
}