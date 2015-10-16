insert into AspNetRoles(Id,Name,Discriminator) values (NEWID(),'Admin','ApplicationRole');
insert into AspNetRoles(Id,Name,Discriminator) values (NEWID(),'Manager','ApplicationRole');
insert into AspNetRoles(Id,Name,Discriminator) values (NEWID(),'SuperUser','ApplicationRole');
insert into AspNetRoles(Id,Name,Discriminator) values (NEWID(),'User','ApplicationRole');

select * from AspNetRoles