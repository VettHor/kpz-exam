using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.DB
{
    public class TherapistDbContext : DbContext
    {
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Therapist> Therapists { get; set; }

        public TherapistDbContext(DbContextOptions<TherapistDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            List<Therapist> therapists = new List<Therapist>()
            {
                new Therapist() {Id = Guid.NewGuid(), Name = "Vitalii", Surname = "Horbovyi", ReceptionRoom = "Traumatology", CanEdit = false},
                new Therapist() {Id = Guid.NewGuid(), Name = "Taras", Surname = "Topolya", ReceptionRoom = "Dentistry", CanEdit = false},
                new Therapist() {Id = Guid.NewGuid(), Name = "Ostap", Surname = "Hovda", ReceptionRoom = "Orthopedics", CanEdit = false},
                new Therapist() {Id = Guid.NewGuid(), Name = "Oksana", Surname = "Dohubets", ReceptionRoom = "Urology", CanEdit = false},
                new Therapist() {Id = Guid.NewGuid(), Name = "John", Surname = "Hams", ReceptionRoom = "Traumatology", CanEdit = false}
            };

            modelBuilder.Entity<Therapist>().HasData(therapists);

            List<Calendar> calendars = new List<Calendar>()
            {
                new Calendar() {Id = Guid.NewGuid(), TherapistId = therapists[0].Id },
                new Calendar() {Id = Guid.NewGuid(), TherapistId = therapists[1].Id },
                new Calendar() {Id = Guid.NewGuid(), TherapistId = therapists[2].Id },
                new Calendar() {Id = Guid.NewGuid(), TherapistId = therapists[3].Id },
                new Calendar() {Id = Guid.NewGuid(), TherapistId = therapists[4].Id }
            };
            modelBuilder.Entity<Calendar>().HasData(calendars);

            List<Record> records = new List<Record>()
            {
                new Record() {Id = Guid.NewGuid(), VisitTime = DateTime.Now.AddDays(-5), Frequency = 4, Text = "Broken leg", CalendarId = calendars[0].Id},
                new Record() {Id = Guid.NewGuid(), VisitTime = DateTime.Now.AddDays(-3), Frequency = 2, Text = "Tooth pain", CalendarId = calendars[1].Id},
                new Record() {Id = Guid.NewGuid(), VisitTime = DateTime.Now.AddDays(-1), Frequency = 1, Text = "Pain in foot", CalendarId = calendars[2].Id},
                new Record() {Id = Guid.NewGuid(), VisitTime = DateTime.Now.AddDays(-1), Frequency = 5, Text = "Groin pain", CalendarId = calendars[3].Id},
                new Record() {Id = Guid.NewGuid(), VisitTime = DateTime.Now.AddDays(-1), Frequency = 2, Text = "Broken hand", CalendarId = calendars[4].Id},
            };
            modelBuilder.Entity<Record>().HasData(records);

            base.OnModelCreating(modelBuilder);
        }
    }
}
