using System;
using System.Collections.Generic;

namespace AnthonyPuppo.SemanticKernel.NL2EF.Data.Models;

public partial class ProductModel
{
    public long ProductModelId { get; set; }

    public string Name { get; set; } = null!;

    public string? CatalogDescription { get; set; }

    public string Rowguid { get; set; } = null!;

    public byte[] ModifiedDate { get; set; } = null!;

    public virtual ICollection<ProductModelProductDescription> ProductModelProductDescriptions { get; set; } = new List<ProductModelProductDescription>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
