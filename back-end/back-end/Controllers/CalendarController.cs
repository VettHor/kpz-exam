using back_end.DTOs;
using back_end.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _service;

        public CalendarController(ICalendarService service)
        {
            _service = service;
        }

        [Route("all")]
        [HttpGet]
        public IActionResult GetAllCalendars()
        {
            var calendars = _service.GetAllCalendars();
            return Ok(calendars);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCalendarById(Guid id)
        {
            var calendar = await _service.GetCalendarById(id);

            if (calendar is null)
            {
                return NotFound($"Calendar with Id {id} does not exist.");
            }

            return Ok(calendar);
        }

        [HttpPost]
        public IActionResult AddCalendar([FromBody] CalendarDto calendar)
        {
            try
            {
                _service.AddCalendar(calendar);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateCalendar([FromBody] CalendarDto calendar)
        {
            try
            {
                _service.UpdateCalendar(calendar);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCalendar(Guid id)
        {
            var result = await _service.DeleteCalendar(id);
            return result ? Ok() : NotFound($"Calendar with Id {id} does not exist.");
        }
    }
}
