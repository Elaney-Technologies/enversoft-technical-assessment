USE EnverSoftSuppliersDb;
GO

-- Search by company name
SELECT TOP 1 Name, TelephoneNo
FROM dbo.Suppliers
WHERE Name = 'Eskom Holdings Limited';
GO

-- Insert new supplier manually
INSERT INTO dbo.Suppliers (Code, Name, TelephoneNo)
VALUES (999999, 'Demo Supplier', '0100000000');
GO
