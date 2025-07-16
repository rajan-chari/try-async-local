using System;
using System.Threading.Tasks;
using AsyncLocalDemo.Console.Examples;

namespace AsyncLocalDemo.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                System.Console.Clear();
                System.Console.WriteLine("AsyncLocal Demonstration");
                System.Console.WriteLine("======================\n");
                System.Console.WriteLine("1. Basic AsyncLocal Usage");
                System.Console.WriteLine("2. ThreadLocal vs AsyncLocal Comparison");
                System.Console.WriteLine("3. Logging Context Example");
                System.Console.WriteLine("4. Exit\n");
                System.Console.Write("Select an option (1-4): ");

                var choice = System.Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await BasicAsyncLocalExample.RunExample();
                            break;
                        case "2":
                            await ThreadLocalVsAsyncLocalExample.RunExample();
                            break;
                        case "3":
                            await LoggingContextExample.RunExample();
                            break;
                        case "4":
                            System.Console.WriteLine("\nThank you for exploring AsyncLocal!");
                            return;
                        default:
                            System.Console.WriteLine("\nInvalid option. Please try again.");
                            break;
                    }

                    System.Console.WriteLine("\nPress any key to continue...");
                    System.Console.ReadKey();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"\nAn error occurred: {ex.Message}");
                    System.Console.WriteLine("Press any key to continue...");
                    System.Console.ReadKey();
                }
            }
        }
    }
}
