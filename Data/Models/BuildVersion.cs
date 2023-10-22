using System;
using System.Collections.Generic;

namespace AnthonyPuppo.SemanticKernel.NL2EF.Data.Models;

public partial class BuildVersion
{
    public long SystemInformationId { get; set; }

    public string DatabaseVersion { get; set; } = null!;

    public byte[] VersionDate { get; set; } = null!;

    public byte[] ModifiedDate { get; set; } = null!;
}
