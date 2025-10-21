using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class VoterId
{
    public int VoterId1 { get; set; }

    public int VoterIdnumber { get; set; }

    public virtual ICollection<IdentityProof> IdentityProofs { get; set; } = new List<IdentityProof>();
}
