

create database BD_proyecto

use BD_proyecto

create table USUARIO(
idUsuario int primary key identity (1,1),
Correo varchar(100),
Clave varchar(500)
)

create proc sp_RegistrarUsuario(
@Correo varchar(100),
@Clave varchar(500),
@Registrado bit output, 
@Mensaje varchar(100) output 
)
as
begin

if(not exists(select * from USUARIO where Correo = @Correo))
begin
	insert into USUARIO(Correo,Clave) values(@Correo,@Clave)
	set @Registrado = 1
	set @Mensaje ='usuario registrado'
	end
	else
	begin
		set @Registrado = 0
		set @Mensaje ='correo ya se encuentra registrado'
		end
end

		select * from USUARIO


create proc sp_ValidarUsuario(
@Correo varchar(100),
@Clave varchar(500)
)
as
begin
	if(exists(select * from USUARIO where Correo = @Correo and Clave =@Clave))
				select IdUsuario from USUARIO where Correo =@Correo and Clave =@Clave
	else
		select '0'
end

	declare @registrado bit, @mensaje varchar(100)

	exec sp_RegistrarUsuario 'prueba@gmail.com','123456',@registrado output, @mensaje output
	select @registrado
	select @mensaje



