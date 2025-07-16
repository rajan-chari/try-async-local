using System.Threading;

namespace AsyncLocalDemo.Console.Utilities
{
    /// <summary>
    /// A generic wrapper for AsyncLocal<T> that provides a cleaner API for managing async-local state
    /// </summary>
    /// <typeparam name="T">The type of value to store in the async-local context</typeparam>
    public class AsyncLocalContext<T>
    {
        private readonly AsyncLocal<T?> _asyncLocal = new AsyncLocal<T?>();

        /// <summary>
        /// Gets or sets the value for the current async context
        /// </summary>
        public T? Value
        {
            get => _asyncLocal.Value;
            set => _asyncLocal.Value = value;
        }

        /// <summary>
        /// Creates a new scope with the specified value and restores the previous value when disposed
        /// </summary>
        public IDisposable CreateScope(T? value)
        {
            return new AsyncLocalScope<T>(_asyncLocal, value);
        }
    }

    /// <summary>
    /// Represents a scope for an AsyncLocal<T> value that automatically restores the previous value when disposed
    /// </summary>
    internal class AsyncLocalScope<T> : IDisposable
    {
        private readonly AsyncLocal<T?> _asyncLocal;
        private readonly T? _previousValue;

        public AsyncLocalScope(AsyncLocal<T?> asyncLocal, T? newValue)
        {
            _asyncLocal = asyncLocal;
            _previousValue = asyncLocal.Value;
            asyncLocal.Value = newValue;
        }

        public void Dispose()
        {
            _asyncLocal.Value = _previousValue;
        }
    }
}