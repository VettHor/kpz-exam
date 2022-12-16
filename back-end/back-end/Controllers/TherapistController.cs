using back_end.DTOs;
using back_end.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TherapistController : ControllerBase
    {
        private readonly ITherapistService _service;

        public TherapistController(ITherapistService service)
        {
            _service = service;
        }

        [Route("all")]
        [HttpGet]
        public IActionResult GetAllTherapists()
        {
            var therapists = _service.GetAllTherapists();
            return Ok(therapists);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetTherapistById(Guid id)
        {
            var therapist = await _service.GetTherapistById(id);

            if (therapist is null)
            {
                return NotFound($"Therapist with Id {id} does not exist.");
            }

            return Ok(therapist);
        }

        [HttpPost]
        public IActionResult AddTherapist([FromBody] TherapistDto therapist)
        {
            try
            {
                _service.AddTherapist(therapist);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateTherapist([FromBody] TherapistDto therapist)
        {
            try
            {
                _service.UpdateTherapist(therapist);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTherapist(Guid id)
        {
            var result = await _service.DeleteTherapist(id);
            return result ? Ok() : NotFound($"Therapist with Id {id} does not exist.");
        }
    }
}
