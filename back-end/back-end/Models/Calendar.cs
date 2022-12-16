using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using Newtonsoft.Json;

namespace back_end.Models
{
    public class Calendar
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Therapist")]
        public Guid TherapistId { get; set; }

        [JsonIgnore]
        public Therapist Therapist { get; set; }

        [JsonIgnore]
        public List<Record> Records { get; set; }
    }
}
