IF DB_ID('EnverSoftSuppliersDb') IS NULL
BEGIN
    CREATE DATABASE EnverSoftSuppliersDb;
END
GO

USE EnverSoftSuppliersDb;
GO

IF OBJECT_ID('dbo.Suppliers', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Suppliers;
END
GO

CREATE TABLE dbo.Suppliers
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Code INT NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    TelephoneNo NVARCHAR(50) NOT NULL,
    CreatedUtc DATETIME2 NOT NULL CONSTRAINT DF_Suppliers_CreatedUtc DEFAULT SYSUTCDATETIME(),
    CONSTRAINT UQ_Suppliers_Name UNIQUE (Name)
);
GO

INSERT INTO dbo.Suppliers (Code, Name, TelephoneNo)
VALUES
(234, N'Eskom Holdings Limited', N'086 0037566'),
(939, N'Focus Rooms (Pty) Ltd', N'0820776910'),
(34, N'GSM Electro', N'0128110069'),
(1264, N'Jody and Herman Investments CC', N'0118864227'),
(5667, N'Johan Le Roux Ingenieurswerke', N'0233423390'),
(667, N'L. J. Ross t/a Petite Cafe''', N'0117868101'),
(45, N'L.A Auto Center  CC t/a LA Body Works', N'0219488412'),
(1351, N'LG Tow-In CC', N'0828044026'),
(1352, N'LM Greyling t/aThe Electric Advertiser', N'0119545972'),
(1437, N'M.H Cloete Enterprises (Pty) Ltd  t/a  Rola Motors', N'0218418300'),
(67, N'M.M Hydraulics CC', N'011425 6578'),
(1980, N'Phulo Human Capital (Pty) Ltd', N'0114755934'),
(345, N'Picaro 115 CC t/a H2O CT Services', N'0216745710'),
(2279, N'Safetygrip CC', N'0117086660'),
(876, N'Safic (Pty) Ltd', N'0114064000'),
(2549, N'The Financial Planning Institute Of Southern Africa', N'0861000374'),
(935, N'The Fitment Zone  CC', N'0118234181'),
(2693, N'Turnweld Engineering CC', N'0115468790'),
(6, N'Tutuka Motor Holdings Pty Ltd t/a Tutuka Motor Lab', N'0117044324'),
(134, N'WP Exhaust Brake & Clutch t/a In Focus Fleet Services', N'0219055028'),
(3277, N'WP Sekuriteit', N'0233421732'),
(53, N'Brietta Trading (Pty) Ltd', N'0115526000'),
(392, N'C.N. Braam t/a CNB Electrical Services', N'0832835399'),
(625, N'Creative Crew (Pty) Ltd', N'0120040218');
GO

SELECT COUNT(*) AS SeededSupplierCount FROM dbo.Suppliers;
GO
