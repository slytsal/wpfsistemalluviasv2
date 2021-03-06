USE [db_SeguimientoProtocolo_r2_v2]
GO
/****** Object:  StoredProcedure [dbo].[spCommitCiRegistroDataTemp]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[spCommitCiRegistroDataTemp](
	@sessionUnid bigint
)
AS
BEGIN
	declare @tempTblTable nvarchar(100)
	declare @sqlStmt nvarchar(max)
	DECLARE @Return BIT=1
	
	select @tempTblTable='TMP_CI_REGISTRO_'+CAST(@sessionUnid AS NVARCHAR(100));
	
	SELECT @sqlStmt='
		IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = ''@@TmpTableName'') BEGIN
			--Insert de datos
			insert into CI_REGISTRO
			SELECT *
			FROM @@TmpTableName as tmp
			WHERE NOT EXISTS(
				SELECT *
				FROM CI_REGISTRO as cr
				WHERE 1=1
					AND cr.FechaNumerica=tmp.FechaNumerica
					and cr.IdRegistro=tmp.IdRegistro
			)
			
			--Update de todos los registros
			UPDATE cr SET
				--cr.IdRegistro=tmp.IdRegistro,
				--cr.IdPuntoMedicion=tmp.IdPuntoMedicion,
				cr.FechaCaptura=tmp.FechaCaptura,
				cr.HoraRegistro=tmp.HoraRegistro,
				cr.DiaRegistro=tmp.DiaRegistro,
				cr.Valor=tmp.Valor,
				cr.AccionActual=tmp.AccionActual,
				cr.IsActive=tmp.IsActive,
				cr.IsModified=tmp.IsModified,
				cr.LastModifiedDate=tmp.LastModifiedDate,
				cr.IdCondicion=tmp.IdCondicion,
				cr.ServerLastModifiedDate=tmp.ServerLastModifiedDate
				--cr.FechaNumerica=tmp.FechaNumerica,
			FROM CI_REGISTRO as cr
				INNER JOIN @@TmpTableName as tmp
				on cr.FechaNumerica=tmp.FechaNumerica
				and cr.IdPuntoMedicion=tmp.IdPuntoMedicion
				AND cr.LastModifiedDate<tmp.LastModifiedDate
		END
	';
	
	select @sqlStmt=replace(@sqlStmt,'@@TmpTableName',@tempTblTable);
	
	exec(@sqlStmt);
	SELECT @Return
	print @sqlStmt
END


/***
--Test de stored procedure
exec [spCommitCiRegistroDataTemp] 20130515135610664

--Generar columnas de set
select
	'cr.'+column_name+'=tmp.'+column_name+','
from information_schema.columns
where table_name like 'ci_registro'
**/
GO
/****** Object:  StoredProcedure [dbo].[spCommitBulkUpsertCiRegistroRecurrent]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[spCommitBulkUpsertCiRegistroRecurrent]
AS
BEGIN
	declare @tempTblTable nvarchar(100)
	declare @sqlStmt nvarchar(max)
	DECLARE @Return BIT=1
	
	--select @tempTblTable='TMP_CI_REGISTRO_'+CAST(@sessionUnid AS NVARCHAR(100));
	select @tempTblTable='TMP_CI_REGISTRO_RECURRENTE';
	
	SELECT @sqlStmt='
		IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = ''@@TmpTableName'') BEGIN
			--Insert de datos
			insert into CI_REGISTRO
			SELECT distinct *
			FROM @@TmpTableName as tmp
			WHERE NOT EXISTS(
				SELECT *
				FROM CI_REGISTRO as cr
				WHERE 1=1
					AND cr.FechaNumerica=tmp.FechaNumerica
					AND cr.IdPuntoMedicion=tmp.IdPuntoMedicion
					
			)
			
			--Update de todos los registros
			UPDATE cr SET
				--cr.IdRegistro=tmp.IdRegistro,
				--cr.IdPuntoMedicion=tmp.IdPuntoMedicion,
				cr.FechaCaptura=tmp.FechaCaptura,
				cr.HoraRegistro=tmp.HoraRegistro,
				cr.DiaRegistro=tmp.DiaRegistro,
				cr.Valor=tmp.Valor,
				cr.AccionActual=tmp.AccionActual,
				cr.IsActive=tmp.IsActive,
				cr.IsModified=tmp.IsModified,
				cr.LastModifiedDate=tmp.LastModifiedDate,
				cr.IdCondicion=tmp.IdCondicion,
				cr.ServerLastModifiedDate=tmp.ServerLastModifiedDate
				--cr.FechaNumerica=tmp.FechaNumerica,
			FROM CI_REGISTRO as cr
				INNER JOIN @@TmpTableName as tmp
				on cr.FechaNumerica=tmp.FechaNumerica
				and cr.IdPuntoMedicion=tmp.IdPuntoMedicion
				AND cr.LastModifiedDate<tmp.LastModifiedDate
		END
	';
	
	select @sqlStmt=replace(@sqlStmt,'@@TmpTableName',@tempTblTable);
	
	exec(@sqlStmt);
	SELECT @Return
	print @sqlStmt
END


/***
--Test de stored procedure
exec [spCommitBulkUpsertCiRegistroRecurrent] 20130515135610664

--Generar columnas de set
select
	'cr.'+column_name+'=tmp.'+column_name+','
from information_schema.columns
where table_name like 'ci_registro'
**/
GO
/****** Object:  StoredProcedure [dbo].[spCommitBulkUpsertCiRegistroOnDemand]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[spCommitBulkUpsertCiRegistroOnDemand]
AS
BEGIN
	declare @tempTblTable nvarchar(100)
	declare @sqlStmt nvarchar(max)
	DECLARE @Return BIT=1
	
	--select @tempTblTable='TMP_CI_REGISTRO_'+CAST(@sessionUnid AS NVARCHAR(100));
	select @tempTblTable='TMP_CI_REGISTRO_ONDEMAND';
	
	SELECT @sqlStmt='
		IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = ''@@TmpTableName'') BEGIN
			--Insert de datos
			insert into CI_REGISTRO
			SELECT distinct *
			FROM @@TmpTableName as tmp
			WHERE NOT EXISTS(
				SELECT *
				FROM CI_REGISTRO as cr
				WHERE 1=1
					AND cr.FechaNumerica=tmp.FechaNumerica
					AND cr.IdPuntoMedicion=tmp.IdPuntoMedicion
			)
			
			--Update de todos los registros
			UPDATE cr SET
				--cr.IdRegistro=tmp.IdRegistro,
				--cr.IdPuntoMedicion=tmp.IdPuntoMedicion,
				cr.FechaCaptura=tmp.FechaCaptura,
				cr.HoraRegistro=tmp.HoraRegistro,
				cr.DiaRegistro=tmp.DiaRegistro,
				cr.Valor=tmp.Valor,
				cr.AccionActual=tmp.AccionActual,
				cr.IsActive=tmp.IsActive,
				cr.IsModified=tmp.IsModified,
				cr.LastModifiedDate=tmp.LastModifiedDate,
				cr.IdCondicion=tmp.IdCondicion,
				cr.ServerLastModifiedDate=tmp.ServerLastModifiedDate
				--cr.FechaNumerica=tmp.FechaNumerica,
			FROM CI_REGISTRO as cr
				INNER JOIN @@TmpTableName as tmp
				on cr.FechaNumerica=tmp.FechaNumerica
				and cr.IdPuntoMedicion=tmp.IdPuntoMedicion
				AND cr.LastModifiedDate<tmp.LastModifiedDate
		END
	';
	
	select @sqlStmt=replace(@sqlStmt,'@@TmpTableName',@tempTblTable);
	
	exec(@sqlStmt);
	SELECT @Return
	print @sqlStmt
END


/***
--Test de stored procedure
exec [spCommitBulkUpsertCiRegistroOnDemand] 20130515135610664

--Generar columnas de set
select
	'cr.'+column_name+'=tmp.'+column_name+','
from information_schema.columns
where table_name like 'ci_registro'
**/
GO
/****** Object:  StoredProcedure [dbo].[spCommitBulkUpsertCiRegistroDownloadOnDemand]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCommitBulkUpsertCiRegistroDownloadOnDemand](
	@session bigint
)
AS
BEGIN
	declare @tempTblTable nvarchar(100)
	DECLARE @Return BIT=1
	select @tempTblTable='TMP_CI_REGISTRO_DOWNLOAD_ONDEMAND_'+cast(@session as nvarchar(100));
	
	declare @sqlStmt nvarchar(max)='';
	select @sqlStmt='	
	declare @unid bigint = dbo.UDF_NewUnid();
	--===================================
	--Actualizar nuevos registros
	--===================================

	;with baseTmp as (
		select 
			IdPuntoMedicion, FechaNumerica, max(LastModifiedDate) as LastModifiedDate
		FROM @@tbl as tmp
		group by IdPuntoMedicion, FechaNumerica
	),distinctTemp as (
		select tmp.*
		FROM @@tbl as tmp
			inner join baseTmp as base
				on base.IdPuntoMedicion=tmp.IdPuntoMedicion
				and base.FechaNumerica=tmp.FechaNumerica
				and base.LastModifiedDate=tmp.LastModifiedDate
	)

	--Update a los registros que sean diferentes
	UPDATE cr SET
		--cr.IdRegistro=tmp.IdRegistro,
		--cr.IdPuntoMedicion=tmp.IdPuntoMedicion,
		cr.FechaCaptura=tmp.FechaCaptura,
		cr.HoraRegistro=tmp.HoraRegistro,
		cr.DiaRegistro=tmp.DiaRegistro,
		cr.Valor=tmp.Valor,
		cr.AccionActual=tmp.AccionActual,
		cr.IsActive=tmp.IsActive,
		cr.IsModified=tmp.IsModified,
		cr.LastModifiedDate=tmp.LastModifiedDate,
		cr.IdCondicion=tmp.IdCondicion,
		cr.ServerLastModifiedDate=tmp.ServerLastModifiedDate
		--cr.FechaNumerica=tmp.FechaNumerica,
	FROM CI_REGISTRO as cr
		INNER JOIN distinctTemp as tmp
		on cr.FechaNumerica=tmp.FechaNumerica
		and cr.IdPuntoMedicion=tmp.IdPuntoMedicion
		
	--select @Return=case when @@rowcount>0 then 1 else 0 end; 
		
	--===================================
	--Insercion de nuevos registros
	--===================================
	;with baseTmp as (
		select 
			IdPuntoMedicion, FechaNumerica
		FROM @@tbl as tmp
		group by IdPuntoMedicion, FechaNumerica
	),distinctTemp as (
		select tmp.*
		FROM @@tbl as tmp
			inner join baseTmp as base
				on base.IdPuntoMedicion=tmp.IdPuntoMedicion
				and base.FechaNumerica=tmp.FechaNumerica				
	),newRecords as (
		SELECT 
			dbo.UDF_NewUnid() + row_number() over( order by 
				FechaCaptura desc,IdPuntoMedicion, HoraRegistro, DiaRegistro, Valor, AccionActual, IsActive, IsModified, LastModifiedDate, IdCondicion, ServerLastModifiedDate, FechaNumerica	
			) as IdRegistro,
			IdPuntoMedicion, FechaCaptura, HoraRegistro, DiaRegistro, Valor, AccionActual, IsActive, IsModified, LastModifiedDate, IdCondicion, ServerLastModifiedDate, FechaNumerica
		FROM @@tbl as tmp
		WHERE NOT EXISTS(
			SELECT *
			FROM CI_REGISTRO as cr
			WHERE 1=1
				AND cr.FechaNumerica=tmp.FechaNumerica
				and cr.IdPuntoMedicion=tmp.IdPuntoMedicion
		)	
	)
	insert into CI_REGISTRO
	select *
	from newRecords
	
	--select tmp.IdPuntoMedicion,tmp.FechaNumerica,tmp.LastModifiedDate,max(cr.ServerLastModifiedDate) as ServerLastModifiedDate
	--from @@tbl as tmp
	--	inner join CI_REGISTRO as cr
	--		on cr.FechaNumerica=tmp.FechaNumerica
	--		and cr.IdPuntoMedicion=tmp.IdPuntoMedicion
	--group by tmp.IdPuntoMedicion,tmp.FechaNumerica,tmp.LastModifiedDate
	--order by 2,1

	drop table @@tbl
	';
	
	
	
	select @sqlStmt=replace(@sqlStmt,'@@tbl',@tempTblTable)	
	EXEC (@sqlStmt)
	print @sqlStmt
	SELECT @Return
END


/***
--Test de stored procedure
exec spCommitBulkUpsertCiRegistroUploaded 20140407180642132

**/
GO
/****** Object:  StoredProcedure [dbo].[spFinishCiRegistroDataTemp]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[spFinishCiRegistroDataTemp](
	@sessionUnid bigint
)
AS
BEGIN
	declare @tempTblTable nvarchar(100)
	declare @sqlStmt nvarchar(max)
	DECLARE @Return BIT=1
	
	select @tempTblTable='TMP_CI_REGISTRO_'+CAST(@sessionUnid AS NVARCHAR(100));
	
	SELECT @sqlStmt='
		IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = ''@@TmpTableName'') BEGIN
			DROP TABLE @@TmpTableName;
		END
	';
	
	select @sqlStmt=replace(@sqlStmt,'@@TmpTableName',@tempTblTable);
	
	exec(@sqlStmt);
	SELECT @Return
END

/**
*TEST
EXEC spFinishCiRegistroDataTemp 20130101
**/
GO
/****** Object:  StoredProcedure [dbo].[spInsertCiRegistroDataTemp]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertCiRegistroDataTemp](
	@IdRegistro bigint,
	@IdPuntoMedicion bigint,
	@FechaCaptura datetime,
	@HoraRegistro int,
	@DiaRegistro int,
	@Valor float,
	@AccionActual nvarchar(1000),
	@IsActive bit,
	@IsModified bit,
	@LastModifiedDate bigint,
	@IdCondicion bigint,
	@ServerLastModifiedDate bigint,
	@FechaNumerica bigint,
	@SessionUnid bigint
)
AS
BEGIN
	declare @tempTblTable nvarchar(100)
	declare @sqlStmt nvarchar(max)
	DECLARE @Return BIT=1
	
	select @tempTblTable='TMP_CI_REGISTRO_'+CAST(@SessionUnid AS NVARCHAR(100));
	
	SELECT @sqlStmt='
		set dateformat ymd;
		IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = ''@@TmpTableName'') BEGIN
			insert into @@TmpTableName(IdRegistro, IdPuntoMedicion, FechaCaptura, HoraRegistro, DiaRegistro, Valor, AccionActual, IsActive, IsModified, LastModifiedDate, IdCondicion, ServerLastModifiedDate, FechaNumerica)
			select 
				@@IdRegistro,
				@@IdPuntoMedicion,
				''@@FechaCaptura'',
				@@HoraRegistro,
				@@DiaRegistro,
				@@Valor,
				''@@AccionActual'',
				@@IsActive,
				@@IsModified,
				@@LastModifiedDate,
				@@IdCondicion,
				@@ServerLastModifiedDate,
				@@FechaNumerica
		END
	';
	
	select @sqlStmt=replace(@sqlStmt,'@@TmpTableName',@tempTblTable);
	
	--
	select @sqlStmt=replace(@sqlStmt,'@@IdRegistro',cast(@IdRegistro as nvarchar(100)) );
	select @sqlStmt=replace(@sqlStmt,'@@IdPuntoMedicion',cast(@IdPuntoMedicion as nvarchar(100)) );
	select @sqlStmt=replace(@sqlStmt,'@@FechaCaptura',convert(nvarchar(100),@FechaCaptura,121));
	select @sqlStmt=replace(@sqlStmt,'@@HoraRegistro',cast(@HoraRegistro as nvarchar(100)) );
	select @sqlStmt=replace(@sqlStmt,'@@DiaRegistro',cast(@DiaRegistro as nvarchar(100)) );
	select @sqlStmt=replace(@sqlStmt,'@@Valor',cast(@Valor as nvarchar(100)) );
	select @sqlStmt=replace(@sqlStmt,'@@AccionActual',@AccionActual);
	select @sqlStmt=replace(@sqlStmt,'@@IsActive',cast(@IsActive as nvarchar(100)) );
	select @sqlStmt=replace(@sqlStmt,'@@IsModified',cast(@IsModified as nvarchar(100)) );
	select @sqlStmt=replace(@sqlStmt,'@@LastModifiedDate',cast(@LastModifiedDate as nvarchar(100)) );
	select @sqlStmt=replace(@sqlStmt,'@@IdCondicion',cast(@IdCondicion as nvarchar(100)) );
	select @sqlStmt=replace(@sqlStmt,'@@ServerLastModifiedDate',cast(@ServerLastModifiedDate as nvarchar(100)) );
	select @sqlStmt=replace(@sqlStmt,'@@FechaNumerica',cast(@FechaNumerica as nvarchar(100)) );
	
	
	exec(@sqlStmt);
	SELECT @Return
	
END

/**
*TEST
EXEC [spInsertCiRegistroDataTemp] 
	20130515135610664,1022,'2013-05-15 02:00:00.000',1356,15,2.21,'prueba',1,0,20130515143505728,1,0,201403091657,
	20130515135610664
	
**/

/***
--Generar parametros en base a las columnas de la tabla
select
	'@@'+column_name+' '+data_type+ coalesce(('('+cast(character_maximum_length as nvarchar(100))+')'),'')+','
--select *
from information_schema.columns
where table_name like 'ci_registro'

--Generar codigo de variables para query dinamico
select
	'@@'+column_name+','
from information_schema.columns
where table_name like 'ci_registro'

--Generar codigo para reeplazo de cadenas
select
	'select @sqlStmt=replace(@sqlStmt,''@@'+column_name+''',cast(@'+column_name+' as nvarchar(100)) );'
from information_schema.columns
where table_name like 'ci_registro'

select *
from ci_registro
****/
GO
/****** Object:  StoredProcedure [dbo].[spGetMaxTable]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetMaxTable]
(
	@TableName NVARCHAR(MAX)
)
AS
BEGIN



	DECLARE @query NVARCHAR(MAX)='SELECT 
	CAST(ISNULL(MAX(LastModifiedDate),0) AS BIGINT) AS LastModifiedDate,
	CAST(ISNULL(MAX(ServerLastModifiedDate),0) AS BIGINT) AS ServerLastModifiedDate 
	FROM _[@TABLENAME]_'

	IF EXISTS (SELECT DISTINCT TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName )
		BEGIN 
			BEGIN TRY		
				SELECT @query=REPLACE(@query,'_[@TABLENAME]_',@TableName) 				
				EXECUTE (@query)	
			END TRY
			BEGIN CATCH
				SELECT CAST(0 AS BIGINT) AS LastModifiedDate,CAST(0 AS BIGINT) AS ServerLastModifiedDate
			END CATCH;
		END
	ELSE
		BEGIN
			SELECT CAST(0 AS BIGINT) AS LastModifiedDate,CAST(0 AS BIGINT) AS ServerLastModifiedDate
		END
END
GO
/****** Object:  StoredProcedure [dbo].[spPrepareBulkUpsertCiRegistroRecurrent]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[spPrepareBulkUpsertCiRegistroRecurrent]
AS
BEGIN
	declare @tempTblTable nvarchar(100)
	declare @sqlStmt nvarchar(max)
	DECLARE @Return BIT=1
	
	--select @tempTblTable='TMP_CI_REGISTRO_'+CAST(@sessionUnid AS NVARCHAR(100));
	select @tempTblTable='TMP_CI_REGISTRO_RECURRENTE';
	
	SELECT @sqlStmt='
		TRUNCATE TABLE @@TmpTableName
	';
	
	select @sqlStmt=replace(@sqlStmt,'@@TmpTableName',@tempTblTable);
	
	exec(@sqlStmt);
	SELECT @Return
	print @sqlStmt
END


/***
--Test de stored procedure
exec [spPrepareBulkUpsertCiRegistroRecurrent] 20130515135610664

--Generar columnas de set
select
	'cr.'+column_name+'=tmp.'+column_name+','
from information_schema.columns
where table_name like 'ci_registro'
**/
GO
/****** Object:  StoredProcedure [dbo].[spPrepareBulkUpsertCiRegistroOnDemand]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[spPrepareBulkUpsertCiRegistroOnDemand]
AS
BEGIN
	declare @tempTblTable nvarchar(100)
	declare @sqlStmt nvarchar(max)
	DECLARE @Return BIT=1
	
	--select @tempTblTable='TMP_CI_REGISTRO_'+CAST(@sessionUnid AS NVARCHAR(100));
	select @tempTblTable='dbo.TMP_CI_REGISTRO_ONDEMAND';
	
	SELECT @sqlStmt='
		TRUNCATE TABLE @@TmpTableName
	';
	
	select @sqlStmt=replace(@sqlStmt,'@@TmpTableName',@tempTblTable);
	
	exec(@sqlStmt);
	SELECT @Return
	print @sqlStmt
END


/***
--Test de stored procedure
exec [spPrepareBulkUpsertCiRegistroOnDemand] 20130515135610664

--Generar columnas de set
select
	'cr.'+column_name+'=tmp.'+column_name+','
from information_schema.columns
where table_name like 'ci_registro'
**/
GO
/****** Object:  StoredProcedure [dbo].[spPrepareBulkUpdateCiRegistroConfirmation]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[spPrepareBulkUpdateCiRegistroConfirmation]
AS
BEGIN
	declare @tempTblTable nvarchar(100)
	declare @sqlStmt nvarchar(max)
	DECLARE @Return BIT=1
	
	--select @tempTblTable='TMP_CI_REGISTRO_'+CAST(@sessionUnid AS NVARCHAR(100));
	select @tempTblTable='dbo.TMP_CI_REGISTRO_CONFIRMATION';
	
	SELECT @sqlStmt='
		TRUNCATE TABLE @@TmpTableName
	';
	
	select @sqlStmt=replace(@sqlStmt,'@@TmpTableName',@tempTblTable);
	
	exec(@sqlStmt);
	SELECT @Return
	print @sqlStmt
END


/***
--Test de stored procedure
exec [spPrepareBulkUpdateCiRegistroConfirmation] 20130515135610664

--Generar columnas de set
select
	'cr.'+column_name+'=tmp.'+column_name+','
from information_schema.columns
where table_name like 'ci_registro'
**/
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_NewUnid]    Script Date: 04/29/2014 14:03:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_NewUnid]()

returns nvarchar(32)

AS

BEGIN

DECLARE @unid NVARCHAR(32)

SET @unid=CONVERT(NVARCHAR(32),GETDATE(),121)

SELECT @unid=REPLACE(REPLACE(REPLACE(REPLACE(@unid,'-',''),':',''),'.',''),' ','')

RETURN @unid

END
GO
/****** Object:  StoredProcedure [dbo].[spSelectCurrentUser]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSelectCurrentUser]
AS
BEGIN
	SELECT IdUsuario ,
          UsuarioCorreo ,
          CAST(DECRYPTBYPASSPHRASE('pass',UsuarioPwd) AS VARCHAR(max))as UsuarioPwd,
          Nombre ,
          Apellido ,
          Area ,
          Puesto ,
          IsActive ,
          IsModified ,
          LastModifiedDate ,
          IsNewUser ,
          IsVerified ,
          IsMailSent ,
          ServerLastModifiedDate
        FROM dbo.APP_USUARIO	
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertUsuario]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertUsuario]
(
--	DECLARE 
			@IdUsuario bigint,
			@UsuarioCorreo NVARCHAR(max),
			@UsuarioPwd VARCHAR(max),
			@Nombre nvarchar(max),
			@Apellido nvarchar(max),
			@Area nvarchar(max),
			@Puesto nvarchar(max),
			@IsActive bit,
			@IsModified bit,
			@LastModifiedDate bigint,
			@IsNewUser bit,
			@IsVerified bit,
			@IsMailSent bit,
			@ServerLastModifiedDate bigint
)
AS
BEGIN
	DECLARE @Return BIT=0
	
	BEGIN TRY
		INSERT INTO dbo.APP_USUARIO
			( IdUsuario ,
			  UsuarioCorreo ,
			  UsuarioPwd ,
			  Nombre ,
			  Apellido ,
			  Area ,
			  Puesto ,
			  IsActive ,
			  IsModified ,
			  LastModifiedDate ,
			  IsNewUser ,
			  IsVerified ,
			  IsMailSent ,
			  ServerLastModifiedDate
			)
	VALUES  ( 
				@IdUsuario,
				@UsuarioCorreo,
				ENCRYPTBYPASSPHRASE('pass',@UsuarioPwd),
				@Nombre,
				@Apellido,
				@Area,
				@Puesto,
				@IsActive,
				@IsModified,
				@LastModifiedDate,
				@IsNewUser,
				@IsVerified,
				@IsMailSent,
				@ServerLastModifiedDate
			)	
			SET @Return =1
	END TRY
	
	BEGIN CATCH
		SET @Return=0
	END	CATCH			
	SELECT @Return	
END
GO
/****** Object:  StoredProcedure [dbo].[spCreateCiRegistroDataTemp]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[spCreateCiRegistroDataTemp]
AS
BEGIN
	declare @sessionUnid bigint = dbo.UDF_NewUnid()
	declare @tempTblTable nvarchar(100)
	declare @sqlStmt nvarchar(max)
	
	--Inicializar cadenas para query dinamico.
	select @tempTblTable='TMP_CI_REGISTRO_'+CAST(@sessionUnid AS NVARCHAR(100));
	
	SELECT @sqlStmt='
		IF NOT OBJECT_ID(''@@TmpTableName'',''U'') IS NULL BEGIN
			DROP TABLE @@TmpTableName
		END
		
		SELECT TOP 1 *
			INTO @@TmpTableName
		FROM CI_REGISTRO;
		
		TRUNCATE TABLE @@TmpTableName;
		
		--TODO: Considerar crear indices
	';
	
	select @sqlStmt=replace(@sqlStmt,'@@TmpTableName',@tempTblTable);
	
	--Ejecutar query dinamico
	exec(@sqlStmt);
	
	--Se retorna el id de la tabla generado para manejo desde la app
	select @sessionUnid;
END
GO
/****** Object:  StoredProcedure [dbo].[spCurrentUser]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCurrentUser]
(
	@IdUsuario BIGINT,
	@IsSveSesion BIT
)
AS 
BEGIN 
	UPDATE dbo.CAT_SESION
		SET
			IdUsuario=@IdUsuario,
			IsSaveSesion=@IsSveSesion
		WHERE IdSesion=20140225110320123
	SELECT ISNULL(IsSaveSesion,0) AS IsSaveSesion FROM dbo.CAT_SESION
END
GO
/****** Object:  StoredProcedure [dbo].[spAutoLogin]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAutoLogin]
AS
BEGIN
	DECLARE @True BIT 
	SELECT @True=ISNULL(IsSaveSesion,0) FROM dbo.CAT_SESION	
	IF @True=1	
		BEGIN
			SELECT * FROM dbo.APP_USUARIO
			WHERE IdUsuario=(SELECT IdUsuario FROM dbo.CAT_SESION)
		END
		ELSE
			BEGIN
				SELECT NULL
			END			
END
GO
/****** Object:  StoredProcedure [dbo].[spGetLastSync]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetLastSync]
AS
	BEGIN
		--DECLARE @UltimaSinc NVARCHAR(MAX)=(SELECT 	
		--											MD.ServerModifiedDate	
		--										FROM dbo.MODIFIEDDATA MD
		--											JOIN dbo.SYNCTABLE ST
		--												ON MD.IdSyncTable = ST.IdSyncTable
		--										WHERE ST.SyncTableName='CI_REGISTRO')
		DECLARE @UltimaSinc NVARCHAR(MAX)=(SELECT MAX(LastModifiedDate) FROM dbo.CI_REGISTRO)
		DECLARE @MSJ NVARCHAR(500)=''
		IF(LEN(@UltimaSinc)>1)
			BEGIN
				DECLARE @FechaCompleta NVARCHAR(20)=''
				DECLARE @Anio NVARCHAR(5)=SUBSTRING(@UltimaSinc,1,4)
				DECLARE @Mes NVARCHAR(2)=SUBSTRING(@UltimaSinc,5,2)
				DECLARE @Dia NVARCHAR(2)=SUBSTRING(@UltimaSinc,7,2)
				DECLARE @Hora NVARCHAR(2)=SUBSTRING(@UltimaSinc,9,2)
				DECLARE @Minuto NVARCHAR(2)=SUBSTRING(@UltimaSinc,11,2)
				DECLARE @Segundo NVARCHAR(2)=SUBSTRING(@UltimaSinc,13,2)
				SET @FechaCompleta=@Anio+'-'+@Mes+'-'+@Dia+' '+@Hora+':'+@Minuto+':'+@Segundo		
				DECLARE @fecha DATETIME=CONVERT(DATETIME,@FechaCompleta,120)		
				--SELECT @fecha
				DECLARE @Count INT
				SET @Count=(SELECT DATEDIFF(minute,@fecha,GETDATE()))
				--PRINT @Count
				IF @Count=0
					BEGIN	
						SELECT @MSJ='Hace unos segundos.'
					END		
				ELSE IF @Count<60
					BEGIN 
						SELECT @MSJ=' Hace '+ CONVERT(NVARCHAR(100),DATEDIFF(minute,@fecha,GETDATE()))+' minutos.'
					END
				ELSE IF(@Count>60 AND @Count<119)		
					BEGIN
						SELECT @MSJ ='Hace aproximadamente 1 hora.'
					END
				ELSE IF @Count>119 AND @Count<1139
					BEGIN 
						SELECT @MSJ ='Hace aproximadamente '+ CONVERT(NVARCHAR(100),DATEDIFF(hour,@fecha,GETDATE()))+' horas.'				
					END
				ELSE IF @Count>1139 AND @Count<10079
					BEGIN	
						SELECT @MSJ ='Hace aproximadamente '+ CONVERT(NVARCHAR(100),DATEDIFF(day,@fecha,GETDATE()))+' dias.'				
					END	
				ELSE IF @Count>10079 AND @Count<40319
					BEGIN	
						SELECT @MSJ ='Hace aproximadamente '+ CONVERT(NVARCHAR(100),DATEDIFF(week,@fecha,GETDATE()))+' semanas.'				
					END			
				ELSE IF @Count>40320 AND @Count<161279
					BEGIN	
						SELECT @MSJ ='Hace aproximadamente '+ CONVERT(NVARCHAR(100),DATEDIFF(month,@fecha,GETDATE()))+' meses.'				
					END								
				--PRINT @MSJ
			END
		ELSE
			BEGIN
				SELECT @MSJ='No se ha sincronizado.'
				--PRINT @MSJ
			END			
		SELECT @MSJ
	END
GO
/****** Object:  StoredProcedure [dbo].[spGetDateTimeLastSync]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetDateTimeLastSync]
AS
	BEGIN
		DECLARE @UltimaSinc NVARCHAR(MAX)=(SELECT 	
													MD.ServerModifiedDate	
												FROM dbo.MODIFIEDDATA MD
													JOIN dbo.SYNCTABLE ST
														ON MD.IdSyncTable = ST.IdSyncTable
												WHERE ST.SyncTableName='CI_REGISTRO')
		DECLARE @MSJ NVARCHAR(500)=''
		IF(LEN(@UltimaSinc)>1)
			BEGIN
				DECLARE @FechaCompleta NVARCHAR(20)=''
				DECLARE @Anio NVARCHAR(5)=SUBSTRING(@UltimaSinc,1,4)
				DECLARE @Mes NVARCHAR(2)=SUBSTRING(@UltimaSinc,5,2)
				DECLARE @Dia NVARCHAR(2)=SUBSTRING(@UltimaSinc,7,2)
				DECLARE @Hora NVARCHAR(2)=SUBSTRING(@UltimaSinc,9,2)
				DECLARE @Minuto NVARCHAR(2)=SUBSTRING(@UltimaSinc,11,2)
				DECLARE @Segundo NVARCHAR(2)=SUBSTRING(@UltimaSinc,13,2)
				SET @FechaCompleta=@Anio+'-'+@Mes+'-'+@Dia+' '+@Hora+':'+@Minuto+':'+@Segundo		
				DECLARE @fecha DATETIME=CONVERT(DATETIME,@FechaCompleta,120)		
				SELECT @MSJ=@fecha				
			END
		ELSE
			BEGIN
				SELECT @MSJ='No se ha sincronizado.'				
			END			
		SELECT @MSJ
	END
GO
/****** Object:  StoredProcedure [dbo].[spGetMaxTableCiRegistroRecurrent]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[spGetMaxTableCiRegistroRecurrent]
AS
BEGIN
	/**
	*Si no hay datos en la tabla, la FechaInicio será la fecha del sistema,
	*La fecha FechaFin será 0; lo que ocasionará que el servidor retorne, apartir de la fecha de inicio hacia atras, los x días con datos (dependen de la configuración del servidor)
	**/
	
	DECLARE @InicioDefault BIGINT
	SET @InicioDefault=CONVERT(VARCHAR(50),GETDATE(),112)
----------
SELECT

CAST(ISNULL(MAX(LastModifiedDate),0) AS BIGINT) AS LastModifiedDate,

CAST(ISNULL(MAX(ServerLastModifiedDate),0) AS BIGINT) AS ServerLastModifiedDate,

--CAST(ISNULL(SUBSTRING(cast(max(c.FechaNumerica) as nvarchar(100)),1,8),@InicioDefault) as bigint) as FechaInicio,

@InicioDefault as FechaInicio,

CAST(ISNULL(SUBSTRING(cast(min(c.FechaNumerica) as nvarchar(100)),1,8),@InicioDefault) as bigint) as FechaFin

FROM dbo.CI_REGISTRO as c


	
END
GO
/****** Object:  StoredProcedure [dbo].[spGetMaxTableCiRegistro]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetMaxTableCiRegistro]
(
	@IdPuntoMedicion BIGINT	
)
AS
BEGIN
	--SET @IdPuntoMedicion=1000
	SELECT
	CAST(ISNULL(MAX(LastModifiedDate),0) AS BIGINT) AS LastModifiedDate,
	CAST(ISNULL(MAX(ServerLastModifiedDate),0) AS BIGINT) AS ServerLastModifiedDate
	FROM dbo.CI_REGISTRO
		WHERE IdPuntoMedicion=@IdPuntoMedicion
END
GO
/****** Object:  StoredProcedure [dbo].[spGetCI_REGISTRO]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--USE [db_SeguimientoProtocolo_r2]
--GO
--/****** Object:  StoredProcedure [dbo].[spGetCI_REGISTRO]    Script Date: 04/12/2014 08:32:11 ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
CREATE PROCEDURE [dbo].[spGetCI_REGISTRO]
(
	--DECLARE	
		@FechaActual BIGINT	,
		@IdPuntoMedicion BIGINT	
)		
AS
BEGIN
	

	--SET @FechaActual=20140412
	--SET @IdPuntoMedicion=1000
	DECLARE @Dias INT
	DECLARE @Cero INT=0		

	SELECT @Dias= Value FROM dbo.APP_SETTINGS
			WHERE SettingName='DownloadRecurrent'		
	
	;WITH test AS(
			SELECT				  
				  SUBSTRING(CAST(R.FechaNumerica AS NVARCHAR(50)),0,(LEN(R.FechaNumerica)-3)) AS FechaNumericaSinHora ,				  
				  DENSE_RANK()
					OVER(ORDER BY SUBSTRING(CAST(R.FechaNumerica AS NVARCHAR(50)),0,(LEN(R.FechaNumerica)-3)) DESC) AS ID,
					R.IdPuntoMedicion,
					R.IdRegistro,					
					R.FechaCaptura, R.HoraRegistro, 
					R.DiaRegistro, 
					R.Valor, 
					R.AccionActual, 
					R.IsActive, 
					R.IsModified, R.LastModifiedDate, 
					R.IdCondicion, R.ServerLastModifiedDate, 
					R.FechaNumerica,
					PM.PuntoMedicionName,
					PM.vAccion,PM.vCondicion,PM.Visibility,
					UM.IdUnidadMedida,UM.UnidadMedidaName,UM.UnidadMedidaShort,
					C.CondicionName,C.PathCodicion,
					TP.IdTipoPuntoMedicion,TP.TipoPuntoMedicionName
							FROM dbo.CI_REGISTRO R
								INNER JOIN dbo.CAT_PUNTO_MEDICION PM
									ON R.IdPuntoMedicion = PM.IdPuntoMedicion
								INNER JOIN  dbo.CAT_UNIDAD_MEDIDA UM
									ON PM.IdUnidadMedida = UM.IdUnidadMedida
								INNER JOIN dbo.CAT_CONDPRO C
									ON C.IdCondicion=R.IdCondicion
								INNER JOIN dbo.CAT_TIPO_PUNTO_MEDICION TP
									ON PM.IdTipoPuntoMedicion= TP.IdTipoPuntoMedicion
							WHERE
								PM.IdPuntoMedicion =@IdPuntoMedicion				
								AND CAST(R.FechaNumerica/10000 AS BIGINT)<=@FechaActual	
								AND r.IsActive=1
		),
				
		res AS (
			SELECT			
				IdRegistro, IdPuntoMedicion, FechaCaptura, 
				HoraRegistro, DiaRegistro, Valor, AccionActual, 
				LastModifiedDate, IdCondicion, ServerLastModifiedDate, FechaNumerica,
				PuntoMedicionName,
				vAccion,vCondicion,Visibility,
				IdUnidadMedida,UnidadMedidaName,UnidadMedidaShort,
				CondicionName,PathCodicion,
				IdTipoPuntoMedicion,TipoPuntoMedicionName
			FROM test
			WHERE id BETWEEN @Cero AND @Dias				
		)
		
		SELECT * FROM res
		ORDER BY 			
			FechaNumerica DESC
	
	
	
END
GO
/****** Object:  StoredProcedure [dbo].[spGetFechaNumericaMinima]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetFechaNumericaMinima]
(
--DECLARE 
	@idPuntoMedicion BIGINT
)
AS 
BEGIN
--SET @idPuntoMedicion=1001
	SELECT
		CAST(SUBSTRING(CAST(MIN(ISNULL(FechaNumerica,0)) AS NVARCHAR(50)),0,(LEN(FechaNumerica)-3)) AS BIGINT) AS FechaNumericaMinima
	FROM dbo.CI_REGISTRO 
		where IdPuntoMedicion=@idPuntoMedicion
	GROUP BY FechaNumerica

END
GO
/****** Object:  StoredProcedure [dbo].[spGetDaysToSync]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Siempre deben de ser valores negativos o cero(De 0 a -N)*/
CREATE PROCEDURE [dbo].[spGetDaysToSync]
(
	@idPuntoMedicion BIGINT
)
AS 
BEGIN
	--DECLARE @idPuntoMedicion BIGINT=1000
	DECLARE @DiaMax NVARCHAR(10)
	DECLARE @DiaMin NVARCHAR(10)
	DECLARE @DateMax DATETIME
	DECLARE @DateMin DATETIME

	SELECT @DiaMax= CAST((MAX(ISNULL(FechaNumerica,0))) AS NVARCHAR(50)) FROM dbo.CI_REGISTRO WHERE IdPuntoMedicion=@idPuntoMedicion AND FechaNumerica IS NOT NULL AND FechaNumerica>0
	SELECT @DiaMin= CAST((MIN(ISNULL(FechaNumerica,0))) AS NVARCHAR(50)) FROM dbo.CI_REGISTRO WHERE IdPuntoMedicion=@idPuntoMedicion AND FechaNumerica IS NOT NULL AND FechaNumerica>0
	
	--SELECT @DiaMax
	--SELECT @DiaMin

	IF @DiaMax >0 AND @DiaMin>0
		BEGIN
		--Obtener la fecha maxima
			DECLARE @Anio NVARCHAR(5)=SUBSTRING(@DiaMax,1,4)
			DECLARE @Mes NVARCHAR(2)=SUBSTRING(@DiaMax,5,2)
			DECLARE @Dia NVARCHAR(2)=SUBSTRING(@DiaMax,7,2)
			DECLARE @Hora NVARCHAR(2)=SUBSTRING(@DiaMax,9,2)
			DECLARE @Minuto NVARCHAR(2)=SUBSTRING(@DiaMax,11,2)								
			SELECT @DateMax=CONVERT(NVARCHAR(50),@Anio+@Mes+@Dia+' '+@Mes+':'+@Dia,120)
			
			--Obtener fecha minima
			SELECT @Anio=SUBSTRING(@DiaMin,1,4)
			SELECT @Mes =SUBSTRING(@DiaMin,5,2)
			SELECT @Dia =SUBSTRING(@DiaMin,7,2)
			SELECT @Hora =SUBSTRING(@DiaMin,9,2)
			SELECT @Minuto =SUBSTRING(@DiaMin,11,2)		
			SELECT @DateMin=CONVERT(NVARCHAR(50),@Anio+@Mes+@Dia+' '+@Mes+':'+@Dia,120)							
			
			--obtener diferencia en dias entre fecha maxima y fecha minima
			SELECT CAST(DATEDIFF(DAY,@DateMin,@DateMax)AS BIGINT)
		END 
	ELSE
		BEGIN
			SELECT CAST(0 AS BIGINT)
		END
END
GO
/****** Object:  StoredProcedure [dbo].[spCommitBulkUpdateCiRegistroConfirmation]    Script Date: 04/29/2014 14:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[spCommitBulkUpdateCiRegistroConfirmation]
AS
BEGIN
	declare @tempTblTable nvarchar(100)
	declare @sqlStmt nvarchar(max)
	DECLARE @Return BIT=0
	
	--===================================
	--Actualizar confirmacion (respuesta del servidor)
	--===================================
	;with baseTmp as (
		select 
			IdPuntoMedicion, FechaNumerica, max(ServerLastModifiedDate) as ServerLastModifiedDate
		FROM TMP_CI_REGISTRO_CONFIRMATION as tmp
		group by IdPuntoMedicion, FechaNumerica
	),distinctTemp as (
		select tmp.*
		FROM TMP_CI_REGISTRO_CONFIRMATION as tmp
			inner join baseTmp as base
				on base.IdPuntoMedicion=tmp.IdPuntoMedicion
				and base.FechaNumerica=tmp.FechaNumerica
				and base.ServerLastModifiedDate=tmp.ServerLastModifiedDate
	)

	--Update a los registros que sean diferentes
	UPDATE cr SET
		cr.IsModified=0,
		cr.ServerLastModifiedDate=tmp.ServerLastModifiedDate
	FROM CI_REGISTRO as cr
		INNER JOIN distinctTemp as tmp
		on cr.FechaNumerica=tmp.FechaNumerica
		and cr.IdPuntoMedicion=tmp.IdPuntoMedicion
		and cr.LastModifiedDate<=tmp.LastModifiedDate
		
	select @Return=case when @@rowcount>0 then 1 else 0 end; 
	
	SELECT @Return
	print @sqlStmt
END


/***
--Test de stored procedure
exec [spCommitBulkUpdateCiRegistroConfirmation] 20130515135610664

--Generar columnas de set
select
	'cr.'+column_name+'=tmp.'+column_name+','
from information_schema.columns
where table_name like 'ci_registro'
**/
GO
