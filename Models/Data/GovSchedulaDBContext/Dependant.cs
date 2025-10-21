using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class Dependant
{
    public int DependantId { get; set; }

    public string DependantName { get; set; } = null!;

    public string Relationship { get; set; } = null!;

    public DateOnly Bdate { get; set; }

    public virtual ICollection<Nhisregistration> Nhisregistrations { get; set; } = new List<Nhisregistration>();
}
