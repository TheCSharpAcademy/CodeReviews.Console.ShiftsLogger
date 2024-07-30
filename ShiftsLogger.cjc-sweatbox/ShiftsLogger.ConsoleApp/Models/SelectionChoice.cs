﻿namespace ShiftsLogger.ConsoleApp.Models;

/// <summary>
/// Used to display a menu choice that requires both a hidden ID and visible Name value.
/// </summary>
internal class SelectionChoice
{
    #region Properties

    public Guid Id { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    #endregion
}
