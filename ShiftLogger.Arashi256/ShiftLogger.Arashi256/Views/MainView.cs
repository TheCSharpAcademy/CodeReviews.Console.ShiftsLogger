using ShiftLogger_Frontend.Arashi256.Classes;
using ShiftLogger_Frontend.Arashi256.Services;
using Spectre.Console;

namespace ShiftLogger_Frontend.Arashi256.Views
{
    internal class MainView
    {
        private const int QUIT_APPLICATION_OPTION_NUM = 3;
        private Table _tblMainMenu;
        private string _appTitle = "SHIFTLOGGER";
        private FigletText _figletAppTitle;
        private string[] _menuOptions =
        {
            "Worker Operations",
            "Worker Shift Operations",
            "Quit application"
        };
        ShiftService _shiftService;
        WorkerView _workerView;
        ShiftView _shiftView;

        public MainView()
        {
            _figletAppTitle = new FigletText(_appTitle);
            _figletAppTitle.Centered();
            _figletAppTitle.Color = Color.LightSlateGrey;
            _tblMainMenu = new Table();
            _tblMainMenu.AddColumn(new TableColumn("[steelblue]CHOICE[/]").Centered());
            _tblMainMenu.AddColumn(new TableColumn("[steelblue]OPTION[/]").LeftAligned());
            for (int i = 0; i < _menuOptions.Length; i++)
            {
                _tblMainMenu.AddRow($"[white]{i + 1}[/]", $"[aqua]{_menuOptions[i]}[/]");
            }
            _tblMainMenu.Alignment(Justify.Center);
            try
            {
                _shiftService = new ShiftService();
                _workerView = new WorkerView(_shiftService);
                _shiftView = new ShiftView(_shiftService, _workerView);
            } 
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DisplayView()
        {
            int selectedValue = 0;
            do
            {
                Console.Clear();
                AnsiConsole.Write(_figletAppTitle);
                AnsiConsole.Write(new Text("M A I N   M E N U").Centered());
                AnsiConsole.Write(_tblMainMenu);
                selectedValue = CommonUI.MenuOption($"Enter a value between 1 and {_menuOptions.Length}: ", 1, _menuOptions.Length);
                ProcessMainMenu(selectedValue);
            } while (selectedValue != QUIT_APPLICATION_OPTION_NUM);
            AnsiConsole.MarkupLine("[lime]Goodbye![/]");
        }

        private void ProcessMainMenu(int option)
        {
            AnsiConsole.Markup($"[lightslategrey]Menu option selected: {option}[/]\n");
            switch (option)
            {
                case 1:
                    // Workers view.
                    Workers();
                    break;
                case 2:
                    // Update an existing contact.
                    Shifts();
                    break;
            }
        }

        private void Workers()
        {
            _workerView.DisplayViewMenu();
        }

        private void Shifts()
        {
            _shiftView.DisplayViewMenu();
        }
    }
}
