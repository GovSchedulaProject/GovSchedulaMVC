using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class Nhi
{
    public int Nhisid { get; set; }

    public string? Nhisidnumber { get; set; }

    public virtual ICollection<IdentityProof> IdentityProofs { get; set; } = new List<IdentityProof>();
}
