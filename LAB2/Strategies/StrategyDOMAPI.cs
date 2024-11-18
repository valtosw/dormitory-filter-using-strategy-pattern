using LAB2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LAB2.Strategies
{
    class StrategyDOMAPI : IParsingStrategy
    {
        public IEnumerable<Student.Student> Parse(string path, Filter filter)
        {
            var students = new List<Student.Student>();
            var doc = new XmlDocument();
            doc.Load(path);

            XmlNodeList studentNodes = doc.SelectNodes("//Student");

            foreach (XmlNode studentNode in studentNodes)
            {
                var student = new Student.Student
                {
                    FirstName = studentNode.Attributes["FirstName"]?.Value ?? studentNode["FirstName"]?.InnerText,
                    LastName = studentNode.Attributes["LastName"]?.Value ?? studentNode["LastName"]?.InnerText,
                    Faculty = studentNode.Attributes["Faculty"]?.Value ?? studentNode["Faculty"]?.InnerText,
                    Cathedra = studentNode.Attributes["Cathedra"]?.Value ?? studentNode["Cathedra"]?.InnerText,
                    Course = studentNode.Attributes["Course"]?.Value ?? studentNode["Course"]?.InnerText,
                    Address = studentNode.Attributes["Address"]?.Value ?? studentNode["Address"]?.InnerText,
                    StartDate = studentNode.Attributes["StartDate"]?.Value ?? studentNode["StartDate"]?.InnerText,
                    EndDate = studentNode.Attributes["EndDate"]?.Value ?? studentNode["EndDate"]?.InnerText,
                    RoomNumber = studentNode.Attributes["RoomNumber"]?.Value ?? studentNode["RoomNumber"]?.InnerText
                };

                if (Filter.IsMatch(student, filter))
                {
                    students.Add(student);
                }
            }

            return students;
        }
    }
}
