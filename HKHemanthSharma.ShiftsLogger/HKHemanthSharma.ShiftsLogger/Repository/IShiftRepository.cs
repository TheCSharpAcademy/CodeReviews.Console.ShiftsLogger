using HKHemanthSharma.ShiftsLogger.Model;
using System.Net.Http.Json;
using System.Text.Json;

namespace HKHemanthSharma.ShiftsLogger.Repository
{
    public interface IShiftRepository
    {
        public Task<ResponseDto<List<Shift>>> GetAllShifts();
        public Task<ResponseDto<List<Shift>>> GetSingleShift(int Id);
        public Task<ResponseDto<Shift>> CreateShift(Shift Newshift);
        public Task<ResponseDto<Shift>> DeleteShift(int DeleteId);
        public Task<ResponseDto<Shift>> UpdateShift(Shift UpdatedShift);
    }
    public class ShiftRepository : IShiftRepository
    {
        private readonly IMyHttpClient client;
        public ShiftRepository(IMyHttpClient _client)
        {
            client = _client;
        }
        public async Task<ResponseDto<Shift>> CreateShift(Shift Newshift)
        {
            try
            {
                HttpClient ShiftClient = client.GetClient();
                string ShiftUrl = client.GetBaseUrl() + "Shifts";
                var objectresponse = await ShiftClient.PostAsJsonAsync(ShiftUrl, Newshift);
                var ResponseStream = await objectresponse.Content.ReadAsStreamAsync();
                ResponseDto<Shift> CreatedResponse = await JsonSerializer.DeserializeAsync<ResponseDto<Shift>>(ResponseStream);
                return CreatedResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ResponseDto<Shift>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = e.Message
                };
            }
            return null;
        }

        public async Task<ResponseDto<Shift>> DeleteShift(int DeleteId)
        {
            try
            {
                HttpClient ShiftClient = client.GetClient();
                string ShiftUrl = client.GetBaseUrl() + $"Shifts/{DeleteId}";
                var objectresponse = await ShiftClient.DeleteAsync(ShiftUrl);
                var ResponseStream = await objectresponse.Content.ReadAsStreamAsync();
                ResponseDto<Shift> CreatedResponse = await JsonSerializer.DeserializeAsync<ResponseDto<Shift>>(ResponseStream);
                return CreatedResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ResponseDto<Shift>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = e.Message
                };
            }
            return null;
        }

        public async Task<ResponseDto<List<Shift>>> GetAllShifts()
        {
            try
            {
                HttpClient ShiftClient = client.GetClient();
                string ShiftUrl = client.GetBaseUrl() + "Shifts";
                using (Stream stream = await ShiftClient.GetStreamAsync(ShiftUrl))
                {
                    var objectresponse = await JsonSerializer.DeserializeAsync<ResponseDto<List<Shift>>>(stream);
                    return objectresponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ResponseDto<List<Shift>>
                {
                    Data = [],
                    IsSuccess = false,
                    Message = e.Message
                };
            }
            return null;
        }

        public async Task<ResponseDto<List<Shift>>> GetSingleShift(int Id)
        {
            try
            {
                HttpClient ShiftClient = client.GetClient();
                string ShiftUrl = client.GetBaseUrl() + $"Shifts/{Id}";
                using (Stream stream = await ShiftClient.GetStreamAsync(ShiftUrl))
                {
                    var objectresponse = JsonSerializer.Deserialize<ResponseDto<List<Shift>>>(stream);
                    return objectresponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ResponseDto<List<Shift>>
                {
                    Data = [],
                    IsSuccess = false,
                    Message = e.Message
                };
            }
            return null;
        }

        public async Task<ResponseDto<Shift>> UpdateShift(Shift UpdatedShift)
        {
            try
            {
                HttpClient ShiftClient = client.GetClient();
                string ShiftUrl = client.GetBaseUrl() + $"Shifts/{UpdatedShift.ShiftId}";
                var ObjectResponse = await ShiftClient.PutAsJsonAsync(ShiftUrl, new ShiftDto
                {
                    WorkerId = UpdatedShift.WorkerId,
                    ShiftStartTime = UpdatedShift.ShiftStartTime,
                    ShiftEndTime = UpdatedShift.ShiftEndTime,
                    ShiftDate = UpdatedShift.ShiftDate
                });
                var ResponseStream = await ObjectResponse.Content.ReadAsStreamAsync();
                ResponseDto<Shift> UpdatedResponse = await JsonSerializer.DeserializeAsync<ResponseDto<Shift>>(ResponseStream);
                return UpdatedResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ResponseDto<Shift>
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
