using System;
using System.Collections.Generic;

namespace GovSchedulaWeb.Models.Data.GovSchedulaDBContext;

public partial class EyeTest
{
    public int EyeTestId { get; set; }

    public byte[] Image { get; set; } = null!;

    public virtual ICollection<DriverLicenceRegistration> DriverLicenceRegistrations { get; set; } = new List<DriverLicenceRegistration>();
}
