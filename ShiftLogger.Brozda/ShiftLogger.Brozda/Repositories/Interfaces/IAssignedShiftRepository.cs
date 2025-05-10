using ShiftLogger.Brozda.Models;

namespace ShiftLogger.Brozda.API.Repositories
{
    /// <summary>
    /// Defines operations to retreive/ store data to the Assigned shifts table
    /// </summary>
    public interface IAssignedShiftRepository
    {
        /// <summary>
        /// Inserts new record to the Assigned shifts table
        /// </summary>
        /// <param name="dto">Record to be inserted</param>
        /// <returns>Task resuls contains new model with assigned Id</returns>
        Task<AssignedShiftDto> Add(AssignedShiftDto dto);

        /// <summary>
        /// Deletes records in the Assigned shifts table
        /// </summary>
        /// <param name="id">Id of record to be deleted</param>
        /// <returns>Task resuls contains true is deletion was successful, false otherwise</returns>
        Task<bool> DeleteById(int id);

        /// <summary>
        /// Retrieves all records from Assigned shifts table
        /// </summary>
        /// <returns>Task result contains List of all <see cref="AssignedShiftDto"/> records in the Assigned shifts table</returns>
        Task<List<AssignedShiftDto>> GetAll();

        /// <summary>
        /// Retrieves specific record in Assigned shifts table
        /// </summary>
        /// <param name="id">Id of record to be retrieved</param>
        /// <returns>Task result contains <see cref="AssignedShiftDto"/> record if record with provided Id is present in the database; null otherwise</returns>
        Task<AssignedShiftDto?> GetById(int id);

        /// <summary>
        /// Retrieves all records assigned to spefic Worker from Assigned shifts table
        /// </summary>
        /// <param name="id">Id of the Worker </param>
        /// <returns>Task result contains List of all <see cref="AssignedShiftDto"/> records associated with provided Worker Id, null in case the Worker id is not present</returns>
        Task<List<AssignedShiftDto>> GetAllForWorker(int id);

        /// <summary>
        /// Updates existing record in the Assigned shifts table
        /// </summary>
        /// <param name="dto"><see cref="AssignedShiftDto"/> object containing new values</param>
        /// <param name="id">Id of the record to be updated</param>
        /// <returns>Task result contains <see cref="AssignedShiftDto"/> object with updated values if if update was sucessful; null otherwise</returns>
        Task<AssignedShiftDto?> Update(AssignedShiftDto dto, int id);

        /// <summary>
        /// Retrieves all records assigned to spefic Worker from Assigned shifts table
        /// </summary>
        /// <param name="id">Id of the Worker </param>
        /// <returns>Task result contains List of all <see cref="AssignedShiftMappedDto"/> records associated with provided Worker Id</returns>
        Task<List<AssignedShiftMappedDto>> GetAllForWorkerMapped(int id);

        /// <summary>
        /// Retrieves all records assigned to spefic Date from Assigned shifts table
        /// </summary>
        /// <param name="date"><see cref="DateTime"/> for which records will be retrieved </param>
        /// <returns>Task result contains List of all <see cref="AssignedShiftMappedDto"/> records associated with provided Date</returns>
        Task<List<AssignedShiftMappedDto>> GetShiftsForDateMapped(DateTime date);
    }
}