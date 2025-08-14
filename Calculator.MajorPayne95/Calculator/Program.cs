using CalculatorLibrary; // Ensure this is included to access the CalculatorLibrary namespace
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;



namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            double result = 0;
            bool endApp = false;
            int usageCount = 0; // Initialize usage count

            List<string> history = new List<string>();

            Calculator calculator = new Calculator();
            UsageLogger usageLogger = new UsageLogger();
            UIManager uiManager = new UIManager();

            // Display title as the C# console calculator app.
            uiManager.DisplayTitle();

            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                string? operation = "";
                int? menuChoice = 0;
                bool calculationPerformed = false;

                // Display the menu options.
                uiManager.DisplayMenu();

                string? userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int choice) && choice >= 1 && choice <= 4)
                {
                    menuChoice = choice;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                }

                switch (menuChoice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Basic Calculator:");
                        Console.WriteLine("This calculator can perform addition, subtraction, multiplication, and division.");
                        (numInput1, numInput2, operation, result) = uiManager.BasicCalculator(ref numInput1, ref numInput2, ref operation, ref result);
                        calculationPerformed = true;
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Advanced Calculator:");
                        Console.WriteLine("This calculator can perform exponentiation and square root operations.");
                        (numInput1, numInput2, operation, result) = uiManager.AdvancedCalculator(ref numInput1, ref numInput2, ref operation, ref result);
                        calculationPerformed = true;
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("History:");
                        Console.WriteLine(history.Count > 0 ? string.Join("\n", history) : "No history available.");
                        Console.WriteLine("Would you like to clear the history? (y/n)");
                        string? clearHistory = Console.ReadLine();
                        if (clearHistory?.ToLower() == "y") 
                        {
                            history.Clear();
                            Console.WriteLine("History cleared.");
                        }
                        else
                        {
                            Console.WriteLine("History not cleared.");
                        }
                        break;
                    case 4:
                        Console.WriteLine("Exiting the application. Goodbye!");
                        endApp = true;
                        break;
                    default:
                        Console.WriteLine("Invalid menu choice.  Please use numbers 1-4.");
                        break;
                }

                if (calculationPerformed)
                {
                    if (double.TryParse(numInput1, out double number1) && double.TryParse(numInput2, out double number2))
                    {
                        result = calculator.DoOperation(number1, number2, operation);
                        switch (operation)
                        {
                            case "1":
                                operation = "+";
                                history.Add($"{number1} {operation} {number2} = {result}");
                                break;
                            case "2":
                                operation = "-";
                                history.Add($"{number1} {operation} {number2} = {result}");
                                break;
                            case "3":
                                operation = "*";
                                history.Add($"{number1} {operation} {number2} = {result}");
                                break;
                            case "4":
                                operation = "/";
                                history.Add($"{number1} {operation} {number2} = {result}");
                                break;
                            case "5":
                                operation = "To the power of";
                                history.Add($"{number1} {operation} {number2} = {result}");
                                break;
                            case "6":
                                operation = "Square Root";
                                history.Add($"The {operation} of {number1} = {result}");
                                break;
                            case "7":
                                operation = "Logarithm";
                                history.Add($"{number1} {operation} = {result}");
                                break;
                            case "8":
                                operation = "Sine";
                                history.Add($"{number1} degree {operation} = {result}");
                                break;
                            case "9":
                                operation = "Cosine";
                                history.Add($"{number1} degree {operation} = {result}");
                                break;
                            case "10":
                                operation = "Tangent";
                                history.Add($"{number1} degree {operation} = {result}");
                                break;
                            default:
                                operation = "Unknown Operation";
                                break;
                        }
                        Console.WriteLine($"Result: {result}");
                        usageCount++;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter valid numbers.");
                    }
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }

            usageLogger.Finish(usageCount); // Ensure to close the logger properly
            calculator.Finish(); // Ensure to close the calculator properly
            return;
        }
    }
}