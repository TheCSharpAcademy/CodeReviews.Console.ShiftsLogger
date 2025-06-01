using HKhemanthSharma.ShiftLoggerAPI.Model;
using HKhemanthSharma.ShiftLoggerAPI.Model.Dto;
using HKhemanthSharma.ShiftLoggerAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HKhemanthSharma.ShiftLoggerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly IShiftRepository repository;
        public ShiftsController(IShiftRepository repo)
        {
            repository = repo;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<Shift>>>> GetAllShiftsAsync()
        {
            List<Shift> Data = null;
            try
            {
                Data = await repository.GetAllShiftsAsync();
                if (Data == null)
                {
                    return NotFound(ResponseDto<List<Shift>>.Success(Data, "No Data Found"));
                }
                return ResponseDto<List<Shift>>.Success(Data, "Successfully Fetched Data!");
            }
            catch (Exception e)
            {
                return ResponseDto<List<Shift>>.Failure(Data, e.Message);
            }
        }
        [HttpGet]
        [Route("{Id:int}")]
        public async Task<ActionResult<ResponseDto<List<Shift>>>> GetShiftByIdAsync([FromRoute] int Id)
        {
            List<Shift> DataList = new List<Shift>();
            try
            {
                var Data = await repository.GetShiftByIdAsync(Id);
                if (Data == null)
                {
                    return ResponseDto<List<Shift>>.Failure(DataList, "No Shifts with Given ID");
                }
                DataList.Add(Data);
                return ResponseDto<List<Shift>>.Success(DataList, "Successfully Fetched The Data!!!");
            }
            catch (Exception e)
            {
                return ResponseDto<List<Shift>>.Failure(DataList, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<ShiftDto>>> CreateShiftAsync([FromBody] ShiftDto shift)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ResponseDto<ShiftDto>.Failure(shift, "Bad Data!! Please give the Properties for Shift with valid format");
                }
                var NewShift = await repository.CreateShift(shift);
                return Ok(ResponseDto<ShiftDto>.Success(shift, "Successfully Created Shift"));
            }
            catch (Exception e)
            {
                return ResponseDto<ShiftDto>.Failure(shift, e.Message);
            }
        }
        [HttpPut]
        [Route("{Id:int}")]
        public async Task<ActionResult<ResponseDto<Shift>>> UpdateShiftAsync([FromBody] ShiftDto shift, [FromRoute] int Id)
        {
            Shift UpdatedShift = null;
            try
            {
                if (!ModelState.IsValid)
                {
                    return ResponseDto<Shift>.Failure(UpdatedShift, "Bad Data!! Please give the Properties for Shift with valid format");
                }
                UpdatedShift = await repository.UpdateShiftAsync(shift, Id);
                return Ok(ResponseDto<Shift>.Success(UpdatedShift, "Succefully Updated"));
            }
            catch (Exception e)
            {
                return ResponseDto<Shift>.Failure(UpdatedShift, e.Message);
            }
        }
        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<ActionResult<ResponseDto<Shift>>> DeleteShiftAsync([FromRoute] int Id)
        {
            Shift DeleteShift = null;
            try
            {
                DeleteShift = await repository.DeleteShiftAsync(Id);
                if (DeleteShift == null)
                {
                    return (ResponseDto<Shift>.Failure(DeleteShift, "No Shift is available!!!"));
                }
                return Ok(ResponseDto<Shift>.Success(DeleteShift, "Shift is successfully Deleted"));
            }
            catch (Exception e)
            {
                return ResponseDto<Shift>.Failure(DeleteShift, e.Message);
            }
        }
    }
}

