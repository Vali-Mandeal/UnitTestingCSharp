using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class StackTests
    {
        //private Stack<string> _stack;
        //[SetUp]
        //public void SetUp()
        //{
        //    _stack = new Stack<string>();
        //}

        [Test]
        public void Push_ArgIsNull_ThrowsArgNullException()
        {
            var stack = new Stack<string>();
            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }
            
        [Test]
        public void Push_ValidArg_AddsValue()
        {
            var stack = new Stack<string>();

            stack.Push("a");

            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Count_EmptyStack_ReturnsZero()
        {
            var stack = new Stack<string>();

            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pop_EmptyStack_ThrowsInvalidOperationException()
        {
            var stack = new Stack<string>();

            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackWithAFewObjects_ReturnsObjectOnTheTop()
        {
            // Arrange
            var stack = new Stack<string>();

            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            // Act
            var result = stack.Pop();

            // Assert
            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void Pop_StackWithAFewObjects_RemovesObjectOnTheTop()
        {
            // Arrange
            var stack = new Stack<string>();

            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            // Act
            stack.Pop();

            Assert.That(stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_EmptyStack_ThrowsInvalidOperationException()
        {
            var stack = new Stack<string>();

            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackWithAFewObjects_ReturnsObjectOnTheTop()
        {
            // Arrange
            var stack = new Stack<string>();

            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            // Act
            var result = stack.Peek();

            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void Peek_StackWithAFewObjects_DoesNotRemoveObjectOnTheTop()
        {
            // Arrange
            var stack = new Stack<string>();
                
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            // Act
            stack.Peek();

            Assert.That(stack.Count, Is.EqualTo(3));
        }
    }
}   
    