using ShiftLogger.Brozda.Models;
using ShiftLogger.Brozda.UIConsole.Helpers;
using ShiftLogger.Brozda.UIConsole.InputOutput;
using ShiftLogger.Brozda.UIConsole.Interfaces.MenuActionHandlers;
using ShiftLogger.Brozda.UIConsole.Interfaces.Services;

namespace ShiftLogger.Brozda.UIConsole.MenuActionHandlers
{
    /// <inheritdoc/>
    internal class AssignedShiftsActionHandler : IAssignedShiftsActionHandler
    {
        private IAssignedShiftService _service;

        /// <inheritdoc/>
        public WorkerDto? SelectedWorker
        { get { return _selectedWorker; } }

        /// <inheritdoc/>
        private WorkerDto? _selectedWorker = null;

        /// <summary>
        /// Initializes new instande of AssignedShiftsActionHandler
        /// </summary>
        /// <param name="service">Service handling calls to a Rest API</param>
        public AssignedShiftsActionHandler(IAssignedShiftService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all shift assigned to workers for specific date.
        /// Gets shift for database and shows the result to the user
        /// </summary>
        public async Task ProcessViewAllForDate()
        {
            var date = AppInput.GetDate();

            var getAllForDateResult = await _service.GetShiftsForDate(date);

            ApiHelper.HandleResult(getAllForDateResult, AppConstants.ActionErrorGetAll, null, true);
        }

        /// <summary>
        /// Selects a worker required for further operations.
        /// Gets all workers from which user select the requested one assuring correctness of user input
        /// </summary>
        public async Task ProcessSelectWorker()
        {
            var allWorkers = await GetAllWorkers();

            if (allWorkers is null)
            {
                return;
            }

            _selectedWorker = GetWorker(allWorkers);
        }

        /// <summary>
        /// Gets all shift assigned to workers for specific date and prints them to user output
        /// Assures that worker is selected first - flow is interrupted and user informed in case it is not
        /// </summary>
        public async Task ProcessViewAllForWorker()
        {
            if (_selectedWorker is null)
            {
                AppOutput.PrintText(AppConstants.ShiftsWorkerNotSelected);
                return;
            }

            await GetAllShiftsForWorker(true);
        }

        /// <summary>
        /// Gets details for new shift from the user, performs API call. User is informed about success or any error.
        /// Assures that worker is selected first - flow is interrupted and user informed in case it is not
        /// </summary>
        public async Task ProcessAssignNewShift()
        {
            var newShift = await GetAssignedShiftFromUser();
            if (newShift is null) { return; }

            var assignNewShiftResult = await _service.Create(newShift);
            ApiHelper.HandleResult(assignNewShiftResult, AppConstants.ActionSucessCreate, AppConstants.ActionSucessCreate);
        }

        /// <summary>
        /// Gets existing assigned shift, updates it based on user input and persfroms an API call for an update. User is informed about success or any error.
        /// Assures that worker is selected first - flow is interrupted and user informed in case it is not
        /// </summary>
        /// <returns></returns>
        public async Task ProcessUpdateShiftForWorker()
        {
            if (_selectedWorker is null)
            {
                AppOutput.PrintText(AppConstants.ShiftsWorkerNotSelected);
                return;
            }

            var toBeUpdatedId = await GetSingleShiftId();
            if (toBeUpdatedId == AppConstants.CancelledID) { return; }

            var toBeUpdatedShift = await GetShiftById(toBeUpdatedId);
            if (toBeUpdatedShift is null) { return; }

            var newShift = await GetAssignedShiftFromUser(toBeUpdatedShift);
            if (newShift is null) { return; }

            newShift.Id = toBeUpdatedId;

            var updateResult = await _service.Edit(toBeUpdatedId, newShift);
            ApiHelper.HandleResult(updateResult, AppConstants.ActionErrorUpdate, AppConstants.ActionSuccessUpdate);
        }

        /// <summary>
        /// Gets existing assigned shift and performs an API call for a delete. User is informed about success or any error.
        /// Assures that worker is selected first - flow is interrupted and user informed in case it is not.
        /// </summary>
        public async Task ProcessDeleteShiftForWorker()
        {
            if (_selectedWorker is null)
            {
                AppOutput.PrintText(AppConstants.ShiftsWorkerNotSelected);
                return;
            }

            var toBeDeletedId = await GetSingleShiftId();
            if (toBeDeletedId == AppConstants.CancelledID) { return; }

            var deleteResult = await _service.Delete(toBeDeletedId);
            ApiHelper.HandleResult(deleteResult, AppConstants.ActionErrorGetAll, AppConstants.ActionSuccessDelete);
        }

        /// <summary>
        /// Gets all shifts for the worker.
        /// Assures that worker is selected first - flow is interrupted and user informed in case it is not.
        /// </summary>
        /// <param name="printShifts">Determines whether data should be displayed to the user</param>
        /// <returns> The task result contains a list of <see cref="AssignedShiftMappedDto"/></returns>
        private async Task<List<AssignedShiftMappedDto>?> GetAllShiftsForWorker(bool printShifts = false)
        {
            if (_selectedWorker is null)
            {
                return null;
            }

            var workerShiftsResult = await _service.GetShiftsForWorker(_selectedWorker.Id);

            var success = ApiHelper.HandleResult(workerShiftsResult, AppConstants.ActionErrorGetAll, null, printShifts);

            return success
                ? workerShiftsResult.Data
                : null;
        }

        /// <summary>
        /// Gets Id of currently assigned shift to the selected worker
        /// </summary>
        /// <returns>The task result contains an integer representing selected shift</returns>
        private async Task<int> GetSingleShiftId()
        {
            var shifts = await GetAllShiftsForWorker(true);
            if (shifts is null) { return AppConstants.CancelledID; }

            var validIds = shifts.Select(x => x.Id).ToList();
            var selectedId = AppInput.GetId(validIds, null);

            return selectedId;
        }

        /// <summary>
        /// Gets a shift type from user input
        /// </summary>
        /// <param name="currentId">Oprional parameter representing existing shift type used as default value</param>
        /// <returns>The task result contains an integer representing selected shift type</returns>
        private async Task<int> GetShiftType(int? currentId = null)
        {
            var getShiftResult = await _service.GetShifts();

            var success = ApiHelper.HandleResult(getShiftResult, AppConstants.ActionErrorGetAll, null, true);
            if (!success)
            {
                return AppConstants.CancelledID;
            }

            var validIds = getShiftResult.Data!.Select(x => x.Id).ToList();
            var userInput = AppInput.GetId(validIds, currentId);

            return userInput;
        }

        /// <summary>
        /// Gets a <see cref="AssignedShiftDto"/> based on values on user input
        /// </summary>
        /// <param name="currentShift">Option parameter representing exisitng assigned shift used as default value</param>
        /// <returns>The task result contains <see cref="AssignedShiftDto"/> in case of success; null will be returned in case of any issues</returns>
        private async Task<AssignedShiftDto?> GetAssignedShiftFromUser(AssignedShiftDto? currentShift = null)
        {
            if (_selectedWorker is null)
            {
                return null;
            }

            int shiftTypeId;
            DateTime date;

            if (currentShift is null)
            {
                shiftTypeId = await GetShiftType();
                if (shiftTypeId == AppConstants.CancelledID) { return null; }
                date = AppInput.GetDate();
            }
            else
            {
                shiftTypeId = await GetShiftType(currentShift.ShiftTypeId);
                if (shiftTypeId == AppConstants.CancelledID) { return null; }
                date = AppInput.GetDate(currentShift.Date);
            }

            return new AssignedShiftDto()
            {
                WorkerId = _selectedWorker.Id,
                ShiftTypeId = shiftTypeId,
                Date = date,
            };
        }

        /// <summary>
        /// Gets all workers from the database
        /// </summary>
        /// <returns>The task result contains List of <see cref="WorkerDto"/> in case of success; null will be returned in case of any issues</returns>
        private async Task<List<WorkerDto>?> GetAllWorkers()
        {
            var getWorkersResult = await _service.GetWorkers();

            if (!ApiHelper.HandleResult(getWorkersResult, AppConstants.ActionErrorGetAll, null, true))
            {
                return null;
            }
            else
            {
                return getWorkersResult.Data;
            }
        }

        /// <summary>
        /// Takes a list of existing workers from which user can select worker to be used for further operations
        /// </summary>
        /// <param name="allWorkers">List of existing workers </param>
        /// <returns>Valid <see cref="WorkerDto"/> object</returns>
        private WorkerDto GetWorker(List<WorkerDto> allWorkers)
        {
            var validIds = allWorkers.Select(x => x.Id).ToList();
            var userInput = AppInput.GetId(validIds, null, false);

            return allWorkers.Find(x => x.Id == userInput)!;
        }

        /// <summary>
        /// Gets a assigned shift from API based on provided id
        /// User is informed about any error encountered
        /// </summary>
        /// <param name="id">Id of existing assigned shift</param>
        /// <returns>The task result contains <see cref="AssignedShiftDto"/> in case of success; null will be returned in case of any issues</returns>
        private async Task<AssignedShiftDto?> GetShiftById(int id)
        {
            var getByIdResult = await _service.GetById(id);

            var success = ApiHelper.HandleResult(getByIdResult, AppConstants.ActionErrorGetAll);

            return success
                ? getByIdResult.Data
                : null;
        }
    }
}