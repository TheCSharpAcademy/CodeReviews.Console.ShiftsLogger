using ShiftLogger.Brozda.Models;

namespace ShiftLogger.Brozda.API.Repositories
{
    public interface IWorkerRepository
    {
        /// <summary>
        /// Inserts new record to the Workers table
        /// </summary>
        /// <param name="dto">Model to be inserted</param>
        /// <returns>Task resuls contains new model with assigned Id</returns>
        Task<WorkerDto> Add(WorkerDto dto);

        /// <summary>
        /// Deletes record in the Workers table
        /// </summary>
        /// <param name="id">Id of record to be deleted</param>
        /// <returns>Task resuls contains true is deletion was successful, false otherwise</returns>
        Task<bool> DeleteById(int id);

        /// <summary>
        /// Retrieves all records from Workers table
        /// </summary>
        /// <returns>Task result contains List of all <see cref="WorkerDto"/> records in the Workers table</returns>
        Task<List<WorkerDto>> GetAll();

        /// <summary>
        /// Retrieves specific record in Workers table
        /// </summary>
        /// <param name="id">Id of record to be retrieved</param>
        /// <returns>Task result contains <see cref="WorkerDto"/> record if record with provided Id is present in the database; null otherwise</returns>
        Task<WorkerDto?> GetById(int id);

        /// <summary>
        /// Confirms whether record with provided Id is present in the Workers table
        /// </summary>
        /// <returns>Task resuls contains true is is the record is present, false otherwise</returns>
        Task<bool> DoesWorkerExist(int id);

        /// <summary>
        /// Updates existing record in the Workers table
        /// </summary>
        /// <param name="dto"><see cref="WorkerDto"/> object containing new values</param>
        /// <param name="id">Id of the record to be updated</param>
        /// <returns>Task result contains <see cref="WorkerDto"/> object with updated values if if update was sucessful; null otherwise</returns>
        Task<WorkerDto?> Update(WorkerDto dto, int id);
    }
}