using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front_App.Models;

public enum MenuChoices
{
    [Description("Show all shifts")]
    ShowShifts,

    [Description("Add a new shift")]
    AddShift,

    [Description("Edit an existing shift")]
    EditShift,

    [Description("Delete shift")]
    DeleteShift,

    Exit
}