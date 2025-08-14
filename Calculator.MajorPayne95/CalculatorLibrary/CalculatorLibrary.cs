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
                case "1":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case "2":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case "3":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case "4":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;
                case "5":
                    // Exponentiation operation
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Exponentiation");
                    break;
                case "6":
                    // Square root operation
                    if (num1 >= 0)
                    {
                        result = Math.Sqrt(num1);
                    }
                    writer.WriteValue("SquareRoot");
                    break;
                case "7":
                    // Logarithm operation
                    if (num1 > 0)
                    {
                        result = Math.Log10(num1);
                    }
                    writer.WriteValue("Logarithm");
                    break;
                case "8":
                    // Sine operation
                    result = Math.Sin(num1 * (Math.PI / 180)); // Convert degrees to radians
                    writer.WriteValue("Sine");
                    break;
                case "9":
                    // Cosine operation
                    result = Math.Cos(num1 * (Math.PI / 180)); // Convert degrees to radians
                    writer.WriteValue("Cosine");
                    break;
                case "10":
                    // Tangent operation
                    result = Math.Tan(num1 * (Math.PI / 180)); // Convert degrees to radians
                    writer.WriteValue("Tangent");
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
        }

        public (string, string, string, double) BasicCalculator(ref string? numInput1, ref string? numInput2, ref string? operation, ref double result)
        {
            if (result != 0)
            {
                Console.WriteLine($"Current result: {result}");
                Console.WriteLine("Do you want to use the current result? (y/n)");
                string? useResult = Console.ReadLine();
                if (useResult?.ToLower() == "y")
                {
                    numInput1 = result.ToString();
                }
            }
            else
            {
                Console.WriteLine("Enter first number: ");
                numInput1 = Console.ReadLine();
            }

            Console.WriteLine("Enter second number: ");
            numInput2 = Console.ReadLine();
            Console.WriteLine("Enter operation: ");
            Console.WriteLine("1. Addition");
            Console.WriteLine("2. Subtraction");
            Console.WriteLine("3. Multiplication");
            Console.WriteLine("4. Division ");
            operation = Console.ReadLine();
            Console.Clear();

            return (numInput1, numInput2, operation, result);
        }

        public (string, string, string, double) AdvancedCalculator(ref string? numInput1, ref string? numInput2, ref string? operation, ref double result)
        {
            Console.Clear();
            Console.WriteLine("Enter operation: ");
            Console.WriteLine("5. Exponent");
            Console.WriteLine("6. Square Root");
            Console.WriteLine("7. Logarithm");
            Console.WriteLine("8. Sine");
            Console.WriteLine("9. Cosine");
            Console.WriteLine("10. Tangent");
            operation = Console.ReadLine();

            if (operation == "5")
            {
                if (result != 0)
                {
                    Console.WriteLine($"Current result: {result}");
                    Console.WriteLine("Do you want to use the current result as base? (y/n)");
                    string? useResult = Console.ReadLine();
                    if (useResult?.ToLower() == "y")
                    {
                        numInput1 = result.ToString();
                    }
                }
                else
                {
                    Console.WriteLine("Enter base number: ");
                    numInput1 = Console.ReadLine();
                }
                Console.WriteLine("Enter exponent: ");
                numInput2 = Console.ReadLine();
            }
            else if (operation == "6")
            {
                Console.WriteLine("Enter number to find square root: ");
                numInput1 = Console.ReadLine();
                numInput2 = "0"; // No second input needed for square root
            }

            else if (operation == "7")
            {
                Console.WriteLine("Enter number to find logarithm: ");
                numInput1 = Console.ReadLine();
                numInput2 = "0"; // No second input needed for logarithm
            }
            else if (operation == "8")
            {
                Console.WriteLine("Enter angle in degrees: ");
                numInput1 = Console.ReadLine();
                numInput2 = "0"; // No second input needed for sine
            }
            else if (operation == "9")
            {
                Console.WriteLine("Enter angle in degrees: ");
                numInput1 = Console.ReadLine();
                numInput2 = "0"; // No second input needed for cosine
            }
            else if (operation == "10")
            {
                Console.WriteLine("Enter angle in degrees: ");
                numInput1 = Console.ReadLine();
                numInput2 = "0"; // No second input needed for tangent
            }
            else
            {
                Console.WriteLine("Invalid operation. Please try again.");
            }
            Console.Clear();
            return (numInput1, numInput2, operation, result);
        }
    }
}