using ShiftsLogger.API.Models;
using System.Data;

namespace ShiftsLogger.API.Services;

public class ShiftDbService
{
    private readonly ApplicationDbContext _context;

    public ShiftDbService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Shift> GetAll()
    {
        List<Shift> shifts = _context.Shifts.ToList();
        return shifts;
    }

    public Shift Get(int id)
    {
        var shift = _context.Shifts.FirstOrDefault(x => x.Id == id);
        return shift;
    }

    public void Add(Shift shift)
    {
        _context.Shifts.Add(shift);
        _context.SaveChanges();
    }

    public void Update(Shift shift)
    {
        _context.Shifts.Update(shift);
        _context.SaveChanges();
    }









    private void PrintRows(DataSet dataSet)
    {
        // For each table in the DataSet, print the values of each row.
        foreach (DataTable thisTable in dataSet.Tables)
        {
            // For each row, print the values of each column.
            foreach (DataRow row in thisTable.Rows)
            {
                foreach (DataColumn column in thisTable.Columns)
                {

                }
            }
        }
    }

    private void AddARow(DataSet dataSet)
    {
        DataTable table;
        table = dataSet.Tables["Suppliers"];
        // Use the NewRow method to create a DataRow with
        // the table's schema.
        DataRow newRow = table.NewRow();

        // Set values in the columns:
        newRow["CompanyID"] = "NewCompanyID";
        newRow["CompanyName"] = "NewCompanyName";

        // Add the row to the rows collection.
        table.Rows.Add(newRow);
    }
}
