DELETE FROM dbo.Orders
DBCC CHECKIDENT ('Orders', RESEED, 0)
GO

DELETE FROM dbo.Employees
DBCC CHECKIDENT ('Employees', RESEED, 0)
GO

DELETE FROM dbo.Posts
DBCC CHECKIDENT ('Posts', RESEED, 0)
GO

DELETE FROM dbo.RepairedModels
DBCC CHECKIDENT ('RepairedModels', RESEED, 0)
GO

DELETE FROM dbo.ServicedStores
DBCC CHECKIDENT ('ServicedStores', RESEED, 0)
GO

DELETE FROM dbo.TypeOfFaults
DBCC CHECKIDENT ('TypeOfFaults', RESEED, 0)
GO

DELETE FROM dbo.SpareParts
DBCC CHECKIDENT ('SpareParts', RESEED, 0)
GO

DELETE FROM dbo.RepairedModels
DBCC CHECKIDENT ('RepairedModels', RESEED, 0)
GO