create Database AzureItemDB;
GO

Create Table Items (
Id int Identity (1, 1) Primary key,
Name Varchar(20),
Quality Varchar(20),
Quantity float);

insert into Item values ('milk','low',2)
insert into Item values ('fruit', 'High', 3)