using back_end.DTOs;
using back_end.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordService _service;

        public RecordController(IRecordService service)
        {
            _service = service;
        }

        [Route("all")]
        [HttpGet]
        public IActionResult GetAllRecords()
        {
            var records = _service.GetAllRecords();
            return Ok(records);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetRecordById(Guid id)
        {
            var record = await _service.GetRecordById(id);

            if (record is null)
            {
                return NotFound($"Record with Id {id} does not exist.");
            }

            return Ok(record);
        }

        [Route("calendar/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetRecordsByCalendarId(Guid id)
        {
            var records = _service.GetRecordsByCalendarId(id);
            if (records is null)
            {
                return NotFound($"Records with calendar id {id} does not exist.");
            }
            return Ok(records);
        }

        [Route("byword/{word}")]
        [HttpGet]
        public async Task<IActionResult> GetAllRecordsByWord(string word)
        {
            var records = _service.GetAllRecordsByWord(word);
            if (records is null)
            {
                return NotFound($"Records with this word does not exist.");
            }
            return Ok(records);
        }

        [HttpPost]
        public IActionResult AddRecord([FromBody] RecordDto record)
        {
            try
            {
                _service.AddRecord(record);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateRecord([FromBody] RecordDto record)
        {
            try
            {
                _service.UpdateRecord(record);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRecord(Guid id)
        {
            var result = await _service.DeleteRecord(id);
            return result ? Ok() : NotFound($"Record with Id {id} does not exist.");
        }
    }
}
