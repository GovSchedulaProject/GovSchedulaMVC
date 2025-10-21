using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class IdentityProof
{
    public int IdentityProof1 { get; set; }

    public int? GhanaCardId { get; set; }

    public int? BirthSetId { get; set; }

    public int? GarantorId { get; set; }

    public int? Nhisid { get; set; }

    public int? VoterId { get; set; }

    public virtual BirthSet? BirthSet { get; set; }

    public virtual Garantor? Garantor { get; set; }

    public virtual ICollection<Garantor> Garantors { get; set; } = new List<Garantor>();

    public virtual ICollection<GeneralDetail> GeneralDetails { get; set; } = new List<GeneralDetail>();

    public virtual GhanaCard? GhanaCard { get; set; }

    public virtual Nhi? Nhis { get; set; }

    public virtual VoterId? Voter { get; set; }
}
