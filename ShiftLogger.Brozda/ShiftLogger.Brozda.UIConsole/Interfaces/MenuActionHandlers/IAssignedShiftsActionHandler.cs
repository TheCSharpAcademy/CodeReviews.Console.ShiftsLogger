using ShiftLogger.Brozda.Models;

namespace ShiftLogger.Brozda.UIConsole.Interfaces.MenuActionHandlers
{
    /// <summary>
    ///  Defines the operations for handling assigned shifts for a workers.
    /// </summary>
    internal interface IAssignedShiftsActionHandler
    {
        /// <summary>
        /// Gets a  property referencing worker selected by user for operations
        /// </summary>
        WorkerDto? SelectedWorker { get; }

        /// <summary>
        /// Assigns a new shift to the selected worker.
        /// </summary>
        /// <remarks>
        /// Method should handle process of assigning of new shift based on selected worker such as geting values from user and performing the API call
        /// Operation should be interrupted in case that worker for operation was not selected yet
        /// </remarks>
        Task ProcessAssignNewShift();

        /// <summary>
        /// Deletes a shift currently assigned to the selected worker.
        /// </summary>
        /// <remarks>
        /// Method should handle process of getting the shift to be deleted from user and performing the API call
        /// Operation should be interrupted in case that worker for operation was not selected yet
        /// </remarks>
        Task ProcessDeleteShiftForWorker();

        /// <summary>
        /// Selects a worker required for further operations.
        /// </summary>
        /// <remarks>
        /// Method should handle process of getting the worker from the user and pulling model from the database
        /// </remarks>
        Task ProcessSelectWorker();

        /// <summary>
        /// Updates a shift currently assigned to the selected worker.
        /// </summary>
        /// <remarks>
        /// Method should handle process of getting the shift to be updated from user, getting new values and performing the API call
        /// Operation should be interrupted in case that worker for operation was not selected yet
        /// </remarks>
        Task ProcessUpdateShiftForWorker();

        /// <summary>
        /// Gets all shift assigned to workers for specific date.
        /// </summary>
        /// <remarks>
        /// Method should handle process of getting the date from user, performing the API call and show result to user
        /// </remarks>
        Task ProcessViewAllForDate();

        /// <summary>
        /// Gets all shift assigned to selected worker.
        /// </summary>
        /// <remarks>
        /// Method should handle process of performing the API call and show result to user
        /// Operation should be interrupted in case that worker for operation was not selected yet
        /// </remarks>
        Task ProcessViewAllForWorker();
    }
}