using AutoMapper;
using back_end.DB;
using back_end.DTOs;
using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Services
{
    public interface ICalendarService
    {
        Task<Calendar?> GetCalendarById(Guid id);
        IQueryable<Calendar?> GetAllCalendars();
        Task AddCalendar(CalendarDto history);
        Task UpdateCalendar(CalendarDto history);
        Task<bool> DeleteCalendar(Guid id);
    }

    public class CalendarService : ICalendarService
    {
        private readonly DbSet<Calendar> _calendars;
        private readonly TherapistDbContext _context;
        private readonly IMapper _mapper;

        public CalendarService(TherapistDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _calendars = context.Set<Calendar>();
        }

        public async Task<Calendar?> GetCalendarById(Guid id)
        {
            return await _calendars.FirstOrDefaultAsync(mh => mh.Id == id);
        }

        public IQueryable<Calendar?> GetAllCalendars()
        {
            return _calendars.AsQueryable();
        }

        public async Task AddCalendar(CalendarDto calendar)
        {
            var newCalendar = _mapper.Map<Calendar>(calendar);
            await _context.AddAsync(newCalendar);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCalendar(CalendarDto history)
        {
            if (!await _calendars.AnyAsync(h => h.Id == history.Id))
            {
                throw new ArgumentException($"Medical history with Id {history.Id} does not exist.");
            }

            var newCalendar = _mapper.Map<Calendar>(history);
            _context.Update(newCalendar);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCalendar(Guid id)
        {
            var calendar = await GetCalendarById(id);
            if (calendar is null)
            {
                return false;
            }

            _context.Remove(calendar);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
