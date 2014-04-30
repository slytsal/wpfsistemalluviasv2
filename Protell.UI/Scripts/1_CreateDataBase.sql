USE [master]
GO


IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'db_SeguimientoProtocolo_r2_v2')
DROP DATABASE db_SeguimientoProtocolo_r2_v2
GO
CREATE DATABASE db_SeguimientoProtocolo_r2_v2


