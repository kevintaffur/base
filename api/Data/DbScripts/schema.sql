create database Productos;

use Productos;

create table Roles (
	Id int primary key identity(1,1),
	Nombre varchar(20) not null,
);
insert into Roles(Nombre) values('admin');
insert into Roles(Nombre) values('user');

create table Usuarios (
	Id int primary key identity(1,1),
	Username varchar(20) not null,
	PasswordHash varchar(255) not null,
	RolId int not null,

	constraint usuarios_roles_fk foreign key(RolId) references Roles (Id),
);

-- hash es largo por lo que no puede ser corto en la especificación.
alter table Usuarios alter column PasswordHash varchar(255) not null;

create table Productos (
	Id int primary key identity(1,1),
	Nombre varchar(20) not null,
	Descripcion varchar(100) not null,
	Estado varchar(1) not null,
	UsuarioId int not null,

	constraint productos_usuarios_fk foreign key(UsuarioId) references Usuarios(Id),
);

select * from roles;
select * from usuarios;
select * from productos;