using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class DriverLicenceRegistration
{
    public int DriverLicenceId { get; set; }

    public int BloodGroupId { get; set; }

    public int NextofKinId { get; set; }

    public int? PreviousLicenceNumber { get; set; }

    public string VehicleClass { get; set; } = null!;

    public int EyeTestId { get; set; }

    public int GeneralDetailsId { get; set; }

    public int? StatusId { get; set; }

    public virtual BloodGroup BloodGroup { get; set; } = null!;

    public virtual EyeTest EyeTest { get; set; } = null!;

    public virtual GeneralDetail GeneralDetails { get; set; } = null!;

    public virtual NextofKin NextofKin { get; set; } = null!;

    public virtual ApprovalStatus Status { get; set; } = null!;
}
