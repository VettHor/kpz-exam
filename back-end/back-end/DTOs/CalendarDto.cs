using back_end.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.DTOs
{
    public class CalendarDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("therapistId")]
        public string TherapistId { get; set; }
    }
}