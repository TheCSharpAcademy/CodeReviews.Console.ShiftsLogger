using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shiftlogger.Entities;
using Shiftlogger.DTOs;
using Shiftlogger.Repositories.Interfaces;
using AutoMapper;

namespace Shiftlogger.javedkhan2k2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IMapper _mapper;
        public ShiftController(IShiftRepository shiftRepository, IMapper mapper)
        {
            _shiftRepository = shiftRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ShiftRequestDto>> GetAllShifts()
        {
            var shifts = _shiftRepository.GetAllShifts();
            var shiftsDto = _mapper.Map<IEnumerable<ShiftRequestDto>>(shifts);
            return Ok(shiftsDto);
        }

        [HttpGet("{id}")]
        public ActionResult<ShiftRequestDto> GetShift(int id)
        {
            var shift = _shiftRepository.GetShiftById(id);
            if(shift == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ShiftRequestDto>(shift));

        }

        [HttpPost]
        public ActionResult<ShiftRequestDto> PostShift(ShiftAddDto shift)
        {
            _shiftRepository.AddShift(shift.ToShift());
            return CreatedAtAction(nameof(GetShift), new { id = shift.Id }, shift);
        }

        [HttpPut("{id}")]
        public ActionResult PutShift(int id, ShiftAddDto shift)
        {
            if(id != shift.Id)
            {
                return BadRequest("Shift ID mismatch");
            }
            var existingShift = _shiftRepository.GetShiftById(shift.Id);
            if(existingShift == null)
            {
                return NotFound();
            }
            _shiftRepository.UpdateShift(shift.ToShiftPut());
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteShift(int id)
        {
            var shift = _shiftRepository.GetShiftById(id);
            if(shift == null)
            {
                return NotFound();
            }
            _shiftRepository.DeleteShift(shift);
            return NoContent();
        }

    }

}