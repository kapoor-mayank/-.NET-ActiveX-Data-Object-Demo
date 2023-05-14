create procedure insertentry
@pname nvarchar(max),
@pprice float,
@pqty int
as
begin
insert into dbo.product(Name, Price, Qty) values (@pname, @pprice, @pqty)
end
