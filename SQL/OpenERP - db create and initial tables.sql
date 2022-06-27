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
    [Active] BIT NOT NULL DEFAULT (0)
);


ALTER TABLE Erp.Company ADD CONSTRAINT PK_Company PRIMARY KEY (CompanyID);

END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'Customer') 
BEGIN

    CREATE TABLE [Erp].[Customer] (
            CompanyID NVARCHAR(8) NOT NULL DEFAULT (''),
            CustomerID INT NOT NULL DEFAULT (0),
            [Name] NVARCHAR(200) NOT NULL DEFAULT (''),
            [Status] NVARCHAR(5) NOT NULL DEFAULT(''),
            [EmailAddress] NVARCHAR(200) NOT NULL DEFAULT('')
        );


    ALTER TABLE Erp.Customer ADD CONSTRAINT PK_Customer PRIMARY KEY (CompanyID, CustomerID);

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
            PostCode NVARCHAR(20) NOT NULL DEFAULT('')
    );

ALTER TABLE Erp.Address ADD CONSTRAINT PK_Address PRIMARY KEY (CompanyID, ReferenceTable, ForeignKeyID, AddressID);

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
    CustomerPONum NVARCHAR(50) NOT NULL DEFAULT('')
);

ALTER TABLE Erp.SalesOrderHed ADD CONSTRAINT PK_SalesOrderHed PRIMARY KEY (CompanyID, SalesOrderNum);

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

    );

    ALTER TABLE Erp.SalesOrderDtl ADD CONSTRAINT PK_SalesOrderDtl PRIMARY KEY (CompanyID, SalesOrderNum, SalesOrderLineNum);

END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'SalesOrderRel') 
BEGIN

    CREATE TABLE  [Erp].[SalesOrderRel] (
        CompanyID NVARCHAR(8) NOT NULL DEFAULT (''),
        SalesOrderNum INT NOT NULL DEFAULT (0),
        SalesOrderLineNum INT NOT NULL DEFAULT(0),
        SalesOrderRelNum INT NOT NULL DEFAULT (0),
        ReleaseQty DECIMAL(9,2) NOT NULL DEFAULT (0),
        RequiredByDate DATETIME NULL

    );

    ALTER TABLE Erp.SalesOrderRel ADD CONSTRAINT PK_SalesOrderRel PRIMARY KEY (CompanyID, SalesOrderNum, SalesOrderLineNum, SalesOrderRelNum);

END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'UOMCode') 
BEGIN

    CREATE TABLE [Erp].[UOMCode] (
        CompanyID NVARCHAR(8) NOT NULL DEFAULT (''),
        UOMCode NVARCHAR(15) NOT NULL DEFAULT (''),
        UOMDescription NVARCHAR(100) NOT NULL DEFAULT(''),  
        Active BIT NOT NULL DEFAULT (0),
    )

    ALTER TABLE Erp.UOMCode ADD CONSTRAINT PK_UOMCode PRIMARY KEY (CompanyID, UOMCode)

END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'Part') 
BEGIN

    CREATE TABLE [Erp].[Part] (
        CompanyID NVARCHAR(8) NOT NULL DEFAULT(''),
        PartNum NVARCHAR(120) NOT NULL DEFAULT(''),
        PartDescription NVARCHAR(1000) NOT NULL DEFAULT(''),
        SerialTracked BIT NOT NULL DEFAULT(0),
        DefaultUOMCode NVARCHAR(15) NOT NULL DEFAULT('')
    )

END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'User') 
BEGIN

    CREATE TABLE [Erp].[User] (
        UserID INT NOT NULL DEFAULT(0), --system generated, used for internal reference
        LoginID NVARCHAR(100) NOT NULL DEFAULT(''), --user assigned, used to login to environment
        UserName NVARCHAR(300) NOT NULL DEFAULT(''),
        AuthKey NVARCHAR(4000) NOT NULL DEFAULT(''), -- password in encrypted form
        UserDisabled BIT NOT NULL DEFAULT(0),
        CompanyList NVARCHAR(1000) NOT NULL DEFAULT(''),
        SSODomain NVARCHAR(50) NOT NULL DEFAULT(''),
        SSOUser NVARCHAR(300) NOT NULL DEFAULT('')
    )


    ALTER TABLE [Erp].[User] ADD CONSTRAINT PK_User PRIMARY KEY (UserID)

END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'PurchaseOrderHed') 
BEGIN

    CREATE TABLE [Erp].[PurchaseOrderHed] (
        CompanyID NVARCHAR(8) NOT NULL DEFAULT(''),
        PurchaseOrderNum INT NOT NULL DEFAULT(0),
        SupplierID INT NOT NULL DEFAULT(0),
        OrderDate DATETIME NULL,
        CurrencyCode NVARCHAR(10) NOT NULL DEFAULT(''),
        CreatedByUserID INT NOT NULL DEFAULT(0),
        CreatedDate DATETIME NULL,
        ApprovalStatus NVARCHAR(1) NOT NULL DEFAULT(''),
        ApprovedDate DATETIME NULL,
        LastChangeDate DATETIME NULL,
        LastChangeUser INT NOT NULL DEFAULT(0)
    )


    ALTER TABLE [Erp].[PurchaseOrderHed] ADD CONSTRAINT PK_PurchaseOrderHed PRIMARY KEY (CompanyID, PurchaseOrderNum)

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
        LastChangeUser INT NOT NULL DEFAULT(0)
    )


    ALTER TABLE [Erp].[PurchaseOrderDtl] ADD CONSTRAINT PK_PurchaseOrderDtl PRIMARY KEY (CompanyID, PurchaseOrderNum, PurchaseOrderLineNum)


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
        LastChangeUser INT NOT NULL DEFAULT(0)
    )


    ALTER TABLE [Erp].[PurchaseOrderRel] ADD CONSTRAINT PK_PurchaseOrderRel PRIMARY KEY (CompanyID, PurchaseOrderNum, PurchaseOrderLineNum, PurchaseOrderRelNum)

END

IF NOT EXISTS (SELECT * FROM sys.tables where tables.name = 'Supplier') 
BEGIN

    CREATE TABLE [Erp].[Supplier] (
        CompanyID NVARCHAR(8) NOT NULL DEFAULT(''),
        SupplierID INT NOT NULL DEFAULT(0),
        SupplierName NVARCHAR(200) NOT NULL DEFAULT(''),
        Active BIT NOT NULL DEFAULT(0),
        AddressID INT NOT NULL DEFAULT(0)
    )


    ALTER TABLE [Erp].[Supplier] ADD CONSTRAINT PK_Supplier PRIMARY KEY (CompanyID, SupplierID)

END

--SEED DATA

/* IF NOT EXISTS (SELECT * FROM Erp.Company WHERE Company = 'TEST1')
BEGIN
    INSERT INTO Erp.Company VALUES ('TEST1', 'Test Company 1', 1)
    INSERT INTO Erp.Company VALUES ('XMPL1', 'Example Company 1', 1)
END
GO
 */