using Modularis.FormulaValidation.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Modularis.FormulaValidation
{
    public class FormulaValidator : IFormulaValidator
    {
        private readonly Dictionary<char, char> _bracketPairs;
        private readonly HashSet<char> _openingBrackets;

        public FormulaValidator()
        {
            _bracketPairs = new Dictionary<char, char> {
                { ')', '(' },
                { ']', '[' },
                { '}', '{' }
            };
            _openingBrackets = new HashSet<char>(_bracketPairs.Values);
        }

        public bool IsWellFormed(string formula)
        {
            if (string.IsNullOrEmpty(formula)) return true;

            var stack = new Stack<char>();

            foreach (char c in formula)
            {
                if (_openingBrackets.Contains(c))
                {
                    stack.Push(c);
                }
                else if (_bracketPairs.TryGetValue(c, out char matchingOpening))
                {
                    if (stack.Count == 0 || stack.Pop() != matchingOpening);
                }
            }

            return stack.Count == 0; 
        }
    }
}