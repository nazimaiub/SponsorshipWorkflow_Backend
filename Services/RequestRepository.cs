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
                return await _context.SponsorshipRequests.Where(x => (x.Status == "Pending Manager Approval") || (x.Status == "Rejected By Finance")).ToListAsync();

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
                        existing.Status = "Draft";
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
                SponsorshipRequest request;

                if (entity.Id != Guid.Empty)
                {
                    request = await _context.SponsorshipRequests.FirstOrDefaultAsync(x => x.Id == entity.Id);

                    if (request == null)
                        throw new Exception("Request not found");

                    request.RequestTitle = entity.RequestTitle;
                    request.Department = entity.Department;
                    request.SponsorshipType = entity.SponsorshipType;
                    request.EventName = entity.EventName;
                    request.EventDate = DateTime.SpecifyKind(entity.EventDate ?? DateTime.UtcNow, DateTimeKind.Utc);
                    request.RequestedAmount = entity.RequestedAmount;
                    request.Purpose = entity.Purpose;
                    request.RequestorRemarks = entity.RequestorRemarks ?? string.Empty;
                    request.UpdatedAt = DateTime.UtcNow;
                    request.Status = "Pending Manager Approval";
                }
                else
                {
                    request = entity;
                    request.Id = Guid.NewGuid();
                    request.Status = "Pending Manager Approval";
                    request.CreatedAt = DateTime.UtcNow;
                    request.EventDate = DateTime.SpecifyKind(entity.EventDate ?? DateTime.UtcNow, DateTimeKind.Utc);

                    await _context.SponsorshipRequests.AddAsync(request);
                }

                var history = new RequestWorkflowHistory
                {
                    RequestId = request.Id,
                    ActionByUserId = request.RequestorId,
                    OldStatus = "Draft",
                    NewStatus = "Pending Manager Approval",
                    Remarks = request.RequestorRemarks,
                    ActionDate = DateTime.UtcNow
                };

                await _context.RequestWorkflowHistories.AddAsync(history);

                await _context.SaveChangesAsync();

                return request.Id;
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message;
                throw new Exception(inner ?? ex.Message);
            }
        }

        public async Task<Guid> CancelByRequestor(Guid id)
        {
            var entity = await _context.SponsorshipRequests
                     .FirstOrDefaultAsync(x => x.Id == id);

            entity.Status = "Cancel By Requestor";
            entity.UpdatedAt = DateTime.UtcNow;

            _context.SponsorshipRequests.Update(entity);

            var history = new RequestWorkflowHistory
            {
                RequestId = entity.Id,
                ActionByUserId = entity.RequestorId,
                OldStatus = "Pending Manager Approval",
                NewStatus = "Cancel By Requestor",
                Remarks = entity.RequestorRemarks,
                ActionDate = DateTime.UtcNow
            };
            await _context.RequestWorkflowHistories.AddAsync(history);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> ManagerApproveAsync(Guid id, string managerId, string remarks)
        {
            var entity = await _context.SponsorshipRequests
                    .FirstOrDefaultAsync(x => x.Id == id);

            entity.ManagerId = managerId;
            entity.Status = "Pending Finance Review";
            entity.UpdatedAt = DateTime.UtcNow;
            entity.ManagerRemarks = remarks;
            _context.SponsorshipRequests.Update(entity);

            var history = new RequestWorkflowHistory
            {
                RequestId = entity.Id,
                ActionByUserId = entity.RequestorId,
                OldStatus = "Pending Manager Approval",
                NewStatus = "Pending Finance Approval",
                Remarks = entity.ManagerRemarks,
                ActionDate = DateTime.UtcNow
            };

            await _context.RequestWorkflowHistories.AddAsync(history);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> ManagerRejectAsync(Guid id, string managerId, string remarks)
        {
            var entity = await _context.SponsorshipRequests
                     .FirstOrDefaultAsync(x => x.Id == id);

            entity.ManagerId = managerId;
            entity.Status = "Rejected By Manager";
            entity.UpdatedAt = DateTime.UtcNow;
            entity.ManagerRemarks = remarks;
            _context.SponsorshipRequests.Update(entity);

            var history = new RequestWorkflowHistory
            {
                RequestId = entity.Id,
                ActionByUserId = entity.RequestorId,
                OldStatus = "Pending Manager Approval",
                NewStatus = "Rejected By Manager",
                Remarks = entity.ManagerRemarks,
                ActionDate = DateTime.UtcNow
            };
            await _context.RequestWorkflowHistories.AddAsync(history);

            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> FinanceApproveAsync(Guid id, string financeId, string remarks)
        {
            var entity = await _context.SponsorshipRequests
                     .FirstOrDefaultAsync(x => x.Id == id);

            entity.FinanceId = financeId;
            entity.Status = "Approved";
            entity.UpdatedAt = DateTime.UtcNow;
            entity.FinanceRemarks = remarks;
            _context.SponsorshipRequests.Update(entity);

            var history = new RequestWorkflowHistory
            {
                RequestId = entity.Id,
                ActionByUserId = entity.RequestorId,
                OldStatus = "Pending Finance Approval",
                NewStatus = "Approved",
                Remarks = entity.FinanceRemarks,
                ActionDate = DateTime.UtcNow
            };
            await _context.RequestWorkflowHistories.AddAsync(history);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> FinanceRejectAsync(Guid id, string financeId, string remarks)
        {
            var entity = await _context.SponsorshipRequests
                       .FirstOrDefaultAsync(x => x.Id == id);

            entity.FinanceId = financeId;
            entity.Status = "Rejected By Finance";
            entity.UpdatedAt = DateTime.UtcNow;
            entity.FinanceRemarks = remarks;

            _context.SponsorshipRequests.Update(entity);

            var history = new RequestWorkflowHistory
            {
                RequestId = entity.Id,
                ActionByUserId = entity.RequestorId,
                OldStatus = "Pending Finance Approval",
                NewStatus = "Rejected By Finance",
                Remarks = entity.FinanceRemarks,
                ActionDate = DateTime.UtcNow
            };
            await _context.RequestWorkflowHistories.AddAsync(history);

            await _context.SaveChangesAsync();
            return entity.Id;
        }
    }
}