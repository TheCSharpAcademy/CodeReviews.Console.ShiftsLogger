using ShiftLogger.Brozda.Models;
using ShiftLogger.Brozda.UIConsole.Helpers;
using ShiftLogger.Brozda.UIConsole.InputOutput;
using ShiftLogger.Brozda.UIConsole.Interfaces.MenuActionHandlers;
using ShiftLogger.Brozda.UIConsole.Interfaces.Services;

namespace ShiftLogger.Brozda.UIConsole.MenuActionHandlers
{
    /// <inheritdoc/>
    internal class ShiftTypesActionHandler : IShiftTypesActionHandler
    {
        private IApiService<ShiftTypeDto> _service;

        /// <summary>
        /// Initializes new instance of ShiftTypesActionHandler
        /// </summary>
        /// <param name="service">An <see cref="IApiService{ShiftTypeDto}"/> handling requests to an API</param>
        public ShiftTypesActionHandler(IApiService<ShiftTypeDto> service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets values for new shift type from user input and performs call to an API
        /// User is informed about success or error
        /// </summary>
        public async Task ProcessCreate()
        {
            var newWorker = AppInput.GetShiftType();

            var createResult = await _service.Create(newWorker);

            ApiHelper.HandleResult(createResult, SharedConstants.ActionErrorCreate, SharedConstants.ActionSucessCreate);
        }

        /// <summary>
        /// Gets all existing Shift types from an API and prints them to user output
        /// User is informed about any error
        /// </summary>
        public async Task ProcessViewAll()
        {
            var getAllResult = await _service.GetAll();

            if (getAllResult.Data is not null)
            {
                getAllResult.Data = GetMappedResult(getAllResult.Data);
            }

            ApiHelper.HandleResult(getAllResult, SharedConstants.ActionErrorGetAll, null, true);
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

            if (getAllResult.Data is not null)
            {
                getAllResult.Data = GetMappedResult(getAllResult.Data);
            }

            if (!ApiHelper.HandleResult(getAllResult, SharedConstants.ActionErrorGetAll, null, true))
            {
                return SharedConstants.CancelledID;
            }

            var mappedIds = getAllResult.Data!
                .Select(x => x.DisplayId)
                .ToList();

            var userChoice = AppInput.GetId(mappedIds);

            return userChoice == SharedConstants.CancelledID
                ? userChoice
                : getAllResult.Data!.Find(x => x.DisplayId == userChoice)!.Id;
        }

        /// <summary>
        /// Gets a new values for <see cref="ShiftTypeDto"/> entity from user input
        /// </summary>
        /// <param name="existingEntityId"></param>
        /// <returns>The task result contains valid <see cref="ShiftTypeDto"/> object in case of success; null in case of any error</returns>
        private async Task<ShiftTypeDto?> GetUpdatedEntity(int existingEntityId)
        {
            //get existing entity from DB
            var toBeUpdatedEntityResult = await _service.GetById(existingEntityId);
            if (!ApiHelper.HandleResult(toBeUpdatedEntityResult, SharedConstants.ActionErrorGetAll))
            { return null; }

            //get new entity
            var updatedEntity = AppInput.GetShiftType(toBeUpdatedEntityResult.Data);

            //update new entity ID
            updatedEntity.Id = existingEntityId;

            return updatedEntity;
        }

        private List<ShiftTypeDto> GetMappedResult(List<ShiftTypeDto> result)
        {
            return result.
                Select((x, displayId) =>
                {
                    x.DisplayId = displayId + 1;
                    return x;
                })
                .ToList();
        }
    }
}