CREATE DATABASE [OpenERP] COLLATE SQL_Latin1_General_CP1_CI_AS
GO

USE [OpenERP]
GO

CREATE SCHEMA [Erp] AUTHORIZATION [dbo]
GO

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'Company') 
BEGIN

CREATE TABLE [Erp].[Company] (
    CompanyID NVARCHAR(8) NOT NULL DEFAULT (''),
    [Name] NVARCHAR(200) NOT NULL DEFAULT (''),
    [Active] BIT NOT NULL DEFAULT (0),
    CONSTRAINT PK_Company PRIMARY KEY (CompanyID)
);

END


-- custom user table on hold - will see what EF Identity generates and adapt from there

/* IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'User') 
BEGIN

    CREATE TABLE [Erp].[User] (
        UserID UNIQUEIDENTIFIER NOT NULL, --system generated, used for internal reference (ASP.NET Core 6 Identity implementation so may change)
        LoginID NVARCHAR(100) NOT NULL DEFAULT(''), --user assigned, used to login to environment
        UserName NVARCHAR(300) NOT NULL DEFAULT(''),
        AuthKey NVARCHAR(4000) NOT NULL DEFAULT(''), -- password in encrypted form
        UserDisabled BIT NOT NULL DEFAULT(0),
        CompanyList NVARCHAR(1000) NOT NULL DEFAULT(''),
        SSODomain NVARCHAR(50) NOT NULL DEFAULT(''),
        SSOUser NVARCHAR(300) NOT NULL DEFAULT(''),
        CONSTRAINT PK_User PRIMARY KEY (UserID)
    )

END */

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'UOMCode') 
BEGIN

    CREATE TABLE [Erp].[UOMCode] (
        CompanyID NVARCHAR(8) NOT NULL DEFAULT (''),
        UOMCode NVARCHAR(15) NOT NULL DEFAULT (''),
        UOMDescription NVARCHAR(100) NOT NULL DEFAULT(''),  
        Active BIT NOT NULL DEFAULT (0),
        CONSTRAINT PK_UOMCode PRIMARY KEY (CompanyID, UOMCode),
        CONSTRAINT FK_UOM_Company FOREIGN KEY (CompanyID) REFERENCES Erp.Company(CompanyID)
    )

END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'Part') 
BEGIN

    CREATE TABLE [Erp].[Part] (
        CompanyID NVARCHAR(8) NOT NULL DEFAULT(''),
        PartNum NVARCHAR(120) NOT NULL DEFAULT(''),
        PartDescription NVARCHAR(1000) NOT NULL DEFAULT(''),
        SerialTracked BIT NOT NULL DEFAULT(0),
        DefaultUOMCode NVARCHAR(15) NOT NULL DEFAULT(''),
        CONSTRAINT PK_Part PRIMARY KEY (CompanyID, PartNum),
        CONSTRAINT FK_Part_Company FOREIGN KEY (CompanyID) REFERENCES Erp.Company(CompanyID)
    )

END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'PartRev') 
BEGIN

CREATE TABLE [Erp].[PartRev] (
    CompanyID NVARCHAR(8) NOT NULL DEFAULT (''),
    [PartNum] NVARCHAR(120) NOT NULL DEFAULT (''),
    [PartRevNum] NVARCHAR(20) NOT NULL DEFAULT (''),
    [PartRevDesc] NVARCHAR(200) NOT NULL DEFAULT(''),
    [Approved] BIT NOT NULL DEFAULT(0),
    [ApprovedDate] DATETIME NULL,
    [ApprovedUser] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_PartRev PRIMARY KEY (CompanyID, PartNum, PartRevNum),
    CONSTRAINT FK_PartRev_Company FOREIGN KEY (CompanyID) REFERENCES [Erp].[Company](CompanyID),
    CONSTRAINT FK_PR_Part FOREIGN KEY (CompanyID, PartNum) REFERENCES [Erp].[Part](CompanyID, PartNum)
    --CONSTRAINT FK_User FOREIGN KEY (ApprovedUser) REFERENCES [Erp].[User](UserID)
);

END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'Customer') 
BEGIN

    CREATE TABLE [Erp].[Customer] (
            CompanyID NVARCHAR(8) NOT NULL DEFAULT (''),
            CustomerID INT NOT NULL DEFAULT (0),
            [Name] NVARCHAR(200) NOT NULL DEFAULT (''),
            [Status] NVARCHAR(5) NOT NULL DEFAULT(''),
            [EmailAddress] NVARCHAR(200) NOT NULL DEFAULT(''),
            CONSTRAINT PK_Customer PRIMARY KEY (CompanyID, CustomerID),
            CONSTRAINT FK_CustomerCompany FOREIGN KEY (CompanyID) REFERENCES [Erp].Company(CompanyID)
        );

END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'Address') 
BEGIN

    CREATE TABLE [Erp].[Address] (
            CompanyID NVARCHAR(8) NOT NULL DEFAULT (''),
            ReferenceTable NVARCHAR(1) NOT NULL DEFAULT(''), -- S for Erp.Supplier, C for Erp.Customer
            ForeignKeyID INT NOT NULL DEFAULT(0),
            AddressID INT NOT NULL DEFAULT(0),
            Address1 NVARCHAR(200) NOT NULL DEFAULT (''),
            Address2 NVARCHAR(200) NOT NULL DEFAULT (''),
            Address3 NVARCHAR(200) NOT NULL DEFAULT (''),
            City NVARCHAR(200) NOT NULL DEFAULT (''),
            CountryNum INT NOT NULL DEFAULT (0),
            PostCode NVARCHAR(20) NOT NULL DEFAULT(''),
            CONSTRAINT PK_Address PRIMARY KEY (CompanyID, ReferenceTable, ForeignKeyID, AddressID),
            CONSTRAINT FK_AddressCompany FOREIGN KEY (CompanyID) REFERENCES Erp.Company(CompanyID)
    );


END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'SalesOrderHed') 
BEGIN


CREATE TABLE  [Erp].[SalesOrderHed] (
    CompanyID NVARCHAR(8) NOT NULL DEFAULT (''),
    SalesOrderNum INT NOT NULL DEFAULT (0),
    CustomerID INT NOT NULL DEFAULT(0),
    BillingAddressID INT NOT NULL DEFAULT(0),
    ShippingAddressID INT NOT NULL DEFAULT(0),
    CustomerRequiredDate DATETIME NULL,
    SuggestedShipDate DATETIME NULL,
    OpenOrder BIT NOT NULL DEFAULT (0),
    CancelledOrder BIT NOT NULL DEFAULT (0),
    ClosedDate DATETIME NULL,
    CustomerPONum NVARCHAR(50) NOT NULL DEFAULT(''),
    CONSTRAINT PK_SalesOrderHed PRIMARY KEY (CompanyID, SalesOrderNum),
    CONSTRAINT FK_SOH_Company FOREIGN KEY (CompanyID) REFERENCES Erp.Company(CompanyID),
    CONSTRAINT FK_SOH_Customer FOREIGN KEY (CompanyID, CustomerID) REFERENCES Erp.Customer(CompanyID, CustomerID)
);


END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'SalesOrderDtl') 
BEGIN

    CREATE TABLE  [Erp].[SalesOrderDtl] (
        CompanyID NVARCHAR(8) NOT NULL DEFAULT (''),
        SalesOrderNum INT NOT NULL DEFAULT (0),
        SalesOrderLineNum INT NOT NULL DEFAULT(0),
        PartNum NVARCHAR(120) NOT NULL DEFAULT (''),
        LineDesc NVARCHAR(1000) NOT NULL DEFAULT (''),
        LineQty DECIMAL(9,2) NOT NULL DEFAULT (0),
        SalesUOM NVARCHAR(15) NOT NULL DEFAULT (''),
        SOLineComments NVARCHAR(1000) NOT NULL DEFAULT(''),
        CONSTRAINT PK_SalesOrderDtl PRIMARY KEY (CompanyID, SalesOrderNum, SalesOrderLineNum),
        CONSTRAINT FK_SOD_SOHed FOREIGN KEY (CompanyID, SalesOrderNum) REFERENCES Erp.SalesOrderHed(CompanyID, SalesOrderNum),
        CONSTRAINT FK_SOD_Part FOREIGN KEY (CompanyID, PartNum) REFERENCES Erp.Part(CompanyID, PartNum)
    );


END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'SalesOrderRel') 
BEGIN

    CREATE TABLE  [Erp].[SalesOrderRel] (
        CompanyID NVARCHAR(8) NOT NULL DEFAULT (''),
        SalesOrderNum INT NOT NULL DEFAULT (0),
        SalesOrderLineNum INT NOT NULL DEFAULT(0),
        SalesOrderRelNum INT NOT NULL DEFAULT (0),
        ReleaseQty DECIMAL(9,2) NOT NULL DEFAULT (0),
        RequiredByDate DATETIME NULL,
        CONSTRAINT PK_SalesOrderRel PRIMARY KEY (CompanyID, SalesOrderNum, SalesOrderLineNum, SalesOrderRelNum),
        CONSTRAINT FK_SOR_Company FOREIGN KEY (CompanyID) REFERENCES Erp.Company(CompanyID),
        CONSTRAINT FK_SOR_SODtl FOREIGN KEY (CompanyID, SalesOrderNum, SalesOrderLineNum) REFERENCES Erp.SalesOrderDtl(CompanyID, SalesOrderNum, SalesOrderLineNum)
    );

END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'Supplier') 
BEGIN

    CREATE TABLE [Erp].[Supplier] (
        CompanyID NVARCHAR(8) NOT NULL DEFAULT(''),
        SupplierID INT NOT NULL DEFAULT(0),
        SupplierName NVARCHAR(200) NOT NULL DEFAULT(''),
        Active BIT NOT NULL DEFAULT(0),
        AddressID INT NOT NULL DEFAULT(0),
        CONSTRAINT PK_Supplier PRIMARY KEY (CompanyID, SupplierID),
        CONSTRAINT FK_Supplier_Company FOREIGN KEY (CompanyID) REFERENCES Erp.Company(CompanyID)
    )

END


IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'PurchaseOrderHed') 
BEGIN

    CREATE TABLE [Erp].[PurchaseOrderHed] (
        CompanyID NVARCHAR(8) NOT NULL DEFAULT(''),
        PurchaseOrderNum INT NOT NULL DEFAULT(0),
        SupplierID INT NOT NULL DEFAULT(0),
        OrderDate DATETIME NULL,
        CurrencyCode NVARCHAR(10) NOT NULL DEFAULT(''),
        CreatedByUserID UNIQUEIDENTIFIER NOT NULL,
        CreatedDate DATETIME NULL,
        ApprovalStatus NVARCHAR(1) NOT NULL DEFAULT(''),
        ApprovedDate DATETIME NULL,
        LastChangeDate DATETIME NULL,
        LastChangeUser UNIQUEIDENTIFIER NOT NULL,
        CONSTRAINT PK_PurchaseOrderHed PRIMARY KEY (CompanyID, PurchaseOrderNum),
        CONSTRAINT FK_POH_Company FOREIGN KEY (CompanyID) REFERENCES Erp.Company(CompanyID),
        CONSTRAINT FK_POH_Supplier FOREIGN KEY (CompanyID, SupplierID) REFERENCES Erp.Supplier(CompanyID, SupplierID)
    )

END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'PurchaseOrderDtl') 
BEGIN

    CREATE TABLE [Erp].[PurchaseOrderDtl] (
        CompanyID NVARCHAR(8) NOT NULL DEFAULT(''),
        PurchaseOrderNum INT NOT NULL DEFAULT(0),
        PurchaseOrderLineNum INT NOT NULL DEFAULT(0),
        PartNum NVARCHAR(120) NOT NULL DEFAULT(''),
        LineDesc NVARCHAR(1000) NOT NULL DEFAULT(''),
        OurOrderQty DECIMAL(9,2) NOT NULL DEFAULT(0),
        SupplierOrderQty DECIMAL(9,2) NOT NULL DEFAULT(0),
        OurUOMCode NVARCHAR(15) NOT NULL DEFAULT(''),
        SupplierUOMCode NVARCHAR(15) NOT NULL DEFAULT(''),
        RequiredDate DATETIME NULL, -- is used to default the releases, one of which is created at the same time as the order line is saved
        LastChangeDate DATETIME NULL,
        LastChangeUser UNIQUEIDENTIFIER NOT NULL,
        CONSTRAINT PK_PurchaseOrderDtl PRIMARY KEY (CompanyID, PurchaseOrderNum, PurchaseOrderLineNum),
        CONSTRAINT FK_POD_Company FOREIGN KEY (CompanyID) REFERENCES Erp.Company(CompanyID)
    )

END


IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'PurchaseOrderRel') 
BEGIN

    CREATE TABLE [Erp].[PurchaseOrderRel] (
        CompanyID NVARCHAR(8) NOT NULL DEFAULT(''),
        PurchaseOrderNum INT NOT NULL DEFAULT(0),
        PurchaseOrderLineNum INT NOT NULL DEFAULT(0),
        PurchaseOrderRelNum INT NOT NULL DEFAULT(0),
        RequiredDate DATETIME NULL,
        DueDate DATETIME NULL,
        OurOrderQty DECIMAL(9,2) NOT NULL DEFAULT(0),
        SupplierOrderQty DECIMAL(9,2) NOT NULL DEFAULT(0),
        OurUOMCode NVARCHAR(15) NOT NULL DEFAULT(''),
        SupplierUOMCode NVARCHAR(15) NOT NULL DEFAULT(''),
        LastChangeDate DATETIME NULL,
        LastChangeUser UNIQUEIDENTIFIER NOT NULL,
        CONSTRAINT PK_PurchaseOrderRel PRIMARY KEY (CompanyID, PurchaseOrderNum, PurchaseOrderLineNum, PurchaseOrderRelNum),
        CONSTRAINT FK_POR_Company FOREIGN KEY (CompanyID) REFERENCES Erp.Company(CompanyID)
    )
END

