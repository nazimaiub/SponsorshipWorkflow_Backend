using Microsoft.EntityFrameworkCore;
using SponsorshipWorkflow.Api.Data;
using SponsorshipWorkflow.Api.Models;

namespace SponsorshipWorkflow.Api.Services
{
    public class RequestRepository : IRequestRepository
    {
        private readonly AppDbContext _context;

        public RequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SponsorshipRequest>> GetAllAsync()
        {
            return await _context.SponsorshipRequests.ToListAsync();
        }

        public async Task<SponsorshipRequest?> GetByIdAsync(Guid id)
        {
            return await _context.SponsorshipRequests
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Guid> SaveDraftAsync(SponsorshipRequest entity)
        {
            var existing = await _context.SponsorshipRequests
                .FirstOrDefaultAsync(x => x.RequestorId == entity.RequestorId);

            if (existing != null)
            {
                existing.RequestTitle = entity.RequestTitle;
                existing.Department = entity.Department;
                existing.SponsorshipType = entity.SponsorshipType;
                existing.EventName = entity.EventName;
                existing.EventDate = entity.EventDate;
                existing.RequestedAmount = entity.RequestedAmount;
                existing.Purpose = entity.Purpose;
                existing.RequestorRemarks = entity.RequestorRemarks ?? string.Empty;
                entity.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return existing.Id;
            }

            entity.Id = Guid.NewGuid();
            entity.Status = "Draft";
            entity.CreatedAt = DateTime.UtcNow;

            await _context.SponsorshipRequests.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<Guid> SubmitAsync(SponsorshipRequest entity)
        {
            if (entity.Id != Guid.Empty)
            {
                var existing = await _context.SponsorshipRequests
                    .FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (existing != null)
                {
                    existing.Status = "Pending Manager Approval";

                    await _context.SaveChangesAsync();
                    return existing.Id;
                }
            }

            entity.Id = Guid.NewGuid();
            entity.Status = "Pending Manager Approval";
            entity.CreatedAt = DateTime.UtcNow;

            await _context.SponsorshipRequests.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }
    }
}