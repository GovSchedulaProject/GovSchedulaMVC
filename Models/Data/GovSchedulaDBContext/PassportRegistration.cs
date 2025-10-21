using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class PassportRegistration
{
    public int PassportId { get; set; }

    public int FamilyId { get; set; }

    public string ReasonForApply { get; set; } = null!;

    public string TypeOfPassport { get; set; } = null!;

    public string CountryDestination { get; set; } = null!;

    public int GeneralDetailsId { get; set; }

    public int StatusId { get; set; }

    public virtual Family Family { get; set; } = null!;

    public virtual GeneralDetail GeneralDetails { get; set; } = null!;
}
