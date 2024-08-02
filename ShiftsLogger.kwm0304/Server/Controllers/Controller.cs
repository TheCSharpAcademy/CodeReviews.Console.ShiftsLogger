using System.Reflection;
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
      PropertyInfo idProperty = typeof(T).GetProperties()
      .FirstOrDefault(p => p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase))!;

      if (idProperty is null)
      {
        return BadRequest("Id is null");
      }
      int entityId = (int)idProperty.GetValue(entity)!;
      if (id != entityId)
      {
        return BadRequest("Given doesn't match entities ID");
      }
      idProperty.SetValue(entity, id);
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