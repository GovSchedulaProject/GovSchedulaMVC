using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class NextofKin
{
    public int NextofKinId { get; set; }

    public string NextofKin1 { get; set; } = null!;

    public string Relationship { get; set; } = null!;

    public virtual ICollection<DriverLicenceRegistration> DriverLicenceRegistrations { get; set; } = new List<DriverLicenceRegistration>();

    public virtual ICollection<Nhisregistration> Nhisregistrations { get; set; } = new List<Nhisregistration>();
}
