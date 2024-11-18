using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2.Attributes
{
    internal class AttributesOptions
    {
        public List<string?> FirstNames { get; set; } = new();
        public List<string?> LastNames { get; set; } = new();
        public List<string?> Faculties { get; set; } = new();
        public List<string?> Cathedras { get; set; } = new();
        public List<string?> Courses { get; set; } = new();
        public List<string?> Addresses { get; set; } = new();
        public List<string?> StartDates { get; set; } = new();
        public List<string?> EndDates { get; set; } = new();
        public List<string?> RoomNumbers { get; set; } = new();
    }
}
