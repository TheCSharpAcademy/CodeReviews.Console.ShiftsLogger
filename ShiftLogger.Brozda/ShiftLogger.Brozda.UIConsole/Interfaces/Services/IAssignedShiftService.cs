using ShiftLogger.Brozda.Models;
using ShiftLogger.Brozda.UIConsole.Models;

namespace ShiftLogger.Brozda.UIConsole.Interfaces.Services
{
    /// <summary>
    /// Extends the generic <see cref="IApiService{T}"/> interface with specific operations
    /// related to assigned shifts and their associated data.
    /// </summary>
    internal interface IAssignedShiftService : IApiService<AssignedShiftDto>
    {
        /// <summary>
        /// Retrieves all available shift types from the API.
        /// </summary>
        /// <returns>
        /// An <see cref="ApiResult{List{ShiftTypeDto}}"/> containing the list of shift types or error information.
        /// </returns>
        Task<ApiResult<List<ShiftTypeDto>>> GetShifts();

        /// <summary>
        /// Retrieves all assigned shifts for a specific date.
        /// </summary>
        /// <param name="date">The date for which to retrieve assigned shifts.</param>
        /// <returns>
        /// An <see cref="ApiResult{List{AssignedShiftMappedDto}}"/> containing the list of assigned shifts on the given date.
        /// </returns>
        Task<ApiResult<List<AssignedShiftMappedDto>>> GetShiftsForDate(DateTime date);

        /// <summary>
        /// Retrieves all assigned shifts for a specific worker.
        /// </summary>
        /// <param name="id">The ID of the worker whose shifts should be retrieved.</param>
        /// <returns>
        /// An <see cref="ApiResult{List{AssignedShiftMappedDto}}"/> containing the list of assigned shifts for the worker.
        /// </returns>
        Task<ApiResult<List<AssignedShiftMappedDto>>> GetShiftsForWorker(int id);

        /// <summary>
        /// Retrieves all workers from the API.
        /// </summary>
        /// <returns>
        /// An <see cref="ApiResult{List{WorkerDto}}"/> containing the list of workers or error information.
        /// </returns>
        Task<ApiResult<List<WorkerDto>>> GetWorkers();
    }
}