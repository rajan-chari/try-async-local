using System;

namespace AsyncLocalDemo.Console.Utilities
{
    /// <summary>
    /// Provides correlation ID tracking across async operations for logging and diagnostics
    /// </summary>
    public static class CorrelationContext
    {
        private static readonly AsyncLocalContext<string> _correlationId = new AsyncLocalContext<string>();

        /// <summary>
        /// Gets the current correlation ID for the async context
        /// </summary>
        public static string Current => _correlationId.Value ?? string.Empty;

        /// <summary>
        /// Creates a new correlation scope with a new correlation ID
        /// </summary>
        public static IDisposable NewCorrelationScope()
        {
            return NewCorrelationScope(Guid.NewGuid().ToString("N"));
        }

        /// <summary>
        /// Creates a new correlation scope with the specified correlation ID
        /// </summary>
        public static IDisposable NewCorrelationScope(string correlationId)
        {
            if (string.IsNullOrEmpty(correlationId))
                throw new ArgumentNullException(nameof(correlationId));

            return _correlationId.CreateScope(correlationId);
        }

        /// <summary>
        /// Logs a message with the current correlation ID
        /// </summary>
        public static void LogWithCorrelation(string message)
        {
            var correlationId = Current;
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
            System.Console.WriteLine($"[{timestamp}] [{correlationId}] {message}");
        }
    }
}