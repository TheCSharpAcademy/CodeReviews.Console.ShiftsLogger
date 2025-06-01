using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front_App;

public interface IShiftClient
{
    Task<List<Shift>> GetShiftsAsync();

    Task CreateShiftAsync(Shift shift);

    Task UpdateShiftAsync(Shift shift);

    Task DeleteShiftAsync(int id);
}