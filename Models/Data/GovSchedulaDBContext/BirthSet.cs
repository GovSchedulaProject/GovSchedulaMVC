using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class BirthSet
{
    public int BirthSetId { get; set; }

    public byte[] Image { get; set; } = null!;

    public virtual ICollection<IdentityProof> IdentityProofs { get; set; } = new List<IdentityProof>();
}
