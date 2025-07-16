# AsyncLocal Demonstration Project

## 🚀 Quick Start Guide

### Prerequisites
- .NET SDK 9.0 or later
- Any code editor (VS Code recommended)

### Run the Demo (2 minutes)
```bash
# Clone the repository
git clone https://github.com/yourusername/AsyncLocalDemo
cd AsyncLocalDemo

# Build and run the project
dotnet build
cd src/AsyncLocalDemo.Console
dotnet run

# Run the tests (optional)
cd ../../
dotnet test
```

### What You'll See
You'll get an interactive menu to explore AsyncLocal examples:
1. **Basic Usage** - See how AsyncLocal maintains values across async operations
2. **ThreadLocal vs AsyncLocal** - Understand key differences through practical examples
3. **Logging Context** - Real-world example of correlation ID tracking

Each example provides clear, step-by-step output explaining the concepts.

### Quick Tips
- Use number keys (1-8) to navigate the menu
- Press any key after each example to return to the menu
- Each example is self-contained and can be run independently
- Check the console output for detailed explanations

## Overview

This project demonstrates the usage of `AsyncLocal<T>` in C#, showing how it maintains state across asynchronous control flows. The examples illustrate various use cases and compare `AsyncLocal<T>` with `ThreadLocal<T>`.

## What is AsyncLocal?

`AsyncLocal<T>` is a .NET type that provides a way to store data that flows with the async execution context. Unlike `ThreadLocal<T>` which stores data specific to a thread, `AsyncLocal<T>` maintains data across async/await boundaries, making it ideal for scenarios where context needs to be preserved across asynchronous operations.

## Project Structure

```
AsyncLocalDemo/
├── src/
│   └── AsyncLocalDemo.Console/
│       ├── Examples/
│       │   ├── BasicAsyncLocalExample.cs
│       │   ├── ThreadLocalVsAsyncLocalExample.cs
│       │   └── LoggingContextExample.cs
│       ├── Utilities/
│       │   ├── AsyncLocalContext.cs
│       │   └── CorrelationContext.cs
│       └── Program.cs
└── README.md
```

## Examples Included

1. **Basic AsyncLocal Usage**
   - Simple value storage and retrieval
   - Async flow demonstration
   - Scoped usage with automatic value restoration

2. **ThreadLocal vs AsyncLocal Comparison**
   - Side-by-side comparison of behaviors
   - Thread affinity vs async flow
   - Behavior in Task.Run and new threads

3. **Logging Context Example**
   - Practical correlation ID tracking
   - Async operation tracing
   - Parallel task correlation

## Key Features Demonstrated

- Value persistence across async/await boundaries
- Automatic context flow with async operations
- Scoped value management
- Thread vs async context behavior
- Practical logging scenarios

## Running the Examples

1. Clone the repository
2. Navigate to the project directory
3. Run the project:
   ```
   cd src/AsyncLocalDemo.Console
   dotnet run
   ```
4. Use the interactive menu to explore different examples

## Key Concepts

### AsyncLocal vs ThreadLocal
- `ThreadLocal<T>`: Values are specific to a thread
- `AsyncLocal<T>`: Values flow with async execution context
- `AsyncLocal<T>` maintains context across thread switches in async operations

### Common Use Cases
1. **Logging Context**: Correlation IDs for distributed tracing
2. **User Context**: Maintaining user identity across async operations
3. **Request Context**: HTTP request tracking
4. **Ambient Data**: Configuration or context that should flow with async operations

### Best Practices
1. Use scoped instances where possible
2. Be cautious with value modifications
3. Consider performance implications in high-throughput scenarios
4. Use AsyncLocal for async flow scenarios, ThreadLocal for thread-specific data

## Additional Resources

- [Microsoft Docs - AsyncLocal<T>](https://docs.microsoft.com/en-us/dotnet/api/system.threading.asynclocal-1)
- [Async/Await Best Practices](https://docs.microsoft.com/en-us/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming)