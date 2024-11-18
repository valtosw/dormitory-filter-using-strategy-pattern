using LAB2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LAB2.Strategies
{
    class StrategyLINQtoXML : IParsingStrategy
    {
        public IEnumerable<Student.Student> Parse(string path, Filter filter)
        {
            var doc = XDocument.Load(path);

            var students = doc.Descendants("Student").Select(student => new Student.Student
            {
                FirstName = (string)student.Attribute("FirstName") ?? (string)student.Element("FirstName"),
                LastName = (string)student.Attribute("LastName") ?? (string)student.Element("LastName"),
                Faculty = (string)student.Attribute("Faculty") ?? (string)student.Element("Faculty"),
                Cathedra = (string)student.Attribute("Cathedra") ?? (string)student.Element("Cathedra"),
                Course = (string)student.Attribute("Course") ?? (string)student.Element("Course"),
                Address = (string)student.Attribute("Address") ?? (string)student.Element("Address"),
                StartDate = (string)student.Attribute("StartDate") ?? (string)student.Element("StartDate"),
                EndDate = (string)student.Attribute("EndDate") ?? (string)student.Element("EndDate"),
                RoomNumber = (string)student.Attribute("RoomNumber") ?? (string)student.Element("RoomNumber")
            });

            return students.Where(student => Filter.IsMatch(student, filter));
        }
    }
}
