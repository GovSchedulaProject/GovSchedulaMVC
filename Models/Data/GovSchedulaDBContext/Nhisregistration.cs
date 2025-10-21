using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class Nhisregistration
{
    public int Nhisid { get; set; }

    public bool MaritalStatus { get; set; }

    public string EducationalLevel { get; set; } = null!;

    public int DependantId { get; set; }

    public int GeneralDetailsId { get; set; }

    public int NextofKinId { get; set; }

    public int? BloodGroupId { get; set; }

    public int StatusId { get; set; }

    public virtual BloodGroup? BloodGroup { get; set; }

    public virtual Dependant Dependant { get; set; } = null!;

    public virtual GeneralDetail GeneralDetails { get; set; } = null!;

    public virtual NextofKin NextofKin { get; set; } = null!;
}
