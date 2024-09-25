using Newtonsoft.Json;
using RestSharp;
using ShiftLogger_Frontend.Arashi256.Config;
using ShiftLogger_Shared.Arashi256.Models;
using ShiftLogger_Shared.Arashi256.Classes;
using ResponseStatus = ShiftLogger_Shared.Arashi256.Classes.ResponseStatus;

namespace ShiftLogger_Frontend.Arashi256.Services
{
    internal class ShiftService
    {
        public string DateTimeApplicationFormat = string.Empty;
        private string _apiURL = string.Empty;

        public ShiftService()
        {
            AppManager appManager = new AppManager();
            string? baseURL = appManager.ApiBaseURL;
            if (string.IsNullOrEmpty(baseURL))
            {
                throw new ArgumentException("ERROR: API URL could not be loaded fron App.config");
            }
            _apiURL = baseURL;
            string? dateTime = appManager.PreferredDateTimeFormat;
            if (string.IsNullOrEmpty(dateTime))
            {
                DateTimeApplicationFormat = "dd-MM-yy HH:mm";
            }
            else
            {
                DateTimeApplicationFormat = dateTime;
            }
        }

        public async Task<ServiceResponse> GetWorkersAsync()
        {
            try
            {
                var client = new RestClient(_apiURL);
                var request = new RestRequest("Worker");
                var response = await client.ExecuteAsync(request);
                string? rawResponse;
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        rawResponse = response.Content;
                        if (rawResponse != null)
                        {
                            // Deserialize the JSON response into a List of WorkerOutputDto
                            var deserialized = JsonConvert.DeserializeObject<List<WorkerOutputDto>>(rawResponse);
                            return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", deserialized);
                        }
                        else
                        {
                            return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Returned empty OK from API", null);
                        }
                    case System.Net.HttpStatusCode.NotFound:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No workers found", null);
                    case System.Net.HttpStatusCode.InternalServerError:
                        rawResponse = response.Content;
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, rawResponse ?? "Internal server error 500", null);
                    case 0:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No response from API endpoint - connection timeout", null);
                    default:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Unhandled status code: {response.StatusCode}", null);
                }
            }
            catch (Exception ex)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Exception thrown: {ex.Message}", null);
            }
        }

        public async Task<ServiceResponse> AddWorkerAsync(WorkerInputDto workerDto)
        {
            try
            {
                var client = new RestClient(_apiURL);
                var request = new RestRequest("Worker", Method.Post);
                // Add the workerDto to the request body
                request.AddJsonBody(workerDto);
                // Execute the API call
                var response = await client.ExecuteAsync(request);
                string? rawResponse;
                // Check the status code of the response
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Created:
                        rawResponse = response.Content;
                        if (rawResponse != null)
                        {
                            // Deserialize the response to a WorkerOutputDto object
                            var createdWorker = JsonConvert.DeserializeObject<WorkerOutputDto>(rawResponse);
                            return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", createdWorker);
                        }
                        else
                        {
                            return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No response from API", null);
                        }
                    case System.Net.HttpStatusCode.BadRequest:
                        rawResponse = response.Content;
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, rawResponse ?? "Invalid data provided - duplicate email address with another worker", null);
                    case System.Net.HttpStatusCode.InternalServerError:
                        rawResponse = response.Content;
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, rawResponse ?? "Internal server error 500", null);
                    case 0:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No response from API endpoint - connection timeout", null);
                    default:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Unhandled status code: {response.StatusCode}", null);
                }
            }
            catch (Exception ex)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Exception thrown: {ex.Message}", null);
            }
        }

        public async Task<ServiceResponse> DeleteWorkerAsync(int workerId)
        {
            try
            {
                var client = new RestClient(_apiURL);
                var request = new RestRequest($"Worker/{workerId}", Method.Delete);
                var response = await client.ExecuteAsync(request);
                string? rawResponse;
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NoContent:
                        // Success, the worker has been deleted.
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", null);
                    case System.Net.HttpStatusCode.BadRequest:
                        rawResponse = response.Content;
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, rawResponse ?? "Bad request - worker id for deletion not found", null);
                    case System.Net.HttpStatusCode.InternalServerError:
                        rawResponse = response.Content;
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, rawResponse ?? "Internal server error 500", null);
                    case 0:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No response from API endpoint - connection timeout", null);
                    default:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Unhandled status code: {response.StatusCode}", null);
                }
            }
            catch (Exception ex)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Exception thrown: {ex.Message}", null);
            }
        }

        public async Task<ServiceResponse> UpdateWorkerAsync(int id, WorkerInputDto workerDto)
        {
            try
            {
                var client = new RestClient(_apiURL);
                // Create the PUT request for updating the worker
                var request = new RestRequest($"Worker/{id}", Method.Put);
                request.AddJsonBody(workerDto);
                var response = await client.ExecuteAsync(request);
                string? rawResponse;
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NoContent:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", null);
                    case System.Net.HttpStatusCode.BadRequest:
                        rawResponse = response.Content;
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, rawResponse ?? "Bad request", null);
                    case System.Net.HttpStatusCode.InternalServerError:
                        rawResponse = response.Content;
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, rawResponse ?? "Internal server error 500", null);
                    case 0:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No response from API endpoint - connection timeout", null);
                    default:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Unhandled status code: {response.StatusCode}", null);
                }
            }
            catch (Exception ex)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Exception thrown: {ex.Message}", null);
            }
        }

        public async Task<ServiceResponse> AddWorkerShiftAsync(WorkerShiftInputDto shiftDto)
        {
            try
            {
                var client = new RestClient(_apiURL);
                var request = new RestRequest("WorkerShift", Method.Post);
                // Add the shiftDto to the request body
                request.AddJsonBody(shiftDto);
                // Execute the API call
                var response = await client.ExecuteAsync(request);
                string? rawResponse;
                // Check the status code of the response
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Created:
                        rawResponse = response.Content;
                        if (rawResponse != null)
                        {
                            // Deserialize the response to a WorkerShiftOutputDto object
                            var createdWorkerShift = JsonConvert.DeserializeObject<WorkerShiftOutputDto>(rawResponse);
                            return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", createdWorkerShift);
                        }
                        else
                        {
                            return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No response from API", null);
                        }
                    case System.Net.HttpStatusCode.BadRequest:
                        rawResponse = response.Content;
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, rawResponse ?? "WorkerShift formatting incorrect, Worker ID not found or WorkerShift already exists", null);
                    case System.Net.HttpStatusCode.InternalServerError:
                        rawResponse = response.Content;
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, rawResponse ?? "Internal server error 500", null);
                    case 0:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No response from API endpoint - connection timeout", null);
                    default:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Unhandled status code: {response.StatusCode}", null);
                }
            }
            catch (Exception ex)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Exception thrown: {ex.Message}", null);
            }
        }

        public async Task<ServiceResponse> GetWorkersShiftAsync()
        {
            try
            {
                var client = new RestClient(_apiURL);
                var request = new RestRequest("WorkerShift");
                var response = await client.ExecuteAsync(request);
                string? rawResponse;
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        rawResponse = response.Content;
                        if (rawResponse != null)
                        {
                            // Deserialize the JSON response into a List of WorkerShiftOutputDto
                            var deserialized = JsonConvert.DeserializeObject<List<WorkerShiftOutputDto>>(rawResponse);
                            return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", deserialized);
                        }
                        else
                        {
                            return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Returned empty OK from API", null);
                        }
                    case System.Net.HttpStatusCode.NotFound:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No worker shifts found", null);
                    case System.Net.HttpStatusCode.InternalServerError:
                        rawResponse = response.Content;
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, rawResponse ?? "Internal server error 500", null);
                    case 0:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No response from API endpoint - connection timeout", null);
                    default:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Unhandled status code: {response.StatusCode}", null);
                }
            }
            catch (Exception ex)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Exception thrown: {ex.Message}", null);
            }
        }

        public async Task<ServiceResponse> DeleteWorkerShiftAsync(int workershiftId)
        {
            try
            {
                var client = new RestClient(_apiURL);
                var request = new RestRequest($"WorkerShift/{workershiftId}", Method.Delete);
                var response = await client.ExecuteAsync(request);
                string? rawResponse;
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NoContent:
                        // Success, the workershift has been deleted.
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", null);
                    case System.Net.HttpStatusCode.BadRequest:
                        rawResponse = response.Content;
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, rawResponse ?? "Bad request - workershift id for deletion not found", null);
                    case System.Net.HttpStatusCode.InternalServerError:
                        rawResponse = response.Content;
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, rawResponse ?? "Internal server error 500", null);
                    case 0:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No response from API endpoint - connection timeout", null);
                    default:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Unhandled status code: {response.StatusCode}", null);
                }
            }
            catch (Exception ex)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Exception thrown: {ex.Message}", null);
            }
        }

        public async Task<ServiceResponse> GetWorkerShiftsForWorkerAsync(int workerId)
        {
            try
            {
                var client = new RestClient(_apiURL);
                var request = new RestRequest($"WorkerShift/worker/{workerId}");
                var response = await client.ExecuteAsync(request);
                string? rawResponse;
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        rawResponse = response.Content;
                        if (rawResponse != null)
                        {
                            // Deserialize the JSON response into a List of WorkerShiftOutputDto
                            var deserialized = JsonConvert.DeserializeObject<List<WorkerShiftOutputDto>>(rawResponse);
                            return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", deserialized);
                        }
                        else
                        {
                            return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "Returned empty OK from API", null);
                        }
                    case System.Net.HttpStatusCode.NotFound:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No shifts found for this worker", null);
                    case System.Net.HttpStatusCode.InternalServerError:
                        rawResponse = response.Content;
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, rawResponse ?? "Internal server error 500", null);
                    case 0:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No response from API endpoint - connection timeout", null);
                    default:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Unhandled status code: {response.StatusCode}", null);
                }
            }
            catch (Exception ex)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Exception thrown: {ex.Message}", null);
            }
        }

        public async Task<ServiceResponse> UpdateWorkerShiftAsync(int id, WorkerShiftInputDto workerDto)
        {
            try
            {
                var client = new RestClient(_apiURL);
                // Create the PUT request for updating the worker shift
                var request = new RestRequest($"WorkerShift/{id}", Method.Put);
                request.AddJsonBody(workerDto);
                var response = await client.ExecuteAsync(request);
                string? rawResponse;
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NoContent:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Success, "OK", null);
                    case System.Net.HttpStatusCode.BadRequest:
                        rawResponse = response.Content;
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, rawResponse ?? "Bad request", null);
                    case System.Net.HttpStatusCode.InternalServerError:
                        rawResponse = response.Content;
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, rawResponse ?? "Internal server error 500", null);
                    case 0:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, "No response from API endpoint - connection timeout", null);
                    default:
                        return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Unhandled status code: {response.StatusCode}", null);
                }
            }
            catch (Exception ex)
            {
                return ServiceResponseUtils.CreateResponse(ResponseStatus.Failure, $"Exception thrown: {ex.Message}", null);
            }
        }

        public string? CalculateAverageShiftsDuration(List<WorkerShiftOutputDto> shifts)
        {
            if (shifts == null || shifts.Count == 0) return null;
            // Sum all the durations
            TimeSpan totalDuration = new TimeSpan();
            foreach (var shift in shifts)
            {
                totalDuration += shift.Duration;
            }
            // Calculate the average
            TimeSpan averageDuration = new TimeSpan(totalDuration.Ticks / shifts.Count);
            return averageDuration.ToString(@"hh\:mm");
        }

        public string? CalculateTotalShiftsDuration(List<WorkerShiftOutputDto> shifts)
        {
            if (shifts == null || shifts.Count == 0) return null;
            TimeSpan total = TimeSpan.Zero;
            foreach (var shift in shifts)
            {
                total += shift.Duration;
            }
            return total.ToString(@"hh\:mm");
        }
    }
}
