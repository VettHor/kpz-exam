using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.DTOs
{
    public class TherapistDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("receptionRoom")]
        public string ReceptionRoom { get; set; }

        [JsonProperty("canEdit")]
        public bool CanEdit { get; set; }
    }
}
