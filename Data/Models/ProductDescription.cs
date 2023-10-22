using System;
using System.Collections.Generic;

namespace AnthonyPuppo.SemanticKernel.NL2EF.Data.Models;

public partial class ProductDescription
{
    public long ProductDescriptionId { get; set; }

    public string Description { get; set; } = null!;

    public string Rowguid { get; set; } = null!;

    public byte[] ModifiedDate { get; set; } = null!;

    public virtual ICollection<ProductModelProductDescription> ProductModelProductDescriptions { get; set; } = new List<ProductModelProductDescription>();
}
