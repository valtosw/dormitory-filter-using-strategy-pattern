using LAB2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2.Strategies
{
    internal class StrategyParser
    {
        private IParsingStrategy _strategy;

        public StrategyParser(IParsingStrategy strategy)
        {
            _strategy = strategy;
        }

        public IEnumerable<Student.Student> Parse(string path, Filter filter)
        {
            return _strategy.Parse(path, filter);
        }
    }
}
