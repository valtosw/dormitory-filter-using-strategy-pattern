using LAB2.Attributes;
using LAB2.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LAB2.Strategies
{
    class StrategySAXAPI : IParsingStrategy
    {
        public IEnumerable<Student.Student> Parse(string path, Filter filter)
        {
            List<Student.Student> students = new();
            Student.Student currentStudent = null;
            string currentElement = null;

            using (XmlReader reader = XmlReader.Create(path))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "Student")
                            {
                                currentStudent = new Student.Student
                                {
                                    FirstName = reader.GetAttribute("FirstName"),
                                    LastName = reader.GetAttribute("LastName"),
                                    Faculty = reader.GetAttribute("Faculty"),
                                    Cathedra = reader.GetAttribute("Cathedra"),
                                    Course = reader.GetAttribute("Course"),
                                    Address = reader.GetAttribute("Address"),
                                    StartDate = reader.GetAttribute("StartDate"),
                                    EndDate = reader.GetAttribute("EndDate"),
                                    RoomNumber = reader.GetAttribute("RoomNumber")
                                };
                            }
                            else
                            {
                                currentElement = reader.Name;
                            }
                            break;

                        case XmlNodeType.Text:
                            if (currentStudent != null && currentElement != null)
                            {
                                switch (currentElement)
                                {
                                    case "FirstName":
                                        currentStudent.FirstName = reader.Value;
                                        break;
                                    case "LastName":
                                        currentStudent.LastName = reader.Value;
                                        break;
                                    case "Faculty":
                                        currentStudent.Faculty = reader.Value;
                                        break;
                                    case "Cathedra":
                                        currentStudent.Cathedra = reader.Value;
                                        break;
                                    case "Course":
                                        currentStudent.Course = reader.Value;
                                        break;
                                    case "Address":
                                        currentStudent.Address = reader.Value;
                                        break;
                                    case "StartDate":
                                        currentStudent.StartDate = reader.Value;
                                        break;
                                    case "EndDate":
                                        currentStudent.EndDate = reader.Value;
                                        break;
                                    case "RoomNumber":
                                        currentStudent.RoomNumber = reader.Value;
                                        break;
                                }
                            }
                            break;

                        case XmlNodeType.EndElement:
                            if (reader.Name == "Student" && currentStudent is not null && Filter.IsMatch(currentStudent, filter))
                            {
                                students.Add(currentStudent);
                                currentStudent = null;
                            }
                            break;
                    }
                }
            }

            return students;
        }
    }
}
