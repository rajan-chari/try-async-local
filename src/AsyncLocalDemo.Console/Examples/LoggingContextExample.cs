using System;
using System.Threading.Tasks;
using AsyncLocalDemo.Console.Utilities;

namespace AsyncLocalDemo.Console.Examples
{
    /// <summary>
    /// Demonstrates using AsyncLocal for correlation IDs in logging scenarios
    /// </summary>
    public class LoggingContextExample
    {
        public static async Task RunExample()
        {
            System.Console.WriteLine("\n=== Logging Context Example ===\n");

            // Simulate processing multiple requests with correlation IDs
            await ProcessRequestAsync("Get User Profile");
            await ProcessRequestAsync("Update Settings");
        }

        private static async Task ProcessRequestAsync(string requestName)
        {
            // Create a new correlation scope for this request
            using (CorrelationContext.NewCorrelationScope())
            {
                CorrelationContext.LogWithCorrelation($"Starting request: {requestName}");

                // Simulate some async processing with multiple operations
                await Task.WhenAll(
                    SimulateDataAccessAsync(requestName),
                    SimulateBackgroundWorkAsync(requestName)
                );

                CorrelationContext.LogWithCorrelation($"Completed request: {requestName}");
            }
        }

        private static async Task SimulateDataAccessAsync(string operation)
        {
            CorrelationContext.LogWithCorrelation($"Starting database operation for {operation}");
            await Task.Delay(100); // Simulate database access

            // Spawn a new task to demonstrate correlation ID flow
            await Task.Run(async () =>
            {
                CorrelationContext.LogWithCorrelation($"Executing database query for {operation}");
                await Task.Delay(50); // Simulate query execution
                CorrelationContext.LogWithCorrelation($"Database query completed for {operation}");
            });

            CorrelationContext.LogWithCorrelation($"Database operation completed for {operation}");
        }

        private static async Task SimulateBackgroundWorkAsync(string operation)
        {
            CorrelationContext.LogWithCorrelation($"Starting background processing for {operation}");
            
            // Simulate parallel work items
            var tasks = new[]
            {
                Task.Run(async () =>
                {
                    CorrelationContext.LogWithCorrelation($"Background task 1 for {operation}");
                    await Task.Delay(75);
                }),
                Task.Run(async () =>
                {
                    CorrelationContext.LogWithCorrelation($"Background task 2 for {operation}");
                    await Task.Delay(50);
                })
            };

            await Task.WhenAll(tasks);
            CorrelationContext.LogWithCorrelation($"Background processing completed for {operation}");
        }
    }
}