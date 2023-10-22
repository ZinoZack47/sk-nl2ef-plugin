using AnthonyPuppo.SemanticKernel.NL2EF.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AnthonyPuppo.SemanticKernel.NL2EF.Data;

public partial class AppDbContext : DbContext
{
    public const string SchemaMemoryCollectionName = $"Schema-{nameof(AppDbContext)}";

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<BuildVersion> BuildVersions { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductDescription> ProductDescriptions { get; set; }

    public virtual DbSet<ProductModel> ProductModels { get; set; }

    public virtual DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }

    public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

    public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=AdventureWorksLT.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.HasIndex(e => e.Rowguid, "IX_Address_rowguid").IsUnique();

            entity.HasIndex(e => new { e.AddressLine1, e.AddressLine2, e.City, e.StateProvince, e.PostalCode, e.CountryRegion }, "IX_Address_AddressLine1_AddressLine2_City_StateProvince_PostalCode_CountryRegion");

            entity.HasIndex(e => e.StateProvince, "IX_Address_StateProvince");

            entity.Property(e => e.AddressId)
                .ValueGeneratedNever()
                .HasColumnName("AddressID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("datetime('now')")
                .HasColumnType("DATETIME");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
        });

        modelBuilder.Entity<BuildVersion>(entity =>
        {
            entity.HasKey(e => e.SystemInformationId);

            entity.ToTable("BuildVersion");

            entity.Property(e => e.SystemInformationId).HasColumnName("SystemInformationID");
            entity.Property(e => e.DatabaseVersion).HasColumnName("Database Version");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("datetime('now')")
                .HasColumnType("DATETIME");
            entity.Property(e => e.VersionDate).HasColumnType("DATETIME");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.HasIndex(e => e.Rowguid, "IX_Customer_rowguid").IsUnique();

            entity.HasIndex(e => e.EmailAddress, "IX_Customer_EmailAddress");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("datetime('now')")
                .HasColumnType("DATETIME");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.AddressId });

            entity.ToTable("CustomerAddress");

            entity.HasIndex(e => e.Rowguid, "IX_CustomerAddress_rowguid").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("datetime('now')")
                .HasColumnType("DATETIME");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");

            entity.HasOne(d => d.Address).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.ToTable("ErrorLog");

            entity.Property(e => e.ErrorLogId).HasColumnName("ErrorLogID");
            entity.Property(e => e.ErrorTime)
                .HasDefaultValueSql("datetime('now')")
                .HasColumnType("DATETIME");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.Name, "IX_Product_Name").IsUnique();

            entity.HasIndex(e => e.ProductNumber, "IX_Product_ProductNumber").IsUnique();

            entity.HasIndex(e => e.Rowguid, "IX_Product_rowguid").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.DiscontinuedDate).HasColumnType("DATETIME");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("datetime('now')")
                .HasColumnType("DATETIME");
            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.SellEndDate).HasColumnType("DATETIME");
            entity.Property(e => e.SellStartDate).HasColumnType("DATETIME");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products).HasForeignKey(d => d.ProductCategoryId);

            entity.HasOne(d => d.ProductModel).WithMany(p => p.Products).HasForeignKey(d => d.ProductModelId);
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("ProductCategory");

            entity.HasIndex(e => e.Name, "IX_ProductCategory_Name").IsUnique();

            entity.HasIndex(e => e.Rowguid, "IX_ProductCategory_rowguid").IsUnique();

            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("datetime('now')")
                .HasColumnType("DATETIME");
            entity.Property(e => e.ParentProductCategoryId).HasColumnName("ParentProductCategoryID");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");

            entity.HasOne(d => d.ParentProductCategory).WithMany(p => p.InverseParentProductCategory).HasForeignKey(d => d.ParentProductCategoryId);
        });

        modelBuilder.Entity<ProductDescription>(entity =>
        {
            entity.ToTable("ProductDescription");

            entity.HasIndex(e => e.Rowguid, "IX_ProductDescription_rowguid").IsUnique();

            entity.Property(e => e.ProductDescriptionId).HasColumnName("ProductDescriptionID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("datetime('now')")
                .HasColumnType("DATETIME");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
        });

        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.ToTable("ProductModel");

            entity.HasIndex(e => e.Name, "IX_ProductModel_Name").IsUnique();

            entity.HasIndex(e => e.Rowguid, "IX_ProductModel_rowguid").IsUnique();

            entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("datetime('now')")
                .HasColumnType("DATETIME");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
        });

        modelBuilder.Entity<ProductModelProductDescription>(entity =>
        {
            entity.HasKey(e => new { e.ProductModelId, e.ProductDescriptionId, e.Culture });

            entity.ToTable("ProductModelProductDescription");

            entity.HasIndex(e => e.Rowguid, "IX_ProductModelProductDescription_rowguid").IsUnique();

            entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            entity.Property(e => e.ProductDescriptionId).HasColumnName("ProductDescriptionID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("datetime('now')")
                .HasColumnType("DATETIME");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");

            entity.HasOne(d => d.ProductDescription).WithMany(p => p.ProductModelProductDescriptions)
                .HasForeignKey(d => d.ProductDescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ProductModel).WithMany(p => p.ProductModelProductDescriptions)
                .HasForeignKey(d => d.ProductModelId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SalesOrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.SalesOrderId, e.SalesOrderDetailId });

            entity.ToTable("SalesOrderDetail");

            entity.HasIndex(e => e.Rowguid, "IX_SalesOrderDetail_rowguid").IsUnique();

            entity.HasIndex(e => e.ProductId, "IX_SalesOrderDetail_ProductID");

            entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");
            entity.Property(e => e.SalesOrderDetailId)
                .HasColumnType("INTEGER IDENTITY (1, 1)")
                .HasColumnName("SalesOrderDetailID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("datetime('now')")
                .HasColumnType("DATETIME");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.UnitPriceDiscount).HasDefaultValueSql("0.0");

            entity.HasOne(d => d.Product).WithMany(p => p.SalesOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SalesOrder).WithMany(p => p.SalesOrderDetails)
                .HasForeignKey(d => d.SalesOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SalesOrderHeader>(entity =>
        {
            entity.HasKey(e => e.SalesOrderId);

            entity.ToTable("SalesOrderHeader");

            entity.HasIndex(e => e.Rowguid, "IX_SalesOrderHeader_rowguid").IsUnique();

            entity.HasIndex(e => e.SalesOrderNumber, "IX_SalesOrderHeader_SalesOrderNumber").IsUnique();

            entity.HasIndex(e => e.CustomerId, "IX_SalesOrderHeader_CustomerID");

            entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");
            entity.Property(e => e.BillToAddressId)
                .HasColumnType("INT")
                .HasColumnName("BillToAddressID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DueDate).HasColumnType("DATETIME");
            entity.Property(e => e.Freight).HasDefaultValueSql("0.00");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("datetime('now')")
                .HasColumnType("DATETIME");
            entity.Property(e => e.OnlineOrderFlag).HasDefaultValueSql("1");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("datetime('now')")
                .HasColumnType("DATETIME");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.ShipDate).HasColumnType("DATETIME");
            entity.Property(e => e.ShipToAddressId)
                .HasColumnType("INT")
                .HasColumnName("ShipToAddressID");
            entity.Property(e => e.Status).HasDefaultValueSql("1");
            entity.Property(e => e.SubTotal).HasDefaultValueSql("0.00");
            entity.Property(e => e.TaxAmt).HasDefaultValueSql("0.00");

            entity.HasOne(d => d.BillToAddress).WithMany(p => p.SalesOrderHeaderBillToAddresses).HasForeignKey(d => d.BillToAddressId);

            entity.HasOne(d => d.Customer).WithMany(p => p.SalesOrderHeaders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ShipToAddress).WithMany(p => p.SalesOrderHeaderShipToAddresses).HasForeignKey(d => d.ShipToAddressId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
