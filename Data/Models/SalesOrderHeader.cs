using System;
using System.Collections.Generic;

namespace AnthonyPuppo.SemanticKernel.NL2EF.Data.Models;

public partial class SalesOrderHeader
{
    public long SalesOrderId { get; set; }

    public long RevisionNumber { get; set; }

    public byte[] OrderDate { get; set; } = null!;

    public byte[] DueDate { get; set; } = null!;

    public byte[]? ShipDate { get; set; }

    public long Status { get; set; }

    public long OnlineOrderFlag { get; set; }

    public string SalesOrderNumber { get; set; } = null!;

    public long? PurchaseOrderNumber { get; set; }

    public string? AccountNumber { get; set; }

    public long CustomerId { get; set; }

    public long? ShipToAddressId { get; set; }

    public long? BillToAddressId { get; set; }

    public string ShipMethod { get; set; } = null!;

    public string? CreditCardApprovalCode { get; set; }

    public long SubTotal { get; set; }

    public long TaxAmt { get; set; }

    public long Freight { get; set; }

    public long TotalDue { get; set; }

    public string? Comment { get; set; }

    public string Rowguid { get; set; } = null!;

    public byte[] ModifiedDate { get; set; } = null!;

    public virtual Address? BillToAddress { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; } = new List<SalesOrderDetail>();

    public virtual Address? ShipToAddress { get; set; }
}
