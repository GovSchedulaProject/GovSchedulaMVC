using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class Garantor
{
    public int GarantorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string OtherNames { get; set; } = null!;

    public string Profession { get; set; } = null!;

    public string? PostalAddress { get; set; }

    public string Email { get; set; } = null!;

    public int PhoneNumber { get; set; }

    public int? IdentityProof { get; set; }

    public virtual IdentityProof? IdentityProofNavigation { get; set; }

    public virtual ICollection<IdentityProof> IdentityProofs { get; set; } = new List<IdentityProof>();
}
