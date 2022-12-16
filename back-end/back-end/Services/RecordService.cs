using AutoMapper;
using back_end.DB;
using back_end.DTOs;
using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Services
{
    public interface IRecordService
    {
        Task<Record?> GetRecordById(Guid id);
        IQueryable<Record?> GetAllRecords();
        Task AddRecord(RecordDto record);
        Task UpdateRecord(RecordDto record);
        Task<bool> DeleteRecord(Guid id);
    }

    public class RecordService : IRecordService
    {
        private readonly DbSet<Record> _records;
        private readonly TherapistDbContext _context;
        private readonly IMapper _mapper;

        public RecordService(TherapistDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _records = context.Set<Record>();
        }

        public async Task<Record?> GetRecordById(Guid id)
        {
            return await _records.FirstOrDefaultAsync(mh => mh.Id == id);
        }

        public IQueryable<Record?> GetAllRecords()
        {
            return _records.AsQueryable();
        }

        public async Task AddRecord(RecordDto record)
        {
            var newRecord = _mapper.Map<Record>(record);
            await _context.AddAsync(newRecord);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecord(RecordDto record)
        {
            if (!await _records.AnyAsync(h => h.Id == record.Id))
            {
                throw new ArgumentException($"Medical history with Id {record.Id} does not exist.");
            }

            var newRecord = _mapper.Map<Record>(record);
            _context.Update(newRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteRecord(Guid id)
        {
            var record = await GetRecordById(id);
            if (record is null)
            {
                return false;
            }

            _context.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
