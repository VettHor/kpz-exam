using AutoMapper;
using back_end.DB;
using back_end.DTOs;
using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Services
{
    public interface ITherapistService
    {
        Task<Therapist?> GetTherapistById(Guid id);
        IQueryable<Therapist?> GetAllTherapists();
        Task AddTherapist(TherapistDto therapist);
        Task UpdateTherapist(TherapistDto therapist);
        Task<bool> DeleteTherapist(Guid id);
    }

    public class TherapistService : ITherapistService
    {
        private readonly DbSet<Therapist> _therapists;
        private readonly TherapistDbContext _context;
        private readonly IMapper _mapper;

        public TherapistService(TherapistDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _therapists = context.Set<Therapist>();
        }

        public async Task<Therapist?> GetTherapistById(Guid id)
        {
            return await _therapists.FirstOrDefaultAsync(mh => mh.Id == id);
        }

        public IQueryable<Therapist?> GetAllTherapists()
        {
            return _therapists.AsQueryable();
        }

        public async Task AddTherapist(TherapistDto therapist)
        {
            var newTherapist = _mapper.Map<Therapist>(therapist);
            await _context.AddAsync(newTherapist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTherapist(TherapistDto therapist)
        {
            if (!await _therapists.AnyAsync(h => h.Id == therapist.Id))
            {
                throw new ArgumentException($"Medical history with Id {therapist.Id} does not exist.");
            }

            var newTherapist = _mapper.Map<Therapist>(therapist);
            _context.Update(newTherapist);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteTherapist(Guid id)
        {
            var therapist = await GetTherapistById(id);
            if (therapist is null)
            {
                return false;
            }

            _context.Remove(therapist);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
