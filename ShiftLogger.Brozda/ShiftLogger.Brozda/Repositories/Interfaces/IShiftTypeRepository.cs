using ShiftLogger.Brozda.Models;

namespace ShiftLogger.Brozda.API.Repositories
{
    public interface IShiftTypeRepository
    {
        /// <summary>
        /// Inserts new record to the Shift Types table
        /// </summary>
        /// <param name="dto">Model to be inserted</param>
        /// <returns>Task resuls containt new model with assigned Id</returns>
        Task<ShiftTypeDto> Add(ShiftTypeDto dto);

        /// <summary>
        /// Deletes records in the Shift Types table
        /// </summary>
        /// <param name="id">Id of record to be deleted</param>
        /// <returns>Task resuls contains true is deletion was successful, false otherwise</returns>
        Task<bool> DeleteById(int id);

        /// <summary>
        /// Retrieves all records from Shift Types table
        /// </summary>
        /// <returns>Task result contains List of <see cref="ShiftTypeDto"/> records in the Shift Types table</returns>
        Task<List<ShiftTypeDto>> GetAll();

        /// <summary>
        /// Retrieves specific record in Shift Types table
        /// </summary>
        /// <param name="id">Id of record to be retrieved</param>
        /// <returns>Task result contains <see cref="ShiftTypeDto"/> record if record with provided Id is present in the database; null otherwise</returns>
        Task<ShiftTypeDto?> GetById(int id);

        /// <summary>
        /// Updates existing record in the Shift Types table
        /// </summary>
        /// <param name="dto"><see cref="ShiftTypeDto"/> object containing new values</param>
        /// <param name="id">Id of the record to be updated</param>
        /// <returns>Task result contains <see cref="ShiftTypeDto"/> object with updated values if if update was sucessful; null otherwise</returns>
        Task<ShiftTypeDto?> Update(ShiftTypeDto dto, int id);
    }
}