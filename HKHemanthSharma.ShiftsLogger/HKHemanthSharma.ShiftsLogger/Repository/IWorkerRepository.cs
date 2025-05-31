using HKHemanthSharma.ShiftsLogger.Model;
using System.Net.Http.Json;
using System.Text.Json;

namespace HKHemanthSharma.ShiftsLogger.Repository
{
    public interface IWorkerRepository
    {
        public Task<ResponseDto<List<Worker>>> GetAllWorker();
        public Task<ResponseDto<List<Worker>>> GetSingleWorker(int Id);
        public Task<ResponseDto<Worker>> CreateWorker(string NewWorker);
        public Task<ResponseDto<Worker>> DeleteWorker(int Id);
        public Task<ResponseDto<Worker>> UpdateWorker(Worker worker);
    }
    public class WorkerRepository : IWorkerRepository
    {
        private readonly IMyHttpClient client;
        public WorkerRepository(IMyHttpClient _client)
        {
            client = _client;
        }
        public async Task<ResponseDto<Worker>> CreateWorker(string NewWorker)
        {
            try
            {
                HttpClient WorkerClient = client.GetClient();
                string BaseUrl = client.GetBaseUrl() + "Worker";
                var response = await WorkerClient.PostAsJsonAsync(BaseUrl, new Worker { Name = NewWorker });
                Stream stream = await response.Content.ReadAsStreamAsync();
                ResponseDto<Worker> ObjectResponse = await JsonSerializer.DeserializeAsync<ResponseDto<Worker>>(stream);
                return ObjectResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ResponseDto<Worker>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = e.Message
                };
            }
            return null;
        }

        public async Task<ResponseDto<Worker>> DeleteWorker(int Id)
        {
            try
            {
                HttpClient WorkerClient = client.GetClient();
                string BaseUrl = client.GetBaseUrl() + $"Worker/{Id}";
                var response = await WorkerClient.DeleteAsync(BaseUrl);
                Stream stream = await response.Content.ReadAsStreamAsync();
                ResponseDto<Worker> ObjectResponse = await JsonSerializer.DeserializeAsync<ResponseDto<Worker>>(stream);
                return ObjectResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ResponseDto<Worker>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = e.Message
                };
            }
            return null;
        }

        public async Task<ResponseDto<List<Worker>>> GetAllWorker()
        {
            try
            {
                HttpClient WorkerClient = client.GetClient();
                string BaseUrl = client.GetBaseUrl() + "Worker";
                using (Stream stream = await WorkerClient.GetStreamAsync(BaseUrl))
                {
                    ResponseDto<List<Worker>> ObjectResponse = await JsonSerializer.DeserializeAsync<ResponseDto<List<Worker>>>(stream);
                    return ObjectResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ResponseDto<List<Worker>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = e.Message
                };
            }
            return null;
        }

        public async Task<ResponseDto<List<Worker>>> GetSingleWorker(int Id)
        {
            try
            {
                HttpClient WorkerClient = client.GetClient();
                string BaseUrl = client.GetBaseUrl() + $"Worker/{Id}";
                using (Stream stream = await WorkerClient.GetStreamAsync(BaseUrl))
                {
                    ResponseDto<List<Worker>> WorkerResponse = await JsonSerializer.DeserializeAsync<ResponseDto<List<Worker>>>(stream);
                    return WorkerResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ResponseDto<List<Worker>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = e.Message
                };
            }
            return null;
        }

        public async Task<ResponseDto<Worker>> UpdateWorker(Worker worker)
        {
            try
            {
                HttpClient WorkerClient = client.GetClient();
                string BaseUrl = client.GetBaseUrl() + "Worker";
                var response = await WorkerClient.PutAsJsonAsync(BaseUrl, worker);
                Stream stream = await response.Content.ReadAsStreamAsync();
                ResponseDto<Worker> ObjectResponse = await JsonSerializer.DeserializeAsync<ResponseDto<Worker>>(stream);
                return ObjectResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ResponseDto<Worker>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = e.Message
                };
            }
            return null;
        }
    }
}
