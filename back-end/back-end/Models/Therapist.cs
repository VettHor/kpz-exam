using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using Newtonsoft.Json;

namespace back_end.Models
{
    public class Therapist
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string ReceptionRoom { get; set; }

        [Required]
        public bool CanEdit { get; set; }
    }
}
