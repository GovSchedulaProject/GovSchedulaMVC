using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class Family
{
    public int FamilyId { get; set; }

    public string FatherFullName { get; set; } = null!;

    public string FatherHomeTown { get; set; } = null!;

    public string MotherFullName { get; set; } = null!;

    public string MotherHomeTown { get; set; } = null!;

    public string SpouseName { get; set; } = null!;

    public string SpouseNationality { get; set; } = null!;

    public virtual ICollection<GhanaCardRegistration> GhanaCardRegistrations { get; set; } = new List<GhanaCardRegistration>();

    public virtual ICollection<PassportRegistration> PassportRegistrations { get; set; } = new List<PassportRegistration>();
}
