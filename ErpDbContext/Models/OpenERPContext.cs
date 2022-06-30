using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace ErpDbContext.Models
{
    public partial class OpenERPContext : DbContext
    {
        private readonly IConfiguration _config;

        public OpenERPContext()
        {
        }

        public OpenERPContext(DbContextOptions<OpenERPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Part> Parts { get; set; } = null!;
        public virtual DbSet<PurchaseOrderDtl> PurchaseOrderDtls { get; set; } = null!;
        public virtual DbSet<PurchaseOrderHed> PurchaseOrderHeds { get; set; } = null!;
        public virtual DbSet<PurchaseOrderRel> PurchaseOrderRels { get; set; } = null!;
        public virtual DbSet<SalesOrderDtl> SalesOrderDtls { get; set; } = null!;
        public virtual DbSet<SalesOrderHed> SalesOrderHeds { get; set; } = null!;
        public virtual DbSet<SalesOrderRel> SalesOrderRels { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<Uomcode> Uomcodes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        public OpenERPContext(IConfiguration config)
        {
            this._config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config["ConnectionStrings:OpenERPContextDb"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.ReferenceTable, e.ForeignKeyId, e.AddressId });

                entity.ToTable("Address", "Erp");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ReferenceTable)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ForeignKeyId).HasColumnName("ForeignKeyID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Address1)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Address2)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Address3)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.City)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PostCode)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company", "Erp");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.CustomerId });

                entity.ToTable("Customer", "Erp");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status)
                    .HasMaxLength(5)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Part", "Erp");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DefaultUomcode)
                    .HasMaxLength(15)
                    .HasColumnName("DefaultUOMCode")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PartDescription)
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PartNum)
                    .HasMaxLength(120)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<PurchaseOrderDtl>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.PurchaseOrderNum, e.PurchaseOrderLineNum });

                entity.ToTable("PurchaseOrderDtl", "Erp");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastChangeDate).HasColumnType("datetime");

                entity.Property(e => e.LineDesc)
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OurOrderQty).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.OurUomcode)
                    .HasMaxLength(15)
                    .HasColumnName("OurUOMCode")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PartNum)
                    .HasMaxLength(120)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.SupplierOrderQty).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.SupplierUomcode)
                    .HasMaxLength(15)
                    .HasColumnName("SupplierUOMCode")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<PurchaseOrderHed>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.PurchaseOrderNum });

                entity.ToTable("PurchaseOrderHed", "Erp");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ApprovalStatus)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencyCode)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastChangeDate).HasColumnType("datetime");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            });

            modelBuilder.Entity<PurchaseOrderRel>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.PurchaseOrderNum, e.PurchaseOrderLineNum, e.PurchaseOrderRelNum });

                entity.ToTable("PurchaseOrderRel", "Erp");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.LastChangeDate).HasColumnType("datetime");

                entity.Property(e => e.OurOrderQty).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.OurUomcode)
                    .HasMaxLength(15)
                    .HasColumnName("OurUOMCode")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.SupplierOrderQty).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.SupplierUomcode)
                    .HasMaxLength(15)
                    .HasColumnName("SupplierUOMCode")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<SalesOrderDtl>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.SalesOrderNum, e.SalesOrderLineNum });

                entity.ToTable("SalesOrderDtl", "Erp");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LineDesc)
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LineQty).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.PartNum)
                    .HasMaxLength(120)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SalesUom)
                    .HasMaxLength(15)
                    .HasColumnName("SalesUOM")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<SalesOrderHed>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.SalesOrderNum });

                entity.ToTable("SalesOrderHed", "Erp");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BillingAddressId).HasColumnName("BillingAddressID");

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerPonum)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerPONum")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomerRequiredDate).HasColumnType("datetime");

                entity.Property(e => e.ShippingAddressId).HasColumnName("ShippingAddressID");

                entity.Property(e => e.SuggestedShipDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SalesOrderRel>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.SalesOrderNum, e.SalesOrderLineNum, e.SalesOrderRelNum });

                entity.ToTable("SalesOrderRel", "Erp");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ReleaseQty).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.RequiredByDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.SupplierId });

                entity.ToTable("Supplier", "Erp");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.SupplierName)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Uomcode>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.Uomcode1 });

                entity.ToTable("UOMCode", "Erp");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Uomcode1)
                    .HasMaxLength(15)
                    .HasColumnName("UOMCode")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Uomdescription)
                    .HasMaxLength(100)
                    .HasColumnName("UOMDescription")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "Erp");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.AuthKey)
                    .HasMaxLength(4000)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CompanyList)
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LoginId)
                    .HasMaxLength(100)
                    .HasColumnName("LoginID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ssodomain)
                    .HasMaxLength(50)
                    .HasColumnName("SSODomain")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ssouser)
                    .HasMaxLength(300)
                    .HasColumnName("SSOUser")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserName)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
