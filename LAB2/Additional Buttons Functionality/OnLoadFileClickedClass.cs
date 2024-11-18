using LAB2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LAB2.Additional_Buttons_Functionality
{
    internal class OnLoadFileClickedClass
    {
        public static AttributesOptions LoadOptionsFromXML(string xmlPath)
        {
            var options = new AttributesOptions();
            var doc = XDocument.Load(xmlPath);

            static List<string?> ExtractValues(XDocument doc, string elementName)
            {
                return doc.Descendants("Student")
                          .SelectMany(student => new[] 
                          { 
                              student.Attribute(elementName)?.Value,
                              student.Element(elementName)?.Value 
                          })
                          .Where(value => !string.IsNullOrEmpty(value))
                          .Distinct()
                          .ToList();
            }

            options.FirstNames = ExtractValues(doc, "FirstName");
            options.LastNames = ExtractValues(doc, "LastName");
            options.Faculties = ExtractValues(doc, "Faculty");
            options.Cathedras = ExtractValues(doc, "Cathedra");
            options.Courses = ExtractValues(doc, "Course");
            options.Addresses = ExtractValues(doc, "Address");
            options.StartDates = ExtractValues(doc, "StartDate");
            options.EndDates = ExtractValues(doc, "EndDate");
            options.RoomNumbers = ExtractValues(doc, "RoomNumber");

            return options;
        }
    }
}
