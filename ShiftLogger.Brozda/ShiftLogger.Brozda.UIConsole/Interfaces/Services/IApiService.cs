using RestSharp;
using ShiftLogger.Brozda.UIConsole.Models;

namespace ShiftLogger.Brozda.UIConsole.Interfaces.Services
{
    /// <summary>
    /// Defines a generic interface for basic CRUD operations on a REST API endpoint.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IApiService<T>
    {
        /// <summary>
        /// Gets the underlying RestClient instance used to communicate with the API.
        /// </summary>
        RestClient Client { get; }

        /// <summary>
        /// Gets the base URL for the specific API endpoint this service interacts with.
        /// </summary>
        string BaseUrl { get; }

        /// <summary>
        /// Sends a request to create a new resource.
        /// </summary>
        /// <param name="dto">The data transfer object to create.</param>
        /// <returns>An <see cref="ApiResult{T}"/> representing the result of the create operation.</returns>
        Task<ApiResult<T>> Create(T dto);

        /// <summary>
        /// Sends a request to delete a resource by its ID.
        /// </summary>
        /// <param name="id">The ID of the resource to delete.</param>
        /// <returns>An <see cref="ApiResult{T}"/> indicating whether the operation was successful.</returns>
        Task<ApiResult<bool>> Delete(int id);

        /// <summary>
        /// Sends a request to update an existing resource by its ID.
        /// </summary>
        /// <param name="id">The ID of the resource to update.</param>
        /// <param name="dto">The updated data transfer object.</param>
        /// <returns>An <see cref="ApiResult{T}"/> representing the updated resource.</returns>
        Task<ApiResult<T>> Edit(int id, T dto);

        /// <summary>
        /// Retrieves a list of all resources.
        /// </summary>
        /// <returns>An <see cref="ApiResult{List{T}}"/> containing all resources or error information.</returns>
        Task<ApiResult<List<T>>> GetAll();

        /// <summary>
        /// Retrieves a single resource by its ID.
        /// </summary>
        /// <param name="id">The ID of the resource to retrieve.</param>
        /// <returns>An <see cref="ApiResult{T}"/> representing the retrieved resource or error.</returns>
        Task<ApiResult<T>> GetById(int id);
    }
}