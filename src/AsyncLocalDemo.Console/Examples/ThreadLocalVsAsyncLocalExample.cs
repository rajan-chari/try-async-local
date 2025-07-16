using System;
using System.Threading;
using System.Threading.Tasks;
using AsyncLocalDemo.Console.Utilities;

namespace AsyncLocalDemo.Console.Examples
{
    /// <summary>
    /// Demonstrates the differences between ThreadLocal and AsyncLocal behavior
    /// </summary>
    public class ThreadLocalVsAsyncLocalExample
    {
        private static readonly ThreadLocal<string> _threadLocal = 
            new ThreadLocal<string>(() => "Default ThreadLocal Value");
        
        private static readonly AsyncLocalContext<string> _asyncLocal = 
            new AsyncLocalContext<string>();

        public static async Task RunExample()
        {
            System.Console.WriteLine("\n=== ThreadLocal vs AsyncLocal Comparison ===\n");

            // Set initial values
            _threadLocal.Value = "Main Thread Value";
            _asyncLocal.Value = "Main Thread Value";

            System.Console.WriteLine("Initial Values:");
            System.Console.WriteLine($"ThreadLocal: {_threadLocal.Value}");
            System.Console.WriteLine($"AsyncLocal: {_asyncLocal.Value}\n");

            // Demonstrate behavior with Task.Run
            System.Console.WriteLine("=== Task.Run Comparison ===");
            await Task.Run(async () =>
            {
                System.Console.WriteLine("\nInside Task.Run:");
                System.Console.WriteLine($"ThreadLocal: {_threadLocal.Value}"); // Will show default value
                System.Console.WriteLine($"AsyncLocal: {_asyncLocal.Value}\n"); // Will show "Main Thread Value"

                _threadLocal.Value = "Modified in Task";
                _asyncLocal.Value = "Modified in Task";

                System.Console.WriteLine("After modification in Task.Run:");
                System.Console.WriteLine($"ThreadLocal: {_threadLocal.Value}");
                System.Console.WriteLine($"AsyncLocal: {_asyncLocal.Value}\n");

                await Task.Delay(100); // Force thread switch

                System.Console.WriteLine("After await in Task.Run:");
                System.Console.WriteLine($"ThreadLocal: {_threadLocal.Value}");
                System.Console.WriteLine($"AsyncLocal: {_asyncLocal.Value}\n");
            });

            System.Console.WriteLine("Back in main thread:");
            System.Console.WriteLine($"ThreadLocal: {_threadLocal.Value}");
            System.Console.WriteLine($"AsyncLocal: {_asyncLocal.Value}\n");

            // Demonstrate behavior with explicit new thread
            System.Console.WriteLine("=== New Thread Comparison ===");
            var thread = new Thread(() =>
            {
                System.Console.WriteLine("\nInside new thread:");
                System.Console.WriteLine($"ThreadLocal: {_threadLocal.Value}"); // Will show default value
                System.Console.WriteLine($"AsyncLocal: {_asyncLocal.Value}\n"); // Will show empty/null
            });
            thread.Start();
            thread.Join();

            System.Console.WriteLine("\nKey Differences:");
            System.Console.WriteLine("1. ThreadLocal values are specific to each thread");
            System.Console.WriteLine("2. AsyncLocal values flow with async execution context");
            System.Console.WriteLine("3. ThreadLocal uses default value for new threads");
            System.Console.WriteLine("4. AsyncLocal maintains value across async/await boundaries\n");
        }
    }
}