namespace ShiftLogger.Brozda.UIConsole.Interfaces.MenuActionHandlers
{
    /// <summary>
    ///  Defines the operations for handling workers.
    /// </summary>
    internal interface IWorkerActionHandler
    {
        /// <summary>
        /// Creates a new worker in the database
        /// </summary>
        /// <remarks>
        /// Method should handle process of getting values for new worker from user and performing a API call
        /// </remarks>
        Task ProcessCreate();

        /// <summary>
        /// Deletes existing worker from the database
        /// </summary>
        /// <remarks>
        /// Method should handle process of getting worker to be deleted details from user and performing a API call
        /// </remarks>
        Task ProcessDelete();

        /// <summary>
        /// Updates existing worker from in  database
        /// </summary>
        /// <remarks>
        /// Method should handle process of getting worker to be updated details from user and performing a API call
        /// </remarks>
        Task ProcessEdit();

        /// <summary>
        /// Show all existing workers in the database to the user
        /// </summary>
        /// <remarks>
        /// Method should handle process of getting all workers, performing a API call and show result to the user
        /// </remarks>
        Task ProcessViewAll();

        /// <summary>
        /// Show selected worker to the user
        /// </summary>
        /// <remarks>
        /// Method should handle process of getting detail about worker to be printed from user, performing a API call and show result to the user
        /// </remarks>
        Task ProcessViewById();
    }
}