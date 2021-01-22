using System;
using System.Linq;

namespace NeoGamesOutcomeCalculator
{
    class Program
    {
        static void Main()
        {
            //Example on how to use the OutcomeCalculator:
            
            var expressions = new string[]
            {
                "1+d4+1",
                "3*2+5",
                "d6",
                "d6+d6",
                "d3*d3"
            };

            foreach (var expresion in expressions)
            {
                Console.WriteLine($"Outcomes for \"{expresion}\":");
                var outcome = OutcomeCalculator.Calculate(expresion);

                Console.WriteLine(OutcomeCalculator.ToString(outcome
                    .OrderByDescending(a => a.Probability)
                    .ThenBy(a => a.Value)));
                Console.WriteLine();
            }
        }
    }
}
