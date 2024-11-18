using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2.Additional_Buttons_Functionality
{
    internal class OnAnalyzeClickedClass
    {
        public static string FormatResults(IEnumerable<Student.Student> students)
        {
            var sb = new StringBuilder();
            foreach (var student in students)
            {
                sb.AppendLine($"First Name: {student.FirstName}");
                sb.AppendLine($"Last Name: {student.LastName}");
                sb.AppendLine($"Faculty: {student.Faculty}");
                sb.AppendLine($"Cathedra: {student.Cathedra}");
                sb.AppendLine($"Course: {student.Course}");
                sb.AppendLine($"Address: {student.Address}");
                sb.AppendLine($"Start Date: {student.StartDate}");
                sb.AppendLine($"End Date: {student.EndDate}");
                sb.AppendLine($"Room Number: {student.RoomNumber}");
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
