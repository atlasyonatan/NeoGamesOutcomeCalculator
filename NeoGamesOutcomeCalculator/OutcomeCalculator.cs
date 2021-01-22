using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace NeoGamesOutcomeCalculator
{
    public static class OutcomeCalculator
    {
        private const string REG = @"[d]\d+";
        public static IEnumerable<(int Value, float Probability)> Calculate(string expression)
        {
            var d = Regex.Match(expression, REG);
            if (d.Success)
            {
                var upper = int.Parse(d.Value.Remove(0, 1));
                return Enumerable.Range(1, upper)
                    .SelectMany(i => Calculate(expression.Remove(d.Index, d.Length).Insert(d.Index, i.ToString())))
                    .GroupBy(r => r.Value)
                    .Select(g => (Value: g.Key, Probability: g.Sum(g => g.Probability) / upper));
            }
            else
            {
                var dt = new DataTable();
                return new[] { ((int)dt.Compute(expression, ""), 1f) };
            }
        }

        public static string ToString(this IEnumerable<(int Value, float Probability)> seq) => 
            string.Join('\n', seq.Select(i => $"{i.Value}\t{i.Probability:P}"));
    }
}
