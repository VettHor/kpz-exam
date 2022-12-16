using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.DTOs
{
    public class RecordDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("visitTime")]
        public DateTime VisitTime { get; set; }

        [JsonProperty("calendarId")]
        public string CalendarId { get; set; }

        [JsonProperty("frequency")]
        public int Frequency { get; set; }
    }
}
