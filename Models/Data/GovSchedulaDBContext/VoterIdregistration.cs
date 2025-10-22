using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class VoterIdregistration
{
    public int VoterId { get; set; }

    public int GeneralDetailsId { get; set; }

    public int? StatusId { get; set; }

    public virtual GeneralDetail GeneralDetails { get; set; } = null!;

    public virtual ApprovalStatus Status { get; set; } = null!;
}

