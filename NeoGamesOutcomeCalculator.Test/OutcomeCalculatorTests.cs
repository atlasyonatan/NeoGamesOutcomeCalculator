using NUnit.Framework;
using System.Linq;

namespace NeoGamesOutcomeCalculator.Test
{
    public class OutcomeCalculatorTests
    {
        [TestCase(1)]
        [TestCase(13)]
        [TestCase(70)]
        public void Dice(int sides)
        {
            var expression = $"d{sides}";
            var outcome = OutcomeCalculator.Calculate(expression).ToArray();
            Assert.AreEqual(sides, outcome.Count());
            Assert.AreEqual(1, outcome.Sum(a => a.Probability));
        }

        [TestCase(new object[] { 1, 1, 1, 1, 1 })]
        [TestCase(new object[] { 1, 2, 3, 4, 5 })]
        public void AdditionDice(params int[] sidesArray)
        {
            var operation = '+';
            var expression = string.Join(operation, sidesArray.Select(s => $"d{s}"));
            var outcome = OutcomeCalculator.Calculate(expression).ToArray();
            Assert.That(outcome.All(a => 
                a.Value >= sidesArray.Length && 
                a.Value <= sidesArray.Sum()));
            Assert.AreEqual(1, outcome.Sum(a => a.Probability));
        }

        [TestCase(new object[] { 1, 1, 1, 1, 1 })]
        [TestCase(new object[] { 1, 2, 3, 4, 5 })]
        public void MultiplicationDice(params int[] sidesArray)
        {
            var operation = '*';
            var expression = string.Join(operation, sidesArray.Select(s => $"d{s}"));
            var outcome = OutcomeCalculator.Calculate(expression).ToArray();
            Assert.That(outcome.All(a =>
                a.Value >= 1 &&
                a.Value <= sidesArray.Aggregate(1, (a, b) => a * b)));
            Assert.AreEqual(1, outcome.Sum(a => a.Probability));
        }
    }
}