using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using Newtonsoft.Json;


namespace back_end.Models
{
    public class Record
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime VisitTime { get; set; }

        [ForeignKey("Calendar")]
        public Guid CalendarId { get; set; }

        [Required]
        public int Frequency { get; set; }
    }
}
