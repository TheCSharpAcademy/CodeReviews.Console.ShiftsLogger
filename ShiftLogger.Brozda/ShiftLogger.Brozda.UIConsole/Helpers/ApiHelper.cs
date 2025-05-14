using RestSharp;
using ShiftLogger.Brozda.UIConsole.InputOutput;
using ShiftLogger.Brozda.UIConsole.Models;


namespace ShiftLogger.Brozda.UIConsole.Helpers
{
    /// <summary>
    /// Static class handling operations towards an API
    /// </summary>
    internal static class ApiHelper
    {
        /// <summary>
        /// Process the result
        /// Check is it was successul; prints out error, success message or received data respectively
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"><see cref="ApiResult{T}"/> containg result of an API call</param>
        /// <param name="errorMsg">Optional argument specifying error message to be printed in case of failed call</param>
        /// <param name="successMsg">Optional argument specifying error message to be printed in case of sucessfull call</param>
        /// <param name="showData">Optional argument directing if received data should be printed to the output</param>
        /// <returns>true if call was successfull, false otherwise</returns>
        public static bool HandleResult<T>(ApiResult<T> result, string? errorMsg = null, string? successMsg = null, bool showData = false)
        {
            if (result.StatusCode == 404)
            {
                AppOutput.PrintErrorApiResult(AppConstants.ERROR_NOTFOUND, result.ErrorMessage ?? AppConstants.ERROR_UNHANDLED);
                return false;
            }
            else if (!result.IsSuccessful || result.Data is null)
            {
                AppOutput.PrintErrorApiResult(errorMsg ?? AppConstants.ERROR_UNHANDLED, result.ErrorMessage ?? AppConstants.ERROR_UNHANDLED);
                return false;
            }

            if (showData)
            {
                AppOutput.PrintData<T>(result.Data);
            }

            if (successMsg is not null)
            {
                AppOutput.PrintText(successMsg);
            }

            return true;
        }

        /// <summary>
        /// Executes HTTP request towards the API
        /// </summary>
        /// <typeparam name="T">Data type expected in data portion of an <see cref="ApiResult{T}"/></typeparam>
        /// <param name="client">Rest client handling the HTTP request</param>
        /// <param name="restRequest">Rest request containing required data and method</param>
        /// <returns></returns>
        public static async Task<ApiResult<T>> ExecuteSafe<T>(RestClient client, RestRequest restRequest)
        {
            try
            {
                var response = await client.ExecuteAsync<T>(restRequest);

                if (response.IsSuccessful && response.Data is not null)
                {
                    return ApiResult<T>.Success(response.Data);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return ApiResult<T>.NotFound();
                }

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    string error;

                    if (response.ErrorException is not null && response.ErrorException.Message is not null)
                        error = response.ErrorException.Message;
                    else
                        error = "n/a";

                    return ApiResult<T>.Fail($"Bad request: {error} ", (int)System.Net.HttpStatusCode.BadRequest);
                }

                if (response.Data is null)
                {
                    string error;

                    if (response.ErrorException is not null && response.ErrorException.Message is not null)
                        error = response.ErrorException.Message;
                    else
                        error = "Unknown deserialization failure";

                    return ApiResult<T>.Fail($"Response format exception: {error}");
                }

                return ApiResult<T>.Fail($"Server error: " +
                    $"{response.StatusCode}; {response.StatusDescription}",
                    (int)response.StatusCode
                    );
            }
            catch (Exception ex)
            {
                return ApiResult<T>.Fail($"Unhandled Server error: {ex.Message}");
            }
        }
    }
}