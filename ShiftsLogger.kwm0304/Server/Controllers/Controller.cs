using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;
using Spectre.Console;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Controller<T>(IService<T> service) : ControllerBase where T : class
{
  private readonly IService<T> _service = service;

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    try
    {
      var entities = await _service.GetAllAsync();
      return Ok(entities);
    }
    catch (Exception e)
    {
      AnsiConsole.WriteException(e);
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(int id)
  {
    try
    {
      var entity = await _service.GetByIdAsync(id);
      if (entity == null)
      {
        return NotFound("Nothing found with this id");
      }
      return Ok(entity);
    }
    catch (Exception e)
    {
      AnsiConsole.WriteException(e);
      return BadRequest(e);
    }
  }

  [HttpPost]
  public async Task<IActionResult> Create(T entity)
  {
    try
    {
      await _service.AddAsync(entity);
      return CreatedAtAction(nameof(GetById), new { id = (entity as dynamic).Id }, entity);
    }
    catch (Exception e)
    {
      AnsiConsole.WriteException(e);
      return BadRequest(e);
    }
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(int id, T entity)
  {
    try
    {
      if (id != (entity as dynamic).Id)
      {
        return BadRequest();
      }
      await _service.UpdateAsync(entity);
      return Ok("Updated successfully");
    }
    catch (Exception e)
    {
      AnsiConsole.WriteException(e);
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    try
    {
      await _service.DeleteAsync(id);
      return Ok("Deleted successfully");
    }
    catch (Exception e)
    {
      AnsiConsole.WriteException(e);
      return BadRequest(e.Message);
    }
  }
}