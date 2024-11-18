using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2.Attributes
{
    internal class Filter : Student.Student
    {
        public static bool IsMatch(Student.Student student, Filter filter)
        {
            var filters = new (string? FilterValue, string? StudentValue)[]
            {
                (filter.FirstName, student.FirstName),
                (filter.LastName, student.LastName),
                (filter.Faculty, student.Faculty),
                (filter.Cathedra, student.Cathedra),
                (filter.Course, student.Course),
                (filter.Address, student.Address),
                (filter.StartDate, student.StartDate),
                (filter.EndDate, student.EndDate),
                (filter.RoomNumber, student.RoomNumber)
            };

            return filters.All(pair =>
                string.IsNullOrEmpty(pair.FilterValue) ||
                string.Equals(pair.FilterValue, pair.StudentValue, StringComparison.OrdinalIgnoreCase));
        }
    }
}
