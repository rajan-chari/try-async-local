using System;
using System.Threading.Tasks;
using AsyncLocalDemo.Console.Utilities;
using Xunit;

namespace AsyncLocalDemo.Tests
{
    public class AsyncLocalContextTests
    {
        [Fact]
        public void Value_SetAndGet_WorksCorrectly()
        {
            // Arrange
            var context = new AsyncLocalContext<string>();
            var testValue = "Test Value";

            // Act
            context.Value = testValue;

            // Assert
            Assert.Equal(testValue, context.Value);
        }

        [Fact]
        public async Task Value_FlowsWithAsyncContext()
        {
            // Arrange
            var context = new AsyncLocalContext<string>();
            var testValue = "Test Value";
            context.Value = testValue;

            // Act
            var result = await Task.Run(() => context.Value);

            // Assert
            Assert.Equal(testValue, result);
        }

        [Fact]
        public void Value_InNewThread_FlowsWithExecutionContext()
        {
            // Arrange
            var context = new AsyncLocalContext<string>();
            var testValue = "Test Value";
            context.Value = testValue;
            string? newThreadValue = null;

            // Act
            var thread = new System.Threading.Thread(() =>
            {
                // Value flows with ExecutionContext
                newThreadValue = context.Value;
                
                // Changes in the new thread are isolated
                context.Value = "New Thread Value";
                Assert.Equal("New Thread Value", context.Value);
            });
            thread.Start();
            thread.Join();

            // Assert
            // Value from parent thread flows to new thread
            Assert.Equal(testValue, newThreadValue);
            // Original thread's value is unchanged
            Assert.Equal(testValue, context.Value);
        }

        [Fact]
        public async Task CreateScope_RestoresValueOnDispose()
        {
            // Arrange
            var context = new AsyncLocalContext<string>();
            var originalValue = "Original Value";
            var scopedValue = "Scoped Value";
            context.Value = originalValue;

            // Act
            using (context.CreateScope(scopedValue))
            {
                Assert.Equal(scopedValue, context.Value);
                
                // Verify scope flows with async
                await Task.Run(() =>
                {
                    Assert.Equal(scopedValue, context.Value);
                });
            }

            // Assert
            Assert.Equal(originalValue, context.Value);
        }

        [Fact]
        public async Task CreateScope_HandlesNestedScopes()
        {
            // Arrange
            var context = new AsyncLocalContext<string>();
            var originalValue = "Original";
            var outerScopeValue = "Outer";
            var innerScopeValue = "Inner";
            context.Value = originalValue;

            // Act & Assert
            using (context.CreateScope(outerScopeValue))
            {
                Assert.Equal(outerScopeValue, context.Value);

                using (context.CreateScope(innerScopeValue))
                {
                    Assert.Equal(innerScopeValue, context.Value);
                    
                    await Task.Run(() =>
                    {
                        Assert.Equal(innerScopeValue, context.Value);
                    });
                }

                Assert.Equal(outerScopeValue, context.Value);
            }

            Assert.Equal(originalValue, context.Value);
        }
    }
}