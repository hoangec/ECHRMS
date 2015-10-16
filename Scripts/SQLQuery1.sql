USE HNGHRMS;
delete from Terminations;
delete from Employees;
delete from Companies;
DBCC CHECKIDENT('[Companies]', RESEED, 0);
DBCC CHECKIDENT('[Employees]', RESEED, 0);

DECLARE @i int =1;
DECLARE @j int = 1;
DECLARE @numOfEmployees int;
DECLARE @salary int;
DECLARE @Upper INT = 100;
DECLARE @Lower INT = 300;
DECLARE @Upper2 INT = 5000000;
DECLARE @Lower2 INT = 50000000;
--
WHILE @i < 15
BEGIN
	insert into COMPANIES(CompanyName) values ('Công Ty Agrico ' + cast(@i as varchar(10))) ;
	SET @numOfEmployees = ROUND(((@Upper - @Lower -1) * RAND() + @Lower), 0);
	WHILE @j < @numOfEmployees
	BEGIN
		SET @salary = ROUND(((@Upper2 - @Lower2 -1) * RAND() + @Lower2), 0);
		INSERT INTO EMPLOYEES(NAME,GENDER,CompanyId,Address,JoinedDate,Departement,Position,Salary,Status)
		VALUES('Nguyen Van A-' + cast(@i as varchar(10)),0,@i,CAST(@i as varchar) + ' Tran Hung Dao',getdate() - FLOOR(RAND()*(2000-100)+100),'Phong A ' + CAST(@i as varchar),'Nhan vien', @salary,0)
		set @j = @j + 1;
	END
	set @j = 1;
	set @i = @i +1;
END
--

