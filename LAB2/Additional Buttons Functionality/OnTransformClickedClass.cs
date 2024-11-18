using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2.Additional_Buttons_Functionality
{
    internal class OnTransformClickedClass
    {
        public static void WriteStudentsToExcel(IEnumerable<Student.Student> students, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Students");

                var headers = new string[]
                {
                    "FirstName", "LastName", "Faculty", "Cathedra", "Course",
                    "Address", "StartDate", "EndDate", "RoomNumber"
                };

                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                }

                int row = 2;
                foreach (var student in students)
                {
                    worksheet.Cells[row, 1].Value = student.FirstName;
                    worksheet.Cells[row, 2].Value = student.LastName;
                    worksheet.Cells[row, 3].Value = student.Faculty;
                    worksheet.Cells[row, 4].Value = student.Cathedra;
                    worksheet.Cells[row, 5].Value = student.Course;
                    worksheet.Cells[row, 6].Value = student.Address;
                    worksheet.Cells[row, 7].Value = student.StartDate;
                    worksheet.Cells[row, 8].Value = student.EndDate;
                    worksheet.Cells[row, 9].Value = student.RoomNumber;
                    row++;
                }

                package.SaveAs(new FileInfo(filePath));
            }
        }

        public static void ConvertToHTML(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                var html = "<table border='1'>";

                for (int row = 1; row <= rowCount; row++)
                {
                    html += "<tr>";
                    for (int col = 1; col <= colCount; col++)
                    {
                        var value = worksheet.Cells[row, col].Text;
                        html += $"<td>{value}</td>";
                    }
                    html += "</tr>";
                }

                html += "</table>";

                string htmlFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Students.html");
                File.WriteAllText(htmlFilePath, html);
            }
        }
    }
}
