using GovSchedulaWeb.Models.Data.GovSchedulaDBContext;
using GovSchedulaWeb.Models.ViewModels;

namespace GovSchedulaWeb.Models.Data.Services
{
    public class PassportService
    {
        private GovSchedulaDbContext _context;

        public PassportService(GovSchedulaDbContext context)
        {
            _context = context;
        }

        public async Task AddPassportAsync(
            PassportApplicationViewModel model,
            int userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Console.WriteLine($"Creating identity proof of type: {model.SelectedIdentityProofType}");

                // Create the identity proof based on type
                var identityProof = new IdentityProof();

                switch (model.SelectedIdentityProofType)
                {
                    case "GhanaCard":
                        if (model.GhanaCard == null)
                            throw new ArgumentException("Ghana Card data is required");

                        _context.GhanaCards.Add(model.GhanaCard);
                        await _context.SaveChangesAsync();
                        identityProof.GhanaCardId = model.GhanaCard.GhanaCardId;
                        Console.WriteLine($"Ghana Card saved with ID: {model.GhanaCard.GhanaCardId}");
                        break;

                    case "BirthCertificate":
                        if (model.BirthCertificate == null)
                            throw new ArgumentException("Birth Certificate data is required");

                        _context.BirthSets.Add(model.BirthCertificate);
                        await _context.SaveChangesAsync();
                        identityProof.BirthSetId = model.BirthCertificate.BirthSetId;
                        Console.WriteLine($"Birth Certificate saved with ID: {model.BirthCertificate.BirthSetId}");
                        break;

                    case "VoterId":
                        if (model.VoterId == null)
                            throw new ArgumentException("Voter ID data is required");

                        _context.VoterIds.Add(model.VoterId);
                        await _context.SaveChangesAsync();
                        identityProof.VoterId = model.VoterId.VoterId1;
                        Console.WriteLine($"Voter ID saved with ID: {model.VoterId.VoterId1}");
                        break;

                    case "NHIS":
                        if (model.Nhis == null)
                            throw new ArgumentException("NHIS data is required");

                        _context.Nhis.Add(model.Nhis);
                        await _context.SaveChangesAsync();
                        identityProof.Nhisid = model.Nhis.Nhisid;
                        Console.WriteLine($"NHIS saved with ID: {model.Nhis.Nhisid}");
                        break;

                    case "Guarantor":
                        if (model.Guarantor == null)
                            throw new ArgumentException("Guarantor data is required");

                        _context.Garantors.Add(model.Guarantor);
                        await _context.SaveChangesAsync();
                        identityProof.GarantorId = model.Guarantor.GarantorId;
                        Console.WriteLine($"Guarantor saved with ID: {model.Guarantor.GarantorId}");
                        break;

                    default:
                        throw new ArgumentException($"Invalid identity proof type: {model.SelectedIdentityProofType}");
                }

                // Saving identity proof
                _context.IdentityProofs.Add(identityProof);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Identity proof saved with ID: {identityProof.IdentityProof1}");

                // Linking identity proof to general details
                model.GeneralDetail.UserId = userId;
                model.GeneralDetail.DepartmentId = 1;
                model.GeneralDetail.IdentityProof = identityProof.IdentityProof1;

                // Saving general details
                _context.GeneralDetails.Add(model.GeneralDetail);
                await _context.SaveChangesAsync();
                Console.WriteLine($"General details saved with ID: {model.GeneralDetail.GeneralDetailsId}");

                // Saving family information if provided
                if (model.Family != null)
                {
                    _context.Families.Add(model.Family);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Family saved with ID: {model.Family.FamilyId}");

                    // Linking family to passport registration
                    model.PassportRegistration.FamilyId = model.Family.FamilyId;
                }


                model.PassportRegistration.GeneralDetailsId = model.GeneralDetail.GeneralDetailsId;
                model.PassportRegistration.StatusId = 1;
                _context.PassportRegistrations.Add(model.PassportRegistration);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Passport registration saved with ID: {model.PassportRegistration.PassportId}");

                // Commit transaction
                await transaction.CommitAsync();
                Console.WriteLine("Transaction committed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in PassportService: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}