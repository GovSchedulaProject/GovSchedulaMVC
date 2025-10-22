using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class GhanaCard
{
    public int GhanaCardId { get; set; }

    public string GhanaCardIdnumber { get; set; } = null!;

    public virtual ICollection<IdentityProof> IdentityProofs { get; set; } = new List<IdentityProof>();
}
