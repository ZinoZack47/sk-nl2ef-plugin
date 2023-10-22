using System;
using System.Collections.Generic;

namespace AnthonyPuppo.SemanticKernel.NL2EF.Data.Models;

public partial class SalesOrderDetail
{
    public long SalesOrderId { get; set; }

    public long SalesOrderDetailId { get; set; }

    public long OrderQty { get; set; }

    public long ProductId { get; set; }

    public long UnitPrice { get; set; }

    public long UnitPriceDiscount { get; set; }

    public long LineTotal { get; set; }

    public string Rowguid { get; set; } = null!;

    public byte[] ModifiedDate { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual SalesOrderHeader SalesOrder { get; set; } = null!;
}
