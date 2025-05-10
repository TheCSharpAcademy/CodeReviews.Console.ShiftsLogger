using RestSharp;
using ShiftLogger.Brozda.UIConsole.Helpers;
using ShiftLogger.Brozda.UIConsole.Interfaces.Services;
using ShiftLogger.Brozda.UIConsole.Models;

namespace ShiftLogger.Brozda.UIConsole.Services
{
    /// <summary>
    /// Generic ApiService managing basic CRUD operations
    /// </summary>
    /// <typeparam name="T">Data type of data expected in the <see cref="ApiResult{T}"/></typeparam>
    internal class ApiService<T> : IApiService<T>
    {
        public RestClient Client { get; }
        public string BaseUrl { get; }

        /// <summary>
        /// Initializes new instance of ApiService
        /// </summary>
        /// <param name="client">Rest client used for an API call</param>
        /// <param name="baseUrl">Base url for API requests</param>
        public ApiService(RestClient client, string baseUrl)
        {
            Client = client;
            BaseUrl = baseUrl;
        }

        /// <inheritdoc/>
        public async Task<ApiResult<List<T>>> GetAll()
        {
            var request = new RestRequest(BaseUrl, Method.Get);

            return await ApiHelper.ExecuteSafe<List<T>>(Client, request);
        }

        /// <inheritdoc/>
        public async Task<ApiResult<T>> GetById(int id)
        {
            var request = new RestRequest($"{BaseUrl}/{id}", Method.Get);

            return await ApiHelper.ExecuteSafe<T>(Client, request);
        }

        /// <inheritdoc/>
        public async Task<ApiResult<T>> Create(T dto)
        {
            var request = new RestRequest(BaseUrl, Method.Post);
            if (dto == null)
            {
                return ApiResult<T>.InvalidData();
            }
            request.AddBody(dto, ContentType.Json);

            return await ApiHelper.ExecuteSafe<T>(Client, request);
        }

        /// <inheritdoc/>
        public async Task<ApiResult<T>> Edit(int id, T dto)
        {
            var request = new RestRequest($"{BaseUrl}/{id}", Method.Put);
            if (dto == null)
            {
                return ApiResult<T>.InvalidData();
            }

            request.AddBody(dto, ContentType.Json);

            return await ApiHelper.ExecuteSafe<T>(Client, request);
        }

        /// <inheritdoc/>
        public async Task<ApiResult<bool>> Delete(int id)
        {
            var request = new RestRequest($"{BaseUrl}/{id}", Method.Delete);

            return await ApiHelper.ExecuteSafe<bool>(Client, request);
        }
    }
}