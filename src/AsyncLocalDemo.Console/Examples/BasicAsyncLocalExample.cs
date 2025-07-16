using System;
using System.Threading.Tasks;
using AsyncLocalDemo.Console.Utilities;

namespace AsyncLocalDemo.Console.Examples
{
    /// <summary>
    /// Demonstrates basic usage of AsyncLocal with simple value storage and async flow
    /// </summary>
    public class BasicAsyncLocalExample
    {
        private static readonly AsyncLocalContext<string> _context = new AsyncLocalContext<string>();

        public static async Task RunExample()
        {
            System.Console.WriteLine("\n=== Basic AsyncLocal Example ===\n");

            // Demonstrate basic value setting and retrieval
            _context.Value = "Main Thread Value";
            System.Console.WriteLine($"Initial Value: {_context.Value}");

            // Show how AsyncLocal flows with async/await
            await Task.Run(async () =>
            {
                System.Console.WriteLine($"Value in Task.Run: {_context.Value}");
                _context.Value = "Changed in Task.Run";
                System.Console.WriteLine($"Changed value: {_context.Value}");

                await Task.Delay(100); // Simulate some async work
                System.Console.WriteLine($"Value after await: {_context.Value}");
            });

            System.Console.WriteLine($"Value back in main thread: {_context.Value}\n");

            // Demonstrate scoped usage
            System.Console.WriteLine("=== Scoped Usage Example ===\n");
            
            _context.Value = "Original Value";
            System.Console.WriteLine($"Starting value: {_context.Value}");

            using (var scope = _context.CreateScope("Scoped Value"))
            {
                System.Console.WriteLine($"Inside scope: {_context.Value}");
                
                await Task.Run(() =>
                {
                    System.Console.WriteLine($"Inside scope and Task.Run: {_context.Value}");
                });
            }

            System.Console.WriteLine($"After scope disposed: {_context.Value}\n");
        }
    }
}