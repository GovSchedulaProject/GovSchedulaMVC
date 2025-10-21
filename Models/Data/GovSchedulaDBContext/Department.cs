using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<Adminlogin> Adminlogins { get; set; } = new List<Adminlogin>();

    public virtual ICollection<GeneralDetail> GeneralDetails { get; set; } = new List<GeneralDetail>();
}
