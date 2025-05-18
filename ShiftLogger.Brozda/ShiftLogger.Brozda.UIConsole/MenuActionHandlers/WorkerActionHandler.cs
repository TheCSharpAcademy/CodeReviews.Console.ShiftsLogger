using ShiftLogger.Brozda.Models;
using ShiftLogger.Brozda.UIConsole.Helpers;
using ShiftLogger.Brozda.UIConsole.InputOutput;
using ShiftLogger.Brozda.UIConsole.Interfaces.MenuActionHandlers;
using ShiftLogger.Brozda.UIConsole.Interfaces.Services;

namespace ShiftLogger.Brozda.UIConsole.MenuActionHandlers
{
    internal class WorkerActionHandler : IWorkerActionHandler
    {
        private IApiService<WorkerDto> _service;

        public WorkerActionHandler(IApiService<WorkerDto> service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets values for new worker from user input and performs call to an API
        /// User is informed about success or error
        /// </summary>
        public async Task ProcessCreate()
        {
            var newWorker = AppInput.GetWorker();

            var createResult = await _service.Create(newWorker);

            ApiHelper.HandleResult(createResult, SharedConstants.ActionErrorCreate, SharedConstants.ActionSucessCreate);
        }

        /// <summary>
        /// Gets all existing Workers from an API and prints them to user output
        /// User is informed about any error
        /// </summary>
        public async Task ProcessViewAll()
        {
            var getAllResult = await _service.GetAll();
            ApiHelper.HandleResult(getAllResult, SharedConstants.ActionErrorGetAll, null, true);
        }

        /// <summary>
        /// Gets an id of record from user input and perform an API call to retrieve record with matching id
        /// User is informed about any error
        /// </summary>
        public async Task ProcessViewById()
        {
            int workerId = await GetRecordId();

            if (workerId == SharedConstants.CancelledID)
            { return; }

            var getByIdResult = await _service.GetById(workerId);

            ApiHelper.HandleResult(getByIdResult, SharedConstants.ActionErrorGetAll, null, true);
        }

        /// <summary>
        /// Gets an record to be updated from user, new values from user and performs an API call to update it
        /// Operation can be cancelled by user during any point
        /// User is informed about success or error
        /// </summary>
        public async Task ProcessEdit()
        {
            //get ID id of the record to be updated
            var toBeUpdatedId = await GetRecordId();
            if (toBeUpdatedId == SharedConstants.CancelledID) { return; }

            //get new entity with new values
            var updatedEntity = await GetUpdatedEntity(toBeUpdatedId);
            if (updatedEntity is null) { return; }

            //push to db
            var editResult = await _service.Edit(toBeUpdatedId, updatedEntity);
            ApiHelper.HandleResult(editResult, SharedConstants.ActionErrorUpdate, SharedConstants.ActionSuccessUpdate);
        }

        /// <summary>
        ///  Gets an record to be deleted from user and performs an API call to update it
        ///  Operation can be cancelled by user during any point
        ///  User is informed about success or error
        /// </summary>
        public async Task ProcessDelete()
        {
            var toBeDeletedId = await GetRecordId();
            if (toBeDeletedId == SharedConstants.CancelledID) { return; }

            var deleteResult = await _service.Delete(toBeDeletedId);

            ApiHelper.HandleResult(deleteResult, SharedConstants.ActionErrorDelete, SharedConstants.ActionSuccessDelete);
        }

        /// <summary>
        /// Gets an Id of single existing input from user input.
        /// </summary>
        /// <returns>The task result contains an integer representing id of existing record or default value representing choice to cancel the operation</returns>
        private async Task<int> GetRecordId()
        {
            var getAllResult = await _service.GetAll();

            if (!ApiHelper.HandleResult(getAllResult, SharedConstants.ActionErrorGetAll, null, true))
            { return SharedConstants.CancelledID; }

            //data cannot be empty here as null check is already in ApiHelper.HandleResult
            var validIds = getAllResult.Data!.Select(x => x.Id).ToList();

            return AppInput.GetId(validIds);
        }

        /// <summary>
        /// Gets a new values for <see cref="WorkerDto"/> entity from user input
        /// </summary>
        /// <param name="existingEntityId"></param>
        /// <returns>The task result contains valid <see cref="WorkerDto"/> object in case of success; null in case of any error</returns>
        private async Task<WorkerDto?> GetUpdatedEntity(int existingEntityId)
        {
            //get existing entity from DB
            var toBeUpdatedEntityResult = await _service.GetById(existingEntityId);
            if (!ApiHelper.HandleResult(toBeUpdatedEntityResult, SharedConstants.ActionErrorGetAll))
            { return null; }

            //get new entity
            var updatedEntity = AppInput.GetWorker(toBeUpdatedEntityResult.Data);

            //update new entity ID
            updatedEntity.Id = existingEntityId;

            return updatedEntity;
        }
    }
}