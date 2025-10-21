using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class Adminlogin
{
    public int AdminId { get; set; }

    public int DepartmentId { get; set; }

    public int Ssn { get; set; }

    public string Password { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;
}
