using LAB2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2.Strategies
{
    interface IParsingStrategy
    {
        IEnumerable<Student.Student> Parse(string path, Filter filter);
    }
}
