using NUnit.Framework;
using System.Text.RegularExpressions;
using MT_Task_Queuing.Config;
using System;

namespace MT_Task_Queuing.Services.ExpressionGenerators.Tests
{
    [TestFixture()]
    public class ExpressionGeneratorTests
    {
        private Regex _mathExpressionRegex;
        private ExpressionGenerator _generator;

        [OneTimeSetUp]
        public void SetUp()
        {
            _mathExpressionRegex = new Regex("^([-+/*]\\d+(\\.\\d+)?)*", RegexOptions.Compiled);
            _generator = new ExpressionGenerator(new Random().Next(), new Configuration());
        }

        [Test]
        [Repeat(10000)]
        public void GenerateExpressionTest()
        {
            var expression = _generator.GenerateExpression();

            MatchCollection matches = _mathExpressionRegex.Matches(expression);

            Assert.That(matches.Count, Is.EqualTo(1));
        }
    }
}