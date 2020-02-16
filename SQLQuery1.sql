/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Name]
      ,[VehicleType]
      ,[Purpose]
  FROM [EFDB].[dbo].[Rocket1]
  


  insert into Rocket1 values(1001,'Rocket-1','PSLV','Research')
  select * from Rocket1