using NUnit.Framework;
using System;
using System.Linq.Expressions;
using MT_Task_Queuing.Services;

namespace MT_Task_Queuing.Services.Tests
{
    [TestFixture]
    public class ExpressionEvaluatorTest
    {
        private ExpressionEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new ExpressionEvaluator();
        }

        [TestCase("0+0")]
        [TestCase("0-0")]
        [TestCase("0*0")]
        public void TestZeroSimpleOperations(string expression)
        {
            var result = _evaluator.Execute(expression, false);

            Assert.That(result, Is.EqualTo("0"));
        }

        [Test]
        public void TestDivideByZero()
        {
            var result = _evaluator.Execute("0/0", false);

            Assert.That(result, Is.EqualTo("Expression result was equal to divide by zero operation."));
        }

        [Test]
        public void TestSimpleAdd()
        {
            var result = _evaluator.Execute("1+1", false);

            Assert.That(result, Is.EqualTo("2"));
        }

        [Test]
        public void TestSimpleSubstract()
        {
            var result = _evaluator.Execute("1-1", false);

            Assert.That(result, Is.EqualTo("0"));
        }

        [TestCase("1*1")]
        [TestCase("1/1")]
        public void TestSimpleMultiplyAndDivide(string expression)
        {
            var result = _evaluator.Execute(expression, false);

            Assert.That(result, Is.EqualTo("1"));
        }

        [Test]
        public void TestSimpleDivide()
        {
            var result = _evaluator.Execute("0/0", false);

            Assert.That(result, Is.EqualTo("Expression result was equal to divide by zero operation."));
        }

        [Test]
        public void TestCorrectOrder()
        {
            var result = _evaluator.Execute("2+2*2", false);

            Assert.That(result, Is.EqualTo("6"));
        }

        [Test]
        public void TestBrackets()
        {
            var result = _evaluator.Execute("(2+2)*2", false);

            Assert.That(result, Is.EqualTo("8"));
        }
        

        [Test]
        public void TestComplicatedExpression()
        {
            var result = _evaluator.Execute("2+2*2+2-10*-1+3*6", false);

            Assert.That(result, Is.EqualTo("36"));
        }

        [Test]
        public void TestComplicatedExpressionWithBrackets()
        {
            var result = _evaluator.Execute("(2+2)*(2+2)-10*(-1+(3*6))", false);

            Assert.That(result, Is.EqualTo("-154"));
        }

        [Test]
        public void TestDoubleOverflow()
        {
            var result = _evaluator.Execute($"{Double.MaxValue}+1", false);

            Assert.That(result, Is.EqualTo("Expression result was too big or too small to store it."));
        }

        [Test]
        public void TestDoubleUnderflow()
        {
            var result = _evaluator.Execute($"{Double.MinValue}-1", false);

            Assert.That(result, Is.EqualTo("Expression result was too big or too small to store it."));
        }
    }
}
