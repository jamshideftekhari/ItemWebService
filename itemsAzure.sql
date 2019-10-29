--create Database AzureItemDB;
--GO

Create Table Items (
Id int Identity (1, 1) Primary key,
Name Varchar(20),
Quality Varchar(20),
Quantity float);

insert into Items values ('milk','low',2)
insert into Items values ('fruit', 'High', 3)
insert into Items values ('bread', 'High', 3)