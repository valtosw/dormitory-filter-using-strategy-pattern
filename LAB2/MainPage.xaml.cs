using LAB2.Additional_Buttons_Functionality;
using LAB2.Attributes;
using LAB2.Strategies;
using System.Text;
using System.Xml;

namespace LAB2
{
    public partial class MainPage : ContentPage
    {
        private StrategyParser _strategyParser;
        private string _selectedFilePath;
        private Filter _filter = new Filter();
        private IEnumerable<Student.Student> _students;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoadFileClicked(object sender, EventArgs e)
        {
            try
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".xml" } },
                    { DevicePlatform.MacCatalyst, new[] { ".xml" } },
                    { DevicePlatform.iOS, new[] { "public.xml" } },
                    { DevicePlatform.Android, new[] { "application/xml" } }
                });

                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select an XML File",
                    FileTypes = customFileType
                });

                if (result is not null)
                {
                    _selectedFilePath = result.FullPath;

                    var options = OnLoadFileClickedClass.LoadOptionsFromXML(_selectedFilePath);

                    DynamicPickersContainer.Children.Clear();

                    CreatePicker("First Name", options.FirstNames);
                    CreatePicker("Last Name", options.LastNames);
                    CreatePicker("Faculty", options.Faculties);
                    CreatePicker("Cathedra", options.Cathedras);
                    CreatePicker("Course", options.Courses);
                    CreatePicker("Address", options.Addresses);
                    CreatePicker("Start Date", options.StartDates);
                    CreatePicker("End Date", options.EndDates);
                    CreatePicker("Room Number", options.RoomNumbers);

                    await DisplayAlert("XML Loaded", "XML file loaded successfully", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnAnalyzeClicked(object sender, EventArgs e)
        { 
            string? parser = ParserPicker.SelectedItem.ToString();

            switch (parser)
            {
                case "SAX API":
                    _strategyParser = new StrategyParser(new StrategySAXAPI());
                    break;
                case "DOM API":
                    _strategyParser = new StrategyParser(new StrategyDOMAPI());
                    break;
                case "LINQ to XML":
                    _strategyParser = new StrategyParser(new StrategyLINQtoXML());
                    break;
                default:
                    await DisplayAlert("Error", "Please select a parser", "OK");
                    return;
            }

            _students = _strategyParser.Parse(_selectedFilePath, _filter);
            ResultsLabel.Text = OnAnalyzeClickedClass.FormatResults(_students);
        }

        private async void OnTransformClicked(object sender, EventArgs e)
        {
            try
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".xls", ".xlsx" } },
                    { DevicePlatform.MacCatalyst, new[] { "com.microsoft.excel.xls", "org.openxmlformats.spreadsheetml.sheet" } },
                    { DevicePlatform.iOS, new[] { "com.microsoft.excel.xls", "org.openxmlformats.spreadsheetml.sheet" } },
                    { DevicePlatform.Android, new[] { "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" } }
                });

                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select an Excel",
                    FileTypes = customFileType
                });

                if (result is not null)
                {
                    string selectedFilePath = result.FullPath;

                    OnTransformClickedClass.WriteStudentsToExcel(_students, selectedFilePath);
                    OnTransformClickedClass.ConvertToHTML(selectedFilePath);
                    _students = Enumerable.Empty<Student.Student>();

                    await DisplayAlert("HTML formed", "HTML file formed successfully", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            ResultsLabel.Text = "Results will appear here...";
            ParserPicker.SelectedIndex = -1;
            _filter = new Filter();

            for (int i = 0; i < DynamicPickersContainer.Children.Count; i++)
            {
                if (DynamicPickersContainer.Children[i] is Picker picker)
                {
                    picker.SelectedIndex = -1;
                }
            }
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirmation", "Do you really want to exit?", "Yes", "No");
            if (answer)
            {
                System.Environment.Exit(0);
            }
        }

        private void CreatePicker(string title, List<string?> items)
        {

            var picker = new Picker
            {
                Title = $"Select {title}",
                FontSize = 14,
                ItemsSource = items
            };

            picker.SelectedIndexChanged += (sender, e) =>
            {
                var selectedPicker = (Picker)sender;

                if (selectedPicker.SelectedIndex < 0)
                    return;

                var selectedValue = selectedPicker.SelectedItem.ToString();

                switch (title)
                {
                    case "First Name":
                        _filter.FirstName = selectedValue;
                        break;
                    case "Last Name":
                        _filter.LastName = selectedValue;
                        break;
                    case "Faculty":
                        _filter.Faculty = selectedValue;
                        break;
                    case "Cathedra":
                        _filter.Cathedra = selectedValue;
                        break;
                    case "Course":
                        _filter.Course = selectedValue;
                        break;
                    case "Address":
                        _filter.Address = selectedValue;
                        break;
                    case "Start Date":
                        _filter.StartDate = selectedValue;
                        break;
                    case "End Date":
                        _filter.EndDate = selectedValue;
                        break;
                    case "Room Number":
                        _filter.RoomNumber = selectedValue;
                        break;
                }
            };

            DynamicPickersContainer.Children.Add(picker);
        }
    }

}
