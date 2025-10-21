using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class GeneralDetail
{
    public int GeneralDetailsId { get; set; }

    public int IdentityProof { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public DateOnly Bdate { get; set; }

    public string BirthPlace { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public string? ResidentalAddress { get; set; }

    public string? PostalAddress { get; set; }

    public string Occupation { get; set; } = null!;

    public int PhoneNumber { get; set; }

    public string Email { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int UserId { get; set; }

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<DriverLicenceRegistration> DriverLicenceRegistrations { get; set; } = new List<DriverLicenceRegistration>();

    public virtual ICollection<GhanaCardRegistration> GhanaCardRegistrations { get; set; } = new List<GhanaCardRegistration>();

    public virtual IdentityProof IdentityProofNavigation { get; set; } = null!;

    public virtual ICollection<Nhisregistration> Nhisregistrations { get; set; } = new List<Nhisregistration>();

    public virtual ICollection<PassportRegistration> PassportRegistrations { get; set; } = new List<PassportRegistration>();

    public virtual Userprofile User { get; set; } = null!;

    public virtual ICollection<VoterIdregistration> VoterIdregistrations { get; set; } = new List<VoterIdregistration>();
}
