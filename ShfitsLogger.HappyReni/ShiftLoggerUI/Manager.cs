using ShiftLoggerUI.Models;
using ShiftLoggerUI.Data;

namespace ShiftLoggerUI
{
    public class Manager
    {
        private UI Ui { get; set; }
        private SELECTOR Selector { get; set; }
        public Manager()
        {
            Ui = new UI();
            Selector = UI.MainMenu();

            while (true)
            {
                Action();
            }
        }

        private void Action()
        {
            switch (Selector)
            {
                case SELECTOR.CREATE:
                    CreateShift();
                    break;
                case SELECTOR.READ:
                    ReadShift();
                    break;
                case SELECTOR.UPDATE:
                    UpdateShift();
                    break;
                case SELECTOR.DELETE:
                    DeleteShift();
                    break;
                case SELECTOR.VIEWALL:
                    ViewAllShifts();
                    break;
                case SELECTOR.EXIT:
                    Environment.Exit(0);
                    break;
                default:
                    UI.Write("Invalid Input");
                    break;
            }
            Selector = Ui.GoToMainMenu("Type any keys to continue.");
        }

        private void CreateShift()
        {
            UI.Clear();
            try 
            {
                var name = UI.GetInput("Type a worker's name.").str;
                var startTime = Validation.CheckDateTime(UI.GetInput("Type a start time of work. (YYYY-MM-dd HH:mm:ss)").str);
                var endTime = Validation.CheckDateTime(UI.GetInput("Type a end time of work. (YYYY-MM-dd HH:mm:ss)").str);

                ShiftController.AddShift(new Shift() { Id = 0, Name = name, StartTime = startTime, EndTime = endTime });
            }
            catch(Exception ex)
            {
                UI.Write(ex.Message);
            }
            
        }

        private void ReadShift()
        {
            ViewAllShifts();
            try
            {
                var id = UI.GetInput("Type an ID to read.").val;
                UI.MakeTable(new List<Shift>() { ShiftController.GetShift(id).Result }, "Shift");
            }
            catch (Exception ex)
            {
                UI.Write(ex.Message);
            }
        }

        private async void UpdateShift()
        {
            ViewAllShifts();
            try
            {
                var id = UI.GetInput("Type an ID to update.").val;
                var name = UI.GetInput("Type new worker's name.").str;
                var startTime = Validation.CheckDateTime(UI.GetInput("Type a start time of work. (YYYY-MM-dd HH:mm:ss)").str);
                var endTime = Validation.CheckDateTime(UI.GetInput("Type a end time of work. (YYYY-MM-dd HH:mm:ss)").str);
                await ShiftController.UpdateShift(id,
                                new Shift() { Id = id, Name = name, StartTime = startTime, EndTime = endTime });
            } 
            catch(Exception ex)
            {
                UI.Write(ex.Message);
            }
        }

        private async void DeleteShift()
        {
            ViewAllShifts();
            try
            {
                var id = UI.GetInput("Type an ID to delete.").val;
                await ShiftController.DeleteShift(id);
            }
            catch (Exception ex)
            {
                UI.Write(ex.Message);
            }
        }

        private void ViewAllShifts()
        {
            try
            {
                var shifts = ShiftController.GetShifts().Result;
                UI.MakeTable(shifts, "Shifts");
            }
            catch (Exception ex)
            {
                UI.Write(ex.Message);
            }
            
        }
    }
}
