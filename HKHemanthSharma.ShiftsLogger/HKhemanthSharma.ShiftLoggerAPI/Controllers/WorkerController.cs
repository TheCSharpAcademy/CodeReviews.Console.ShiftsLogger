using HKhemanthSharma.ShiftLoggerAPI.Model;
using HKhemanthSharma.ShiftLoggerAPI.Model.Dto;
using HKhemanthSharma.ShiftLoggerAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HKhemanthSharma.ShiftLoggerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerRepository Repository;
        public WorkerController(IWorkerRepository repo)
        {
            Repository = repo;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<Worker>>>> GetAllWorkersAsync()
        {
            List<Worker> Workers = null;
            try
            {
                Workers = await Repository.GetAllWorkerAsync();
                if (Workers == null)
                {
                    return NotFound(ResponseDto<List<Worker>>.Failure(Workers, "No Workers Found"));
                }
                return Ok(ResponseDto<List<Worker>>.Success(Workers, "Successfully Fetched Workers"));
            }
            catch (Exception e)
            {
                return ResponseDto<List<Worker>>.Failure(Workers, e.Message);
            }
        }
        [HttpGet]
        [Route("{Id:int}")]
        public async Task<ActionResult<List<Worker>>> GetWorkerAsync([FromRoute] int Id)
        {
            List<Worker> Workers = new List<Worker>();
            try
            {
                var worker = await Repository.GetWorkerAsync(Id);
                if (worker == null)
                {
                    return NotFound(ResponseDto<List<Worker>>.Failure(Workers, "No Workers Found"));
                }
                Workers.Add(worker);
                return Ok(ResponseDto<List<Worker>>.Success(Workers, "Successfully Fetched Workers"));
            }
            catch (Exception e)
            {
                return Ok(ResponseDto<List<Worker>>.Failure(Workers, e.Message));
            }
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto<Worker>>> CreateWorkerAsync([FromBody] WorkerDto NewWorker)
        {
            Worker Worker = null;
            try
            {
                Worker = await Repository.CreateWorkerAsync(NewWorker);
                return Ok(ResponseDto<Worker>.Success(Worker, "Successfully Created Worker"));
            }
            catch (Exception e)
            {
                return StatusCode(501, e.Message);
            }
        }
        [HttpPut]
        [Route("{Id:int}")]
        public async Task<ActionResult<ResponseDto<Worker>>> GetWorkerAsync([FromRoute] int Id, [FromBody] WorkerDto UpdateWorker)
        {
            Worker Worker = null;
            try
            {
                Worker = await Repository.UpdateWorkerAsync(UpdateWorker, Id);
                if (Worker == null)
                {
                    return NotFound(ResponseDto<Worker>.Failure(Worker, "No Worker Found"));
                }
                return Ok(ResponseDto<Worker>.Success(Worker, "Successfully Updated Worker"));
            }
            catch (Exception e)
            {
                return ResponseDto<Worker>.Failure(Worker, e.Message);
            }
        }
        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<ActionResult<ResponseDto<Worker>>> DeleteWorkerAsync([FromRoute] int Id)
        {
            Worker Worker = null;
            try
            {
                Worker = await Repository.DeleteWorkerAsync(Id);
                if (Worker == null)
                {
                    return NotFound(ResponseDto<Worker>.Failure(Worker, "No Worker Found"));
                }
                return Ok(ResponseDto<Worker>.Success(Worker, "Successfully Deleted Worker"));
            }
            catch (Exception e)
            {
                return ResponseDto<Worker>.Failure(Worker, e.Message);
            }
        }
    }
}
