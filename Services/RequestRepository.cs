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

        public async Task<List<SponsorshipRequest>> GetAllMyRequests(string userEmail, string role)
        {
            if (role == "requestor")
            {
                return await _context.SponsorshipRequests.Where(x => x.RequestorId == userEmail).ToListAsync();

            }
            else if (role == "manager")
            {
                return await _context.SponsorshipRequests.Where(x => x.Status == "Pending Manager Approval").ToListAsync();

            }
            else if (role == "finance")
            {
                return await _context.SponsorshipRequests.Where(x => x.Status == "Pending Finance Review").ToListAsync();

            }
            else
            {
                return await _context.SponsorshipRequests.ToListAsync();

            }
        }

        public async Task<SponsorshipRequest?> GetSponsorshipRequestsByIdAsync(Guid id)
        {
            return await _context.SponsorshipRequests
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Guid> SaveDraftAsync(SponsorshipRequest entity)
        {
            try
            {
                if (entity.Id != Guid.Empty)
                {
                    var existing = await _context.SponsorshipRequests
                    .FirstOrDefaultAsync(x => x.RequestorId == entity.RequestorId && x.Id == entity.Id);

                    if (existing != null)
                    {
                        existing.RequestTitle = entity.RequestTitle;
                        existing.Department = entity.Department;
                        existing.SponsorshipType = entity.SponsorshipType;
                        existing.EventName = entity.EventName;
                        existing.EventDate = DateTime.SpecifyKind(entity.EventDate ?? DateTime.UtcNow, DateTimeKind.Utc);
                        existing.RequestedAmount = entity.RequestedAmount;
                        existing.Purpose = entity.Purpose;
                        existing.RequestorRemarks = entity.RequestorRemarks ?? string.Empty;
                        existing.UpdatedAt = DateTime.UtcNow;
                        await _context.SaveChangesAsync();
                        return existing.Id;
                    }
                }

                entity.Id = Guid.NewGuid();
                entity.Status = "Draft";
                entity.CreatedAt = DateTime.UtcNow;
                entity.EventDate = DateTime.SpecifyKind(entity.EventDate ?? DateTime.UtcNow, DateTimeKind.Utc);
                await _context.SponsorshipRequests.AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message;
                throw new Exception(inner ?? ex.Message);
            }
        }

        public async Task<Guid> SubmitAsync(SponsorshipRequest entity)
        {
            try
            {
                if (entity.Id != Guid.Empty)
                {
                    var existing = await _context.SponsorshipRequests
                    .FirstOrDefaultAsync(x => x.Id == entity.Id);

                    if (existing != null)
                    {
                        existing.RequestTitle = entity.RequestTitle;
                        existing.Department = entity.Department;
                        existing.SponsorshipType = entity.SponsorshipType;
                        existing.EventName = entity.EventName;
                        existing.EventDate = DateTime.SpecifyKind(entity.EventDate ?? DateTime.UtcNow, DateTimeKind.Utc);
                        existing.RequestedAmount = entity.RequestedAmount;
                        existing.Purpose = entity.Purpose;
                        existing.RequestorRemarks = entity.RequestorRemarks ?? string.Empty;
                        existing.UpdatedAt = DateTime.UtcNow;
                        existing.Status = "Pending Manager Approval";
                        await _context.SaveChangesAsync();
                        return existing.Id;
                    }
                }
                entity.Id = Guid.NewGuid();
                entity.Status = "Pending Manager Approval";
                entity.CreatedAt = DateTime.UtcNow;
                entity.EventDate = DateTime.SpecifyKind(entity.EventDate ?? DateTime.UtcNow, DateTimeKind.Utc);
                await _context.SponsorshipRequests.AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message;
                throw new Exception(inner ?? ex.Message);
            }
        }

        public async Task<Guid> ManagerApproveAsync(SponsorshipRequest entity, string managerId)
        {
            entity.ManagerId = managerId;
            entity.Status = "Pending Finance Review";
            entity.UpdatedAt = DateTime.UtcNow;

            _context.SponsorshipRequests.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> ManagerRejectAsync(SponsorshipRequest entity, string managerId)
        {
            entity.ManagerId = managerId;
            entity.Status = "Rejected By Manager";
            entity.UpdatedAt = DateTime.UtcNow;

            _context.SponsorshipRequests.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> FinanceApproveAsync(SponsorshipRequest entity, string financeId)
        {
            entity.FinanceId = financeId;
            entity.Status = "Approved";
            entity.UpdatedAt = DateTime.UtcNow;

            _context.SponsorshipRequests.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> FinanceRejectAsync(SponsorshipRequest entity, string financeId)
        {
            entity.FinanceId = financeId;
            entity.Status = "Rejected By Finance";
            entity.UpdatedAt = DateTime.UtcNow;

            _context.SponsorshipRequests.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> ManagerApproveAsync(Guid id, string managerId)
        {
            var entity = await _context.SponsorshipRequests
                    .FirstOrDefaultAsync(x => x.Id == id);

            entity.ManagerId = managerId;
            entity.Status = "Pending Finance Review";
            entity.UpdatedAt = DateTime.UtcNow;

            _context.SponsorshipRequests.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> ManagerRejectAsync(Guid id, string managerId)
        {
            var entity = await _context.SponsorshipRequests
                     .FirstOrDefaultAsync(x => x.Id == id);

            entity.ManagerId = managerId;
            entity.Status = "Rejected By Manager";
            entity.UpdatedAt = DateTime.UtcNow;

            _context.SponsorshipRequests.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> FinanceApproveAsync(Guid id, string financeId)
        {
            var entity = await _context.SponsorshipRequests
                     .FirstOrDefaultAsync(x => x.Id == id);

            entity.FinanceId = financeId;
            entity.Status = "Pending Finance Review";
            entity.UpdatedAt = DateTime.UtcNow;

            _context.SponsorshipRequests.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> FinanceRejectAsync(Guid id, string financeId)
        {
            var entity = await _context.SponsorshipRequests
                       .FirstOrDefaultAsync(x => x.Id == id);

            entity.FinanceId = financeId;
            entity.Status = "Rejected By Finance";
            entity.UpdatedAt = DateTime.UtcNow;

            _context.SponsorshipRequests.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
    }
}