using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class BloodGroup
{
    public int BloodGroupId { get; set; }

    public string? BloodGroup1 { get; set; }

    public string? ChronicIllness { get; set; }

    public virtual ICollection<DriverLicenceRegistration> DriverLicenceRegistrations { get; set; } = new List<DriverLicenceRegistration>();

    public virtual ICollection<Nhisregistration> Nhisregistrations { get; set; } = new List<Nhisregistration>();
}
