using GovSchedulaWeb.Models.Data.GovSchedulaDBContext;
using GovSchedulaWeb.Models.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GovSchedulaWeb.Models.Data.Services
{
    public class AdminService
    {
        private GovSchedulaDbContext _context;
        public AdminService(GovSchedulaDbContext context)
        {
            _context = context;
        }

        public async Task<AdminLoginViewModel?> AuthenticateAsync(int ssn, string password)
        {
            var admin = await _context.Adminlogins
                .Where(a => a.Ssn == ssn && a.Password == password)
                .Select(a => new AdminLoginViewModel
                {
                    AdminId = a.AdminId,
                    DepartmentId = a.DepartmentId,
                    Ssn = a.Ssn,
                    Password = a.Password
                })
                .FirstOrDefaultAsync();

            return admin;
        }
        public async Task<AdminLoginViewModel?> GetAdminByIdAsync(int adminId)
        {
            var admin = await _context.Adminlogins
                .FirstOrDefaultAsync(a => a.AdminId == adminId);

            if (admin == null) return null;

            return new AdminLoginViewModel
            {
                AdminId = admin.AdminId,
                DepartmentId = admin.DepartmentId,
                Ssn = admin.Ssn,
                Password = admin.Password
            };
        }


        public async Task<AdminDashboardViewModel> GetDashboardDataAsync(int departmentId)
        {
            int pendingCount = 0;
            int approvedCount = 0;
            int rejectedCount = 0;
            string departmentName = "";

            if (departmentId == 1) // Passport Department
            {
                pendingCount = await _context.PassportRegistrations.CountAsync(p => p.StatusId == 1);
                approvedCount = await _context.PassportRegistrations.CountAsync(p => p.StatusId == 2);
                rejectedCount = await _context.PassportRegistrations.CountAsync(p => p.StatusId == 3);
                departmentName = "Department Of Passport";
            }
            else if (departmentId == 2) // Voter ID Department
            {
                pendingCount = await _context.VoterIdregistrations.CountAsync(v => v.StatusId == 1);
                approvedCount = await _context.VoterIdregistrations.CountAsync(v => v.StatusId == 2);
                rejectedCount = await _context.VoterIdregistrations.CountAsync(v => v.StatusId == 3);
                departmentName = "Electoral Commission";
            }
            else if (departmentId == 3) // ghanacard ID Department
            {
                pendingCount = await _context.VoterIdregistrations.CountAsync(v => v.StatusId == 1);
                approvedCount = await _context.VoterIdregistrations.CountAsync(v => v.StatusId == 2);
                rejectedCount = await _context.VoterIdregistrations.CountAsync(v => v.StatusId == 3);
                departmentName = "National Indentification Authority";
            }
            // Add other departments (3 and 4) when you have those tables

            return new AdminDashboardViewModel
            {
                PendingCount = pendingCount,
                ApprovedCount = approvedCount,
                RejectedCount = rejectedCount,
                DepartmentName = departmentName
            };
        }

        public async Task<List<AdminViewModel>> GetVoterApplicationsByDepartmentAsync(int departmentId, int? statusId = null)
        {
            var query = _context.VoterIdregistrations
                .Include(v => v.Status)
                .Include(v => v.GeneralDetails)
                    .ThenInclude(g => g.Department)
                .Include(v => v.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.GhanaCard)
                .Where(v => v.GeneralDetails.DepartmentId == departmentId);

            if (statusId.HasValue)
                query = query.Where(v => v.StatusId == statusId.Value);

            var result = await query
                .Select(v => new AdminViewModel
                {
                    GeneralDetails = v.GeneralDetails,
                    Department = v.GeneralDetails.Department,
                    IdentityProof = v.GeneralDetails.IdentityProofNavigation,
                    GhanaCard = v.GeneralDetails.IdentityProofNavigation.GhanaCard,
                    VoterIdregistration = v,
                    ApprovalStatus = v.Status
                })
                .OrderByDescending(v => v.GeneralDetails.GeneralDetailsId)
                .ToListAsync();

            return result;
        }

        public async Task<bool> UpdateVoterApplicationStatusAsync(int voterIdRegistrationId, int newStatusId)
        {
            var record = await _context.VoterIdregistrations
                .FirstOrDefaultAsync(v => v.VoterId == voterIdRegistrationId);

            if (record == null)
                return false;

            record.StatusId = newStatusId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ApprovalStatus>> GetAllStatusesAsync()
        {
            return await _context.ApprovalStatuses.ToListAsync();
        }


        public async Task<AdminViewModel?> GetVoterApplicationDetailsAsync(int voterIdRegistrationId)
        {
            var v = await _context.VoterIdregistrations
                .Include(v => v.Status)
                .Include(v => v.GeneralDetails)
                    .ThenInclude(g => g.Department)
                .Include(v => v.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.GhanaCard)
                .FirstOrDefaultAsync(v => v.VoterId == voterIdRegistrationId);

            if (v == null)
                return null;

            return new AdminViewModel
            {
                GeneralDetails = v.GeneralDetails,
                Department = v.GeneralDetails.Department,
                IdentityProof = v.GeneralDetails.IdentityProofNavigation,
                GhanaCard = v.GeneralDetails.IdentityProofNavigation?.GhanaCard,
                VoterIdregistration = v,
                ApprovalStatus = v.Status
            };
        }


        //Passport


        public async Task<List<AdminViewModel>> GetPassportApplicationsByDepartmentAsync(int departmentId, int? statusId = null)
        {
            var query = _context.PassportRegistrations
                .Include(p => p.Status)
                .Include(p => p.Family)
                .Include(p => p.GeneralDetails)
                    .ThenInclude(g => g.Department)
                .Include(p => p.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.GhanaCard)
                .Include(p => p.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.Nhis)
                .Include(p => p.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.Voter)
                .Include(p => p.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.BirthSet)
                .Include(p => p.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.Garantor)
                .Where(p => p.GeneralDetails.DepartmentId == departmentId);

            if (statusId.HasValue)
                query = query.Where(v => v.StatusId == statusId.Value);

            var result = await query
                .Select(p => new AdminViewModel
                {
                    GeneralDetails = p.GeneralDetails,
                    Department = p.GeneralDetails.Department,
                    IdentityProof = p.GeneralDetails.IdentityProofNavigation,
                    GhanaCard = p.GeneralDetails.IdentityProofNavigation.GhanaCard,
                    PassportRegistration = p,
                    ApprovalStatus = p.Status
                })
                .OrderByDescending(p => p.GeneralDetails.GeneralDetailsId)
                .ToListAsync();

            return result;
        }

        public async Task<bool> UpdatePassportApplicationStatusAsync(int passportId, int newStatusId)
        {
            var record = await _context.PassportRegistrations
                .FirstOrDefaultAsync(v => v.PassportId == passportId);

            if (record == null)
                return false;

            record.StatusId = newStatusId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AdminViewModel?> GetPassportApplicationDetailsAsync(int passportId)
        {
            var p = await _context.PassportRegistrations
                .Include(p => p.Status)
                .Include(p => p.Family)
                .Include(p => p.GeneralDetails)
                    .ThenInclude(g => g.Department)
                .Include(p => p.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.GhanaCard)
                .Include(p => p.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.Nhis)
                .Include(p => p.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.Voter)
                .Include(p => p.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.BirthSet)
                .Include(p => p.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.Garantor)
                .FirstOrDefaultAsync(p => p.PassportId == passportId);

            if (p == null)
                return null;

            return new AdminViewModel
            {
                GeneralDetails = p.GeneralDetails,
                Department = p.GeneralDetails.Department,
                IdentityProof = p.GeneralDetails.IdentityProofNavigation,
                Family = p.Family,
                //GhanaCard = p.GeneralDetails.IdentityProofNavigation?.GhanaCard,
                PassportRegistration = p,
                ApprovalStatus = p.Status
            };
        }


        //NIA ghana card

        public async Task<List<AdminViewModel>> GetNiaApplicationsByDepartmentAsync(int departmentId, int? statusId = null)
        {
            var query = _context.GhanaCardRegistrations
                .Include(n => n.Status)
                .Include(n => n.Family)
                .Include(n => n.GeneralDetails)
                    .ThenInclude(g => g.Department)
                .Include(n => n.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.GhanaCard)
                .Include(n => n.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.Nhis)
                .Include(n => n.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.Voter)
                .Include(n => n.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.BirthSet)
                .Include(n => n.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.Garantor)
                .Where(n => n.GeneralDetails.DepartmentId == departmentId);

            if (statusId.HasValue)
                query = query.Where(n => n.StatusId == statusId.Value);

            var result = await query
                .Select(n => new AdminViewModel
                {
                    GeneralDetails = n.GeneralDetails,
                    Department = n.GeneralDetails.Department,
                    IdentityProof = n.GeneralDetails.IdentityProofNavigation,
                    //GhanaCard = n.GeneralDetails.IdentityProofNavigation.GhanaCard,
                    GhanaCardRegistration = n,
                    ApprovalStatus = n.Status
                })
                .OrderByDescending(n => n.GeneralDetails.GeneralDetailsId)
                .ToListAsync();

            return result;
        }

        public async Task<bool> UpdateNiaApplicationStatusAsync(int ghanaCardRegistrationId, int newStatusId)
        {
            var record = await _context.GhanaCardRegistrations
                .FirstOrDefaultAsync(n => n.GhanaCardId == ghanaCardRegistrationId);

            if (record == null)
                return false;

            record.StatusId = newStatusId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AdminViewModel?> GetNiaApplicationDetailsAsync(int ghanaCardRegistrationId)
        {
            var n = await _context.GhanaCardRegistrations
                .Include(n => n.Status)
                .Include(n => n.Family)
                .Include(n => n.GeneralDetails)
                    .ThenInclude(g => g.Department)
                .Include(n => n.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.GhanaCard)
                .Include(n => n.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.Nhis)
                .Include(n => n.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.Voter)
                .Include(n => n.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.BirthSet)
                .Include(n => n.GeneralDetails.IdentityProofNavigation)
                    .ThenInclude(i => i.Garantor)
                .FirstOrDefaultAsync(n => n.GhanaCardId == ghanaCardRegistrationId);

            if (n == null)
                return null;

            return new AdminViewModel
            {
                GeneralDetails = n.GeneralDetails,
                Department = n.GeneralDetails.Department,
                IdentityProof = n.GeneralDetails.IdentityProofNavigation,
                //GhanaCard = n.GeneralDetails.IdentityProofNavigation?.GhanaCard,
                Family = n.Family,
                GhanaCardRegistration = n,
                ApprovalStatus = n.Status
            };
        }
    }
}
