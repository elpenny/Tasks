﻿using NUnit.Framework;
using System;
using System.Linq.Expressions;
using MT_Task_Queuing.Services;

namespace MT_Task_Queuing.Tests
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

        [Test]
        public void TestSimpleAdd()
        {
            var result = _evaluator.Execute("0+0", false);

            Assert.That(result, Is.EqualTo("0"));
        }

        [Test]
        public void TestSimpleSubstract()
        {
            var result = _evaluator.Execute("0-0", false);

            Assert.That(result, Is.EqualTo("0"));
        }

        [Test]
        public void TestSimpleMultiply()
        {
            var result = _evaluator.Execute("0*0", false);

            Assert.That(result, Is.EqualTo("0"));
        }

        [Test]
        public void TestSimpleDivide()
        {
            var result = _evaluator.Execute("0/0", false);

            Assert.That(result, Is.EqualTo("Expression result was equal to divide by zero operation."));
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
