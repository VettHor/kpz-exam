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
        IQueryable<Record?> GetAllRecordsByWord(string word);
        IQueryable<Record?> GetRecordsByCalendarId(Guid id);
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

        public IQueryable<Record?> GetRecordsByCalendarId(Guid id)
        {
            return _records.Where(mh => mh.CalendarId == id).AsQueryable();
        }

        public IQueryable<Record?> GetAllRecordsByWord(string word)
        {
            return _records.Where(mh => mh.Text.Contains(word)).AsQueryable();
        }

        public async Task AddRecord(RecordDto record)
        {
            var newRecord = _mapper.Map<Record>(record);
            await _context.AddAsync(newRecord);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecord(RecordDto record)
        {
            var recordToUpdate = await GetRecordById(record.Id);
            if(recordToUpdate is null)
            {
                throw new NullReferenceException();
            }
            _mapper.Map<RecordDto, Record>(record, recordToUpdate);
            _context.Update(recordToUpdate);
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
