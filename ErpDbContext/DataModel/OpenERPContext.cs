using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OpenERP.ErpDbContext.DataModel
{
    public partial class OpenERPContext : IdentityDbContext
    {
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
        public virtual DbSet<PartRev> PartRevs { get; set; } = null!;
        public virtual DbSet<PurchaseOrderDtl> PurchaseOrderDtls { get; set; } = null!;
        public virtual DbSet<PurchaseOrderHed> PurchaseOrderHeds { get; set; } = null!;
        public virtual DbSet<PurchaseOrderRel> PurchaseOrderRels { get; set; } = null!;
        public virtual DbSet<SalesOrderDtl> SalesOrderDtls { get; set; } = null!;
        public virtual DbSet<SalesOrderHed> SalesOrderHeds { get; set; } = null!;
        public virtual DbSet<SalesOrderRel> SalesOrderRels { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<Uom> Uoms { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=OpenERPContextDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_AddressCompany");
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

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_CustomerCompany");
            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.PartNum });

                entity.ToTable("Part", "Erp");

                entity.HasIndex(e => new { e.CompanyId, e.DefaultUom }, "IX_Part_CompanyID_DefaultUOM");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PartNum)
                    .HasMaxLength(120)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DefaultUom)
                    .HasMaxLength(15)
                    .HasColumnName("DefaultUOM")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PartDescription)
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Parts)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Part_Company");

                entity.HasOne(d => d.Uom)
                    .WithMany(p => p.Parts)
                    .HasForeignKey(d => new { d.CompanyId, d.DefaultUom })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Part_UOM");
            });

            modelBuilder.Entity<PartRev>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.PartNum, e.PartRevNum });

                entity.ToTable("PartRev", "Erp");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PartNum)
                    .HasMaxLength(120)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PartRevNum)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovedUser)
                    .HasMaxLength(450)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PartRevDesc)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.PartRevs)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_PartRev_Company");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.PartRevs)
                    .HasForeignKey(d => new { d.CompanyId, d.PartNum })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PR_Part");
            });

            modelBuilder.Entity<PurchaseOrderDtl>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.PurchaseOrderNum, e.PurchaseOrderLineNum });

                entity.ToTable("PurchaseOrderDtl", "Erp");

                entity.HasIndex(e => new { e.CompanyId, e.OurUom }, "IX_PurchaseOrderDtl_CompanyID_OurUOM");

                entity.HasIndex(e => new { e.CompanyId, e.SupplierUom }, "IX_PurchaseOrderDtl_CompanyID_SupplierUOM");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CostCentre)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CostElement)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InternalOrder)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastChangeDate).HasColumnType("datetime");

                entity.Property(e => e.LastChangeUser)
                    .HasMaxLength(450)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LineDesc)
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OurOrderQty).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.OurUom)
                    .HasMaxLength(15)
                    .HasColumnName("OurUOM")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PartNum)
                    .HasMaxLength(120)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.SupplierOrderQty).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.SupplierUom)
                    .HasMaxLength(15)
                    .HasColumnName("SupplierUOM")
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.PurchaseOrderDtls)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_POD_Company");

                entity.HasOne(d => d.Uom)
                    .WithMany(p => p.PurchaseOrderDtlUoms)
                    .HasForeignKey(d => new { d.CompanyId, d.OurUom })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POD_OurUOM");

                entity.HasOne(d => d.UomNavigation)
                    .WithMany(p => p.PurchaseOrderDtlUomNavigations)
                    .HasForeignKey(d => new { d.CompanyId, d.SupplierUom })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POD_SupplierUOM");
            });

            modelBuilder.Entity<PurchaseOrderHed>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.PurchaseOrderNum });

                entity.ToTable("PurchaseOrderHed", "Erp");

                entity.HasIndex(e => new { e.CompanyId, e.SupplierId }, "IX_PurchaseOrderHed_CompanyID_SupplierID");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ApprovalStatus)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUserId)
                    .HasMaxLength(450)
                    .HasColumnName("CreatedByUserID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencyCode)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastChangeDate).HasColumnType("datetime");

                entity.Property(e => e.LastChangeUser)
                    .HasMaxLength(450)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.PurchaseOrderHeds)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_POH_Company");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PurchaseOrderHeds)
                    .HasForeignKey(d => new { d.CompanyId, d.SupplierId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POH_Supplier");
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

                entity.Property(e => e.LastChangeUser)
                    .HasMaxLength(450)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OurOrderQty).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.SupplierOrderQty).HasColumnType("decimal(9, 2)");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.PurchaseOrderRels)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_POR_Company");
            });

            modelBuilder.Entity<SalesOrderDtl>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.SalesOrderNum, e.SalesOrderLineNum });

                entity.ToTable("SalesOrderDtl", "Erp");

                entity.HasIndex(e => new { e.CompanyId, e.PartNum }, "IX_SalesOrderDtl_CompanyID_PartNum");

                entity.HasIndex(e => new { e.CompanyId, e.SalesUom }, "IX_SalesOrderDtl_CompanyID_SalesUOM");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CostCentre)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CostElement)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InternalOrder)
                    .HasMaxLength(50)
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

                entity.Property(e => e.SolineComments)
                    .HasMaxLength(1000)
                    .HasColumnName("SOLineComments")
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.SalesOrderDtls)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_SOD_Company");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.SalesOrderDtls)
                    .HasForeignKey(d => new { d.CompanyId, d.PartNum })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SOD_Part");

                entity.HasOne(d => d.SalesOrderHed)
                    .WithMany(p => p.SalesOrderDtls)
                    .HasForeignKey(d => new { d.CompanyId, d.SalesOrderNum })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SOD_SOHed");

                entity.HasOne(d => d.Uom)
                    .WithMany(p => p.SalesOrderDtls)
                    .HasForeignKey(d => new { d.CompanyId, d.SalesUom })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SOD_UOM");
            });

            modelBuilder.Entity<SalesOrderHed>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.SalesOrderNum });

                entity.ToTable("SalesOrderHed", "Erp");

                entity.HasIndex(e => new { e.CompanyId, e.CustomerId }, "IX_SalesOrderHed_CompanyID_CustomerID");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BillingAddressId).HasColumnName("BillingAddressID");

                entity.Property(e => e.ClosedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedByUser)
                    .HasMaxLength(450)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerPonum)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerPONum")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomerRequiredDate).HasColumnType("datetime");

                entity.Property(e => e.ShippingAddressId).HasColumnName("ShippingAddressID");

                entity.Property(e => e.SuggestedShipDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.SalesOrderHeds)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_SOH_Company");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.SalesOrderHeds)
                    .HasForeignKey(d => new { d.CompanyId, d.CustomerId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SOH_Customer");
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

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.SalesOrderRels)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_SOR_Company");

                entity.HasOne(d => d.SalesOrderDtl)
                    .WithMany(p => p.SalesOrderRels)
                    .HasForeignKey(d => new { d.CompanyId, d.SalesOrderNum, d.SalesOrderLineNum })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SOR_SODtl");
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

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Supplier_Company");
            });

            modelBuilder.Entity<Uom>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.Uom1 });

                entity.ToTable("UOM", "Erp");

                entity.Property(e => e.CompanyId)
                    .HasMaxLength(8)
                    .HasColumnName("CompanyID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Uom1)
                    .HasMaxLength(15)
                    .HasColumnName("UOM")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Uomdescription)
                    .HasMaxLength(100)
                    .HasColumnName("UOMDescription")
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Uoms)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_UOM_Company");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
