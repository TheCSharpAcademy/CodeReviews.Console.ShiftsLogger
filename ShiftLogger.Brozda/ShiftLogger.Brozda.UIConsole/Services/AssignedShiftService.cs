using RestSharp;
using ShiftLogger.Brozda.Models;
using ShiftLogger.Brozda.UIConsole.Helpers;
using ShiftLogger.Brozda.UIConsole.Interfaces.Services;
using ShiftLogger.Brozda.UIConsole.Models;

namespace ShiftLogger.Brozda.UIConsole.Services
{
    /// <summary>
    /// An API service used for API calls regarding <see cref="AssignedShiftMappedDto"/>
    /// Inherits from <see cref="ApiResult{AssignedShiftDto}"/>
    /// Implements IAssignedShiftService
    /// </summary>
    internal class AssignedShiftService : ApiService<AssignedShiftDto>, IAssignedShiftService
    {
        /// <summary>
        /// Initializes new instance of AssignedShiftService
        /// </summary>
        /// <param name="client">Rest client used for an API call</param>
        /// <param name="baseUrl">Base url for API requests</param>
        public AssignedShiftService(RestClient client, string baseUrl) : base(client, baseUrl) { }

        /// <inheritdoc/>
        public async Task<ApiResult<List<AssignedShiftMappedDto>>> GetShiftsForWorker(int id)
        {
            var request = new RestRequest($"{BaseUrl}/worker/{id}", Method.Get);
            return await ApiHelper.ExecuteSafe<List<AssignedShiftMappedDto>>(Client, request);
        }

        /// <inheritdoc/>
        public async Task<ApiResult<List<AssignedShiftMappedDto>>> GetShiftsForDate(DateTime date)
        {
            var request = new RestRequest($"{BaseUrl}/date/{date.ToString("yyyy-MM-dd")}", Method.Get);
            return await ApiHelper.ExecuteSafe<List<AssignedShiftMappedDto>>(Client, request);
        }

        /// <inheritdoc/>
        public async Task<ApiResult<List<WorkerDto>>> GetWorkers()
        {
            var request = new RestRequest($"api/workers/", Method.Get);
            return await ApiHelper.ExecuteSafe<List<WorkerDto>>(Client, request);
        }

        /// <inheritdoc/>
        public async Task<ApiResult<List<ShiftTypeDto>>> GetShifts()
        {
            var request = new RestRequest($"/api/shifttypes", Method.Get);
            return await ApiHelper.ExecuteSafe<List<ShiftTypeDto>>(Client, request);
        }
    }
}