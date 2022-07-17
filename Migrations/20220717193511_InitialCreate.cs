﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenERP.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Erp");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyList = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "Erp",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValueSql: "('')"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "('')"),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "Erp",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValueSql: "('')"),
                    ReferenceTable = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false, defaultValueSql: "('')"),
                    ForeignKeyID = table.Column<int>(type: "int", nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "('')"),
                    Address2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "('')"),
                    Address3 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "('')"),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "('')"),
                    CountryNum = table.Column<int>(type: "int", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => new { x.CompanyID, x.ReferenceTable, x.ForeignKeyID, x.AddressID });
                    table.ForeignKey(
                        name: "FK_AddressCompany",
                        column: x => x.CompanyID,
                        principalSchema: "Erp",
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Erp",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValueSql: "('')"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "('')"),
                    Status = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, defaultValueSql: "('')"),
                    EmailAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => new { x.CompanyID, x.CustomerID });
                    table.ForeignKey(
                        name: "FK_CustomerCompany",
                        column: x => x.CompanyID,
                        principalSchema: "Erp",
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderRel",
                schema: "Erp",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValueSql: "('')"),
                    PurchaseOrderNum = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderLineNum = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderRelNum = table.Column<int>(type: "int", nullable: false),
                    RequiredDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    OurOrderQty = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    SupplierOrderQty = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    LastChangeDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastChangeUser = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderRel", x => new { x.CompanyID, x.PurchaseOrderNum, x.PurchaseOrderLineNum, x.PurchaseOrderRelNum });
                    table.ForeignKey(
                        name: "FK_POR_Company",
                        column: x => x.CompanyID,
                        principalSchema: "Erp",
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                schema: "Erp",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValueSql: "('')"),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "('')"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => new { x.CompanyID, x.SupplierID });
                    table.ForeignKey(
                        name: "FK_Supplier_Company",
                        column: x => x.CompanyID,
                        principalSchema: "Erp",
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UOM",
                schema: "Erp",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValueSql: "('')"),
                    UOM = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, defaultValueSql: "('')"),
                    UOMDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UOM", x => new { x.CompanyID, x.UOM });
                    table.ForeignKey(
                        name: "FK_UOM_Company",
                        column: x => x.CompanyID,
                        principalSchema: "Erp",
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderHed",
                schema: "Erp",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValueSql: "('')"),
                    SalesOrderNum = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    BillingAddressID = table.Column<int>(type: "int", nullable: false),
                    ShippingAddressID = table.Column<int>(type: "int", nullable: false),
                    CustomerRequiredDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SuggestedShipDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    OpenOrder = table.Column<bool>(type: "bit", nullable: false),
                    CancelledOrder = table.Column<bool>(type: "bit", nullable: false),
                    ClosedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CustomerPONum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    CreatedByUser = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false, defaultValueSql: "('')"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderHed", x => new { x.CompanyID, x.SalesOrderNum });
                    table.ForeignKey(
                        name: "FK_SOH_Company",
                        column: x => x.CompanyID,
                        principalSchema: "Erp",
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SOH_Customer",
                        columns: x => new { x.CompanyID, x.CustomerID },
                        principalSchema: "Erp",
                        principalTable: "Customer",
                        principalColumns: new[] { "CompanyID", "CustomerID" });
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderHed",
                schema: "Erp",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValueSql: "('')"),
                    PurchaseOrderNum = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValueSql: "('')"),
                    CreatedByUserID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false, defaultValueSql: "('')"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false, defaultValueSql: "('')"),
                    ApprovedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastChangeDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastChangeUser = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderHed", x => new { x.CompanyID, x.PurchaseOrderNum });
                    table.ForeignKey(
                        name: "FK_POH_Company",
                        column: x => x.CompanyID,
                        principalSchema: "Erp",
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POH_Supplier",
                        columns: x => new { x.CompanyID, x.SupplierID },
                        principalSchema: "Erp",
                        principalTable: "Supplier",
                        principalColumns: new[] { "CompanyID", "SupplierID" });
                });

            migrationBuilder.CreateTable(
                name: "Part",
                schema: "Erp",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValueSql: "('')"),
                    PartNum = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "('')"),
                    PartDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, defaultValueSql: "('')"),
                    SerialTracked = table.Column<bool>(type: "bit", nullable: false),
                    DefaultUOM = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Part", x => new { x.CompanyID, x.PartNum });
                    table.ForeignKey(
                        name: "FK_Part_Company",
                        column: x => x.CompanyID,
                        principalSchema: "Erp",
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Part_UOM",
                        columns: x => new { x.CompanyID, x.DefaultUOM },
                        principalSchema: "Erp",
                        principalTable: "UOM",
                        principalColumns: new[] { "CompanyID", "UOM" });
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderDtl",
                schema: "Erp",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValueSql: "('')"),
                    PurchaseOrderNum = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderLineNum = table.Column<int>(type: "int", nullable: false),
                    PartNum = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "('')"),
                    LineDesc = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, defaultValueSql: "('')"),
                    OurOrderQty = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    SupplierOrderQty = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    OurUOM = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, defaultValueSql: "('')"),
                    SupplierUOM = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, defaultValueSql: "('')"),
                    CostElement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    CostCentre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    InternalOrder = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    RequiredDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastChangeDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastChangeUser = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderDtl", x => new { x.CompanyID, x.PurchaseOrderNum, x.PurchaseOrderLineNum });
                    table.ForeignKey(
                        name: "FK_POD_Company",
                        column: x => x.CompanyID,
                        principalSchema: "Erp",
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POD_OurUOM",
                        columns: x => new { x.CompanyID, x.OurUOM },
                        principalSchema: "Erp",
                        principalTable: "UOM",
                        principalColumns: new[] { "CompanyID", "UOM" });
                    table.ForeignKey(
                        name: "FK_POD_SupplierUOM",
                        columns: x => new { x.CompanyID, x.SupplierUOM },
                        principalSchema: "Erp",
                        principalTable: "UOM",
                        principalColumns: new[] { "CompanyID", "UOM" });
                });

            migrationBuilder.CreateTable(
                name: "PartRev",
                schema: "Erp",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValueSql: "('')"),
                    PartNum = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "('')"),
                    PartRevNum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValueSql: "('')"),
                    PartRevDesc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValueSql: "('')"),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ApprovedUser = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartRev", x => new { x.CompanyID, x.PartNum, x.PartRevNum });
                    table.ForeignKey(
                        name: "FK_PartRev_Company",
                        column: x => x.CompanyID,
                        principalSchema: "Erp",
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PR_Part",
                        columns: x => new { x.CompanyID, x.PartNum },
                        principalSchema: "Erp",
                        principalTable: "Part",
                        principalColumns: new[] { "CompanyID", "PartNum" });
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderDtl",
                schema: "Erp",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValueSql: "('')"),
                    SalesOrderNum = table.Column<int>(type: "int", nullable: false),
                    SalesOrderLineNum = table.Column<int>(type: "int", nullable: false),
                    PartNum = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "('')"),
                    LineDesc = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, defaultValueSql: "('')"),
                    LineQty = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    SalesUOM = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, defaultValueSql: "('')"),
                    SOLineComments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, defaultValueSql: "('')"),
                    CostElement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    CostCentre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    InternalOrder = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderDtl", x => new { x.CompanyID, x.SalesOrderNum, x.SalesOrderLineNum });
                    table.ForeignKey(
                        name: "FK_SOD_Company",
                        column: x => x.CompanyID,
                        principalSchema: "Erp",
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SOD_Part",
                        columns: x => new { x.CompanyID, x.PartNum },
                        principalSchema: "Erp",
                        principalTable: "Part",
                        principalColumns: new[] { "CompanyID", "PartNum" });
                    table.ForeignKey(
                        name: "FK_SOD_SOHed",
                        columns: x => new { x.CompanyID, x.SalesOrderNum },
                        principalSchema: "Erp",
                        principalTable: "SalesOrderHed",
                        principalColumns: new[] { "CompanyID", "SalesOrderNum" });
                    table.ForeignKey(
                        name: "FK_SOD_UOM",
                        columns: x => new { x.CompanyID, x.SalesUOM },
                        principalSchema: "Erp",
                        principalTable: "UOM",
                        principalColumns: new[] { "CompanyID", "UOM" });
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderRel",
                schema: "Erp",
                columns: table => new
                {
                    CompanyID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValueSql: "('')"),
                    SalesOrderNum = table.Column<int>(type: "int", nullable: false),
                    SalesOrderLineNum = table.Column<int>(type: "int", nullable: false),
                    SalesOrderRelNum = table.Column<int>(type: "int", nullable: false),
                    ReleaseQty = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    RequiredByDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderRel", x => new { x.CompanyID, x.SalesOrderNum, x.SalesOrderLineNum, x.SalesOrderRelNum });
                    table.ForeignKey(
                        name: "FK_SOR_Company",
                        column: x => x.CompanyID,
                        principalSchema: "Erp",
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SOR_SODtl",
                        columns: x => new { x.CompanyID, x.SalesOrderNum, x.SalesOrderLineNum },
                        principalSchema: "Erp",
                        principalTable: "SalesOrderDtl",
                        principalColumns: new[] { "CompanyID", "SalesOrderNum", "SalesOrderLineNum" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Part_CompanyID_DefaultUOM",
                schema: "Erp",
                table: "Part",
                columns: new[] { "CompanyID", "DefaultUOM" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDtl_CompanyID_OurUOM",
                schema: "Erp",
                table: "PurchaseOrderDtl",
                columns: new[] { "CompanyID", "OurUOM" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDtl_CompanyID_SupplierUOM",
                schema: "Erp",
                table: "PurchaseOrderDtl",
                columns: new[] { "CompanyID", "SupplierUOM" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderHed_CompanyID_SupplierID",
                schema: "Erp",
                table: "PurchaseOrderHed",
                columns: new[] { "CompanyID", "SupplierID" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDtl_CompanyID_PartNum",
                schema: "Erp",
                table: "SalesOrderDtl",
                columns: new[] { "CompanyID", "PartNum" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDtl_CompanyID_SalesUOM",
                schema: "Erp",
                table: "SalesOrderDtl",
                columns: new[] { "CompanyID", "SalesUOM" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderHed_CompanyID_CustomerID",
                schema: "Erp",
                table: "SalesOrderHed",
                columns: new[] { "CompanyID", "CustomerID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "Erp");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PartRev",
                schema: "Erp");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDtl",
                schema: "Erp");

            migrationBuilder.DropTable(
                name: "PurchaseOrderHed",
                schema: "Erp");

            migrationBuilder.DropTable(
                name: "PurchaseOrderRel",
                schema: "Erp");

            migrationBuilder.DropTable(
                name: "SalesOrderRel",
                schema: "Erp");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Supplier",
                schema: "Erp");

            migrationBuilder.DropTable(
                name: "SalesOrderDtl",
                schema: "Erp");

            migrationBuilder.DropTable(
                name: "Part",
                schema: "Erp");

            migrationBuilder.DropTable(
                name: "SalesOrderHed",
                schema: "Erp");

            migrationBuilder.DropTable(
                name: "UOM",
                schema: "Erp");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Erp");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "Erp");
        }
    }
}