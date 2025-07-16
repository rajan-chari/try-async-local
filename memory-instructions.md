# AsyncLocal C# Project Memory

## Task Overview
Research AsyncLocal for C# and create a .NET Core project that explains AsyncLocal and how it's used.

## Implementation Status
✅ Project successfully completed with:
- Focused console application demonstrating AsyncLocal concepts
- Comprehensive examples and utilities
- Full test coverage (13 passing tests)
- Clean build with no warnings

## Repository Information
- **GitHub Repository**: https://github.com/rajan-chari/try-async-local
- **Clone Command**: `git clone https://github.com/rajan-chari/try-async-local`

### Implemented Components
1. **Core Utilities**
   - AsyncLocalContext<T>: Generic wrapper with proper null handling
   - CorrelationContext: Practical logging context implementation
   - Thread-safe value management
   - Scoped value handling with IDisposable pattern

2. **Examples**
   - BasicAsyncLocalExample: Core concepts and usage
   - ThreadLocalVsAsyncLocalExample: Comparison and behavior
   - LoggingContextExample: Practical correlation tracking

3. **Test Coverage**
   - Value management and flow
   - Thread behavior verification
   - Scope handling
   - Correlation context scenarios

4. **Project Structure**
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
   ├── tests/
   │   └── AsyncLocalDemo.Tests/
   └── README.md
   ```

## Key Research Areas
- What is AsyncLocal and why it exists
- How AsyncLocal differs from ThreadLocal
- Use cases and scenarios where AsyncLocal is beneficial
- Best practices and performance considerations
- Common patterns and anti-patterns

## AsyncLocal Fundamentals
- **Definition**: AsyncLocal<T> provides ambient data that is local to a given asynchronous control flow
- **Namespace**: System.Threading
- **Key Behavior**: Data flows with async/await execution context, not thread affinity
- **Thread Safety**: AsyncLocal itself is thread-safe for reading/writing the Value property

## Key Differences from ThreadLocal
- **ThreadLocal**: Data is tied to a specific thread
- **AsyncLocal**: Data follows async execution context across thread boundaries
- **Use Case**: AsyncLocal is ideal for async/await scenarios where operations may continue on different threads

## Common Use Cases
1. **Logging Context**: Maintain correlation IDs across async operations
2. **User Context**: Preserve user identity through async call chains
3. **Request Tracing**: Track request information across async boundaries
4. **Ambient Data**: Store data that should be available to all operations in an async flow
5. **Scoped Services**: Implement request-scoped dependency injection

## Technical Details
- Uses ExecutionContext.SuppressFlow() and ExecutionContext.RestoreFlow()
- Value changes are isolated to the current async flow
- Child async operations inherit parent's AsyncLocal values
- Changes in child operations don't affect parent's values (copy-on-write semantics)

## Performance Considerations
- Minimal overhead for reading values
- Setting values has higher cost due to ExecutionContext management
- Should be used judiciously in high-performance scenarios
