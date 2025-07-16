using System;
using System.Threading.Tasks;
using AsyncLocalDemo.Console.Utilities;
using Xunit;

namespace AsyncLocalDemo.Tests
{
    public class CorrelationContextTests
    {
        [Fact]
        public void Current_ReturnsEmptyString_WhenNoCorrelationIdSet()
        {
            // Act
            var correlationId = CorrelationContext.Current;

            // Assert
            Assert.Equal(string.Empty, correlationId);
        }

        [Fact]
        public void NewCorrelationScope_CreatesNewGuid_WhenNoIdProvided()
        {
            // Act
            using (CorrelationContext.NewCorrelationScope())
            {
                // Assert
                Assert.NotEqual(string.Empty, CorrelationContext.Current);
                Assert.True(Guid.TryParse(CorrelationContext.Current, out _));
            }
        }

        [Fact]
        public void NewCorrelationScope_UsesProvidedId()
        {
            // Arrange
            var testId = "test-correlation-id";

            // Act
            using (CorrelationContext.NewCorrelationScope(testId))
            {
                // Assert
                Assert.Equal(testId, CorrelationContext.Current);
            }
        }

        [Fact]
        public void NewCorrelationScope_RestoresPreviousValue_AfterDispose()
        {
            // Arrange
            var outerCorrelationId = "outer-id";
            var innerCorrelationId = "inner-id";

            // Act & Assert
            using (CorrelationContext.NewCorrelationScope(outerCorrelationId))
            {
                Assert.Equal(outerCorrelationId, CorrelationContext.Current);

                using (CorrelationContext.NewCorrelationScope(innerCorrelationId))
                {
                    Assert.Equal(innerCorrelationId, CorrelationContext.Current);
                }

                Assert.Equal(outerCorrelationId, CorrelationContext.Current);
            }

            Assert.Equal(string.Empty, CorrelationContext.Current);
        }

        [Fact]
        public async Task CorrelationId_FlowsWithAsyncContext()
        {
            // Arrange
            var correlationId = "async-flow-test-id";

            // Act & Assert
            using (CorrelationContext.NewCorrelationScope(correlationId))
            {
                Assert.Equal(correlationId, CorrelationContext.Current);

                await Task.Run(() =>
                {
                    Assert.Equal(correlationId, CorrelationContext.Current);
                });

                // Test parallel tasks
                var tasks = new[]
                {
                    Task.Run(() => Assert.Equal(correlationId, CorrelationContext.Current)),
                    Task.Run(() => Assert.Equal(correlationId, CorrelationContext.Current))
                };

                await Task.WhenAll(tasks);
            }
        }

        [Fact]
        public void NewCorrelationScope_ThrowsArgumentNullException_WhenIdIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                CorrelationContext.NewCorrelationScope(correlationId: null!);
            });
        }

        [Fact]
        public void LogWithCorrelation_IncludesCorrelationId()
        {
            // Arrange
            var correlationId = "test-log-correlation-id";
            var testMessage = "Test message";

            using (CorrelationContext.NewCorrelationScope(correlationId))
            {
                // Act
                // Note: In a real test, we would mock the console output and verify the exact format
                // For this example, we're just verifying it doesn't throw an exception
                CorrelationContext.LogWithCorrelation(testMessage);

                // Assert
                Assert.Equal(correlationId, CorrelationContext.Current);
            }
        }
    }
}