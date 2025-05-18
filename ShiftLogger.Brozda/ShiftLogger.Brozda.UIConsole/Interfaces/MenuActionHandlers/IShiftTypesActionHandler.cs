namespace ShiftLogger.Brozda.UIConsole.Interfaces.MenuActionHandlers
{
    /// <summary>
    ///  Defines the operations for handling shift types.
    /// </summary>
    internal interface IShiftTypesActionHandler
    {
        /// <summary>
        /// Creates a new shift type in the database
        /// </summary>
        /// <remarks>
        /// Method should handle process of getting values for new shift type from user and performing a API call
        /// </remarks>
        Task ProcessCreate();

        /// <summary>
        /// Deletes existing shift type in the database
        /// </summary>
        /// <remarks>
        /// Method should handle process of getting shift type to be deleted details from user and performing a API call
        /// </remarks>
        Task ProcessDelete();

        /// <summary>
        /// Updates existing shift type in the database
        /// </summary>
        /// <remarks>
        /// Method should handle process of getting shift type to be updated details from user and performing a API call
        /// </remarks>
        Task ProcessEdit();

        /// <summary>
        /// Show all existing shift types in the database to the user
        /// </summary>
        /// <remarks>
        /// Method should handle process of getting all shift types, performing a API call and show result to the user
        /// </remarks>
        Task ProcessViewAll();
    }
}