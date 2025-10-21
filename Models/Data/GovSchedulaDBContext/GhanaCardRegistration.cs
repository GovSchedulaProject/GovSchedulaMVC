using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class GhanaCardRegistration
{
    public int GhanaCardId { get; set; }

    public string Religion { get; set; } = null!;

    public string HomeTown { get; set; } = null!;

    public string? District { get; set; }

    public int FamilyId { get; set; }

    public int GeneralDetailsId { get; set; }

    public int StatusId { get; set; }

    public virtual Family Family { get; set; } = null!;

    public virtual GeneralDetail GeneralDetails { get; set; } = null!;
}
