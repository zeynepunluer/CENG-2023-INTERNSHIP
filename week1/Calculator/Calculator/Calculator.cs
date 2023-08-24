namespace Calculator
{
    internal class Calculator
    {
        double num1, num2;
        double? result;

        public double Result { get => (double)result; set => result = value; }
        public double Num1 { get => num1; set => num1 = value; }
        public double Num2 { get => num2; set => num2 = value; }

        public void menu()
        {
            while (true)
            {
                Console.WriteLine("*******CALCULATOR*******");
                Console.WriteLine();
                Console.WriteLine(@"
            1. Addition
            2. Subtraction
            3. Multiplication
            4. Division
            5. Exit
            ");
                Console.WriteLine();
                Console.Write("Please enter the operation number:");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Please enter the first number: ");
                        double Num1 = double.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the second number: ");
                        double Num2 = double.Parse(Console.ReadLine());
                        Console.WriteLine(Addition(Num1, Num2));
                        break;
                    case "2":
                        Console.WriteLine("Please enter the first number: ");
                        Num1 = double.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the second number: ");
                        Num2 = double.Parse(Console.ReadLine());
                        Console.WriteLine(Subtraction(Num1, Num2));
                        break;
                    case "3":
                        Console.WriteLine("Please enter the first number: ");
                        Num1 = double.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the second number: ");
                        Num2 = double.Parse(Console.ReadLine());
                        Console.WriteLine(Multiplication(Num1, Num2));
                        break;
                    case "4":
                        Console.WriteLine("Please enter the first number: ");
                        Num1 = double.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the second number: ");
                        Num2 = double.Parse(Console.ReadLine());
                        Console.WriteLine(Division(Num1, Num2));
                        break;
                    case "5":
                        Console.WriteLine("Exiting the calculator.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        

        public double? Addition(double num1, double num2)
        {
            Result = num1 + num2;
            return result;

        }
        public double? Subtraction(double num1, double num2)
        {
            Result = num1 - num2;
            return result;

        }
        public double? Multiplication(double num1, double num2)
        {
            Result = num1 * num2;
            return result;

        }
        public double? Division(double num1, double num2)
        {
            if (num2 != 0)
            {
                Result = num1 / num2;



            }
            else
            {
                Console.WriteLine("Cannot divide by zero.");
                result = null;

            }
            return result;




        }

    }
}
