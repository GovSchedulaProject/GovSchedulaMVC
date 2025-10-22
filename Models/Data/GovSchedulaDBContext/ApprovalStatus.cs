using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class ApprovalStatus
{
    public int StatusId { get; set; }

    public string? Approval { get; set; }

    public virtual ICollection<DriverLicenceRegistration> DriverLicenceRegistrations { get; set; } = new List<DriverLicenceRegistration>();

    public virtual ICollection<GhanaCardRegistration> GhanaCardRegistrations { get; set; } = new List<GhanaCardRegistration>();

    public virtual ICollection<Nhisregistration> Nhisregistrations { get; set; } = new List<Nhisregistration>();

    public virtual ICollection<VoterIdregistration> VoterIdregistrations { get; set; } = new List<VoterIdregistration>();
}
