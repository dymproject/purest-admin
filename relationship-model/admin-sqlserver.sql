/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2012                    */
/* Created on:     2024/5/20 9:46:44                            */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PUREST_DICT_DATA') and o.name = 'FK_PUREST_D_REFERENCE_PUREST_D')
alter table PUREST_DICT_DATA
   drop constraint FK_PUREST_D_REFERENCE_PUREST_D
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PUREST_FUNCTION_INTERFACE') and o.name = 'FK_PUREST_F_REFERENCE_PUREST_F')
alter table PUREST_FUNCTION_INTERFACE
   drop constraint FK_PUREST_F_REFERENCE_PUREST_F
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PUREST_FUNCTION_INTERFACE') and o.name = 'FK_PUREST_F_REFERENCE_PUREST_I')
alter table PUREST_FUNCTION_INTERFACE
   drop constraint FK_PUREST_F_REFERENCE_PUREST_I
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PUREST_INTERFACE') and o.name = 'FK_PUREST_I_REFERENCE_PUREST_I')
alter table PUREST_INTERFACE
   drop constraint FK_PUREST_I_REFERENCE_PUREST_I
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PUREST_NOTICE_RECORD') and o.name = 'FK_PUREST_N_REFERENCE_PUREST_N')
alter table PUREST_NOTICE_RECORD
   drop constraint FK_PUREST_N_REFERENCE_PUREST_N
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PUREST_ORGANIZATION') and o.name = 'FK_PUREST_O_REFERENCE_PUREST_O')
alter table PUREST_ORGANIZATION
   drop constraint FK_PUREST_O_REFERENCE_PUREST_O
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PUREST_ROLE_FUNCTION') and o.name = 'FK_PUREST_R_REFERENCE_PUREST_R')
alter table PUREST_ROLE_FUNCTION
   drop constraint FK_PUREST_R_REFERENCE_PUREST_R
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PUREST_ROLE_FUNCTION') and o.name = 'FK_PUREST_R_REFERENCE_PUREST_F')
alter table PUREST_ROLE_FUNCTION
   drop constraint FK_PUREST_R_REFERENCE_PUREST_F
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PUREST_USER') and o.name = 'FK_PUREST_U_REFERENCE_PUREST_O')
alter table PUREST_USER
   drop constraint FK_PUREST_U_REFERENCE_PUREST_O
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PUREST_USER_ROLE') and o.name = 'FK_PUREST_U_REFERENCE_PUREST_R')
alter table PUREST_USER_ROLE
   drop constraint FK_PUREST_U_REFERENCE_PUREST_R
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PUREST_USER_ROLE') and o.name = 'FK_PUREST_U_REFERENCE_PUREST_U')
alter table PUREST_USER_ROLE
   drop constraint FK_PUREST_U_REFERENCE_PUREST_U
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_BACKGROUND_JOB_RECORD')
            and   type = 'U')
   drop table PUREST_BACKGROUND_JOB_RECORD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_DICT_CATEGORY')
            and   type = 'U')
   drop table PUREST_DICT_CATEGORY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_DICT_DATA')
            and   type = 'U')
   drop table PUREST_DICT_DATA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_FILE_RECORD')
            and   type = 'U')
   drop table PUREST_FILE_RECORD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_FUNCTION')
            and   type = 'U')
   drop table PUREST_FUNCTION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_FUNCTION_INTERFACE')
            and   type = 'U')
   drop table PUREST_FUNCTION_INTERFACE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_INTERFACE')
            and   type = 'U')
   drop table PUREST_INTERFACE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_INTERFACE_GROUP')
            and   type = 'U')
   drop table PUREST_INTERFACE_GROUP
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_NOTICE')
            and   type = 'U')
   drop table PUREST_NOTICE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_NOTICE_RECORD')
            and   type = 'U')
   drop table PUREST_NOTICE_RECORD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_ORGANIZATION')
            and   type = 'U')
   drop table PUREST_ORGANIZATION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_REQUEST_LOG')
            and   type = 'U')
   drop table PUREST_REQUEST_LOG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_ROLE')
            and   type = 'U')
   drop table PUREST_ROLE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_ROLE_FUNCTION')
            and   type = 'U')
   drop table PUREST_ROLE_FUNCTION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_SYSTEM_CONFIG')
            and   type = 'U')
   drop table PUREST_SYSTEM_CONFIG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_USER')
            and   type = 'U')
   drop table PUREST_USER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUREST_USER_ROLE')
            and   type = 'U')
   drop table PUREST_USER_ROLE
go

/*==============================================================*/
/* Table: PUREST_BACKGROUND_JOB_RECORD                          */
/*==============================================================*/
create table PUREST_BACKGROUND_JOB_RECORD (
   ID                   varchar(40)          not null,
   JOB_NAME             varchar(128)         not null,
   JOB_ARGS             text                 not null,
   TRY_COUNT            numeric              null,
   CREATION_TIME        datetime             null,
   NEXT_TRY_TIME        datetime             null,
   LAST_TRY_TIME        datetime             null,
   IS_ABANDONED         numeric(1)           null,
   PRIORITY             numeric              null,
   constraint PK_PUREST_BACKGROUND_JOB_RECOR primary key nonclustered (ID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_BACKGROUND_JOB_RECORD') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '后台作业记录表', 
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_BACKGROUND_JOB_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_BACKGROUND_JOB_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'JOB_NAME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'JOB_NAME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '名称',
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'JOB_NAME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_BACKGROUND_JOB_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'JOB_ARGS')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'JOB_ARGS'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '参数',
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'JOB_ARGS'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_BACKGROUND_JOB_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TRY_COUNT')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'TRY_COUNT'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '重试次数',
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'TRY_COUNT'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_BACKGROUND_JOB_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATION_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'CREATION_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'CREATION_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_BACKGROUND_JOB_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NEXT_TRY_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'NEXT_TRY_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '下次执行时间',
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'NEXT_TRY_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_BACKGROUND_JOB_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LAST_TRY_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'LAST_TRY_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后执行时间',
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'LAST_TRY_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_BACKGROUND_JOB_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IS_ABANDONED')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'IS_ABANDONED'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否超时',
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'IS_ABANDONED'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_BACKGROUND_JOB_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PRIORITY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'PRIORITY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '优先级',
   'user', @CurrentUser, 'table', 'PUREST_BACKGROUND_JOB_RECORD', 'column', 'PRIORITY'
go

/*==============================================================*/
/* Table: PUREST_DICT_CATEGORY                                  */
/*==============================================================*/
create table PUREST_DICT_CATEGORY (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   NAME                 varchar(20)          not null,
   CODE                 varchar(40)          not null,
   constraint PK_PUREST_DICT_CATEGORY primary key nonclustered (ID),
   constraint UK_PUREST_CATEGORY_CODE unique (CODE)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_DICT_CATEGORY') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '字典分类', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_CATEGORY')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_CATEGORY')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_CATEGORY')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_CATEGORY')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_CATEGORY')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_CATEGORY')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_CATEGORY')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NAME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'NAME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分类名称',
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'NAME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_CATEGORY')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CODE')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'CODE'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分类编码',
   'user', @CurrentUser, 'table', 'PUREST_DICT_CATEGORY', 'column', 'CODE'
go

/*==============================================================*/
/* Table: PUREST_DICT_DATA                                      */
/*==============================================================*/
create table PUREST_DICT_DATA (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   CATEGORY_ID          numeric(19,0)        not null,
   NAME                 varchar(20)          not null,
   SORT                 numeric(8,0)         not null,
   constraint PK_PUREST_DICT_DATA primary key nonclustered (ID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_DICT_DATA') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '字典数据', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_DATA')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_DATA')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_DATA')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_DATA')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_DATA')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_DATA')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_DATA')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CATEGORY_ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'CATEGORY_ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '字典分类ID',
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'CATEGORY_ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_DATA')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NAME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'NAME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '字典名称',
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'NAME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_DICT_DATA')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SORT')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'SORT'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '排序',
   'user', @CurrentUser, 'table', 'PUREST_DICT_DATA', 'column', 'SORT'
go

/*==============================================================*/
/* Table: PUREST_FILE_RECORD                                    */
/*==============================================================*/
create table PUREST_FILE_RECORD (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   FILE_NAME            varchar(1000)        not null,
   FILE_SIZE            numeric(10)          not null,
   FILE_EXT             varchar(10)          not null,
   FULL_PATH            varchar(1000)        not null,
   constraint PK_PUREST_FILE_RECORD primary key nonclustered (ID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_FILE_RECORD') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '文件上传记录表', 
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FILE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Id',
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FILE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FILE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FILE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FILE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FILE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FILE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FILE_NAME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'FILE_NAME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件名',
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'FILE_NAME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FILE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FILE_SIZE')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'FILE_SIZE'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件大小',
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'FILE_SIZE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FILE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FILE_EXT')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'FILE_EXT'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件扩展名',
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'FILE_EXT'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FILE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FULL_PATH')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'FULL_PATH'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '完整路径',
   'user', @CurrentUser, 'table', 'PUREST_FILE_RECORD', 'column', 'FULL_PATH'
go

/*==============================================================*/
/* Table: PUREST_FUNCTION                                       */
/*==============================================================*/
create table PUREST_FUNCTION (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   NAME                 varchar(20)          not null,
   CODE                 varchar(40)          not null,
   PARENT_ID            numeric(19,0)        null,
   constraint PK_PUREST_FUNCTION primary key nonclustered (ID),
   constraint UK_PUREST_FUNCTION_CODE unique (CODE)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_FUNCTION') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '功能表', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NAME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'NAME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '名称',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'NAME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CODE')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'CODE'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '编码',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'CODE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PARENT_ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'PARENT_ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '隶属于',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION', 'column', 'PARENT_ID'
go

/*==============================================================*/
/* Table: PUREST_FUNCTION_INTERFACE                             */
/*==============================================================*/
create table PUREST_FUNCTION_INTERFACE (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   INTERFACE_ID         numeric(19,0)        null,
   FUNCTION_ID          numeric(19,0)        null,
   constraint PK_PUREST_FUNCTION_INTERFACE primary key nonclustered (ID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_FUNCTION_INTERFACE') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '页面接口表', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'INTERFACE_ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'INTERFACE_ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '接口ID',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'INTERFACE_ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_FUNCTION_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FUNCTION_ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'FUNCTION_ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '功能ID',
   'user', @CurrentUser, 'table', 'PUREST_FUNCTION_INTERFACE', 'column', 'FUNCTION_ID'
go

/*==============================================================*/
/* Table: PUREST_INTERFACE                                      */
/*==============================================================*/
create table PUREST_INTERFACE (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   NAME                 varchar(20)          not null,
   PATH                 varchar(200)         not null,
   REQUEST_METHOD       varchar(20)          null,
   GROUP_ID             numeric(19,0)        null,
   constraint PK_PUREST_INTERFACE primary key nonclustered (ID),
   constraint UK_PUREST_INTERFACE_PR unique (PATH, REQUEST_METHOD)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_INTERFACE') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '接口表', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NAME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'NAME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '接口名称',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'NAME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PATH')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'PATH'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '接口地址',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'PATH'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REQUEST_METHOD')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'REQUEST_METHOD'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '请求方法',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'REQUEST_METHOD'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'GROUP_ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'GROUP_ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '接口分组ID',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE', 'column', 'GROUP_ID'
go

/*==============================================================*/
/* Table: PUREST_INTERFACE_GROUP                                */
/*==============================================================*/
create table PUREST_INTERFACE_GROUP (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   NAME                 varchar(20)          null,
   CODE                 varchar(40)          not null,
   constraint PK_PUREST_INTERFACE_GROUP primary key nonclustered (ID),
   constraint UK_PUREST_IG_CODE unique (CODE)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_INTERFACE_GROUP') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '接口分组表', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE_GROUP')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE_GROUP')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE_GROUP')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE_GROUP')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE_GROUP')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE_GROUP')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE_GROUP')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NAME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'NAME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '名称',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'NAME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_INTERFACE_GROUP')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CODE')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'CODE'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '编码',
   'user', @CurrentUser, 'table', 'PUREST_INTERFACE_GROUP', 'column', 'CODE'
go

/*==============================================================*/
/* Table: PUREST_NOTICE                                         */
/*==============================================================*/
create table PUREST_NOTICE (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   TITLE                varchar(40)          not null,
   CONTENT              text                 null,
   NOTICE_TYPE          numeric(19,0)        not null,
   LEVEL                numeric(19,0)        null,
   constraint PK_PUREST_NOTICE primary key nonclustered (ID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_NOTICE') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_NOTICE' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '通知公告表', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TITLE')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'TITLE'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主题',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'TITLE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CONTENT')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'CONTENT'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '内容',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'CONTENT'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NOTICE_TYPE')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'NOTICE_TYPE'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '类型',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'NOTICE_TYPE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LEVEL')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'LEVEL'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '级别',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE', 'column', 'LEVEL'
go

/*==============================================================*/
/* Table: PUREST_NOTICE_RECORD                                  */
/*==============================================================*/
create table PUREST_NOTICE_RECORD (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   RECEIVER             numeric(19,0)        not null,
   IS_READ              numeric(1)           not null,
   NOTICE_ID            numeric(19,0)        not null,
   constraint PK_PUREST_NOTICE_RECORD primary key nonclustered (ID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_NOTICE_RECORD') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '通知公告记录表', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RECEIVER')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'RECEIVER'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '接收人',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'RECEIVER'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IS_READ')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'IS_READ'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否已读',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'IS_READ'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_NOTICE_RECORD')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NOTICE_ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'NOTICE_ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '通知公告Id',
   'user', @CurrentUser, 'table', 'PUREST_NOTICE_RECORD', 'column', 'NOTICE_ID'
go

/*==============================================================*/
/* Table: PUREST_ORGANIZATION                                   */
/*==============================================================*/
create table PUREST_ORGANIZATION (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   NAME                 varchar(100)         not null,
   PARENT_ID            numeric(19,0)        null,
   TELEPHONE            varchar(20)          null,
   LEADER               varchar(20)          null,
   SORT                 int                  null,
   constraint PK_PUREST_ORGANIZATION primary key nonclustered (ID),
   constraint UK_PUREST_ORG_NAME_PID unique (NAME, PARENT_ID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_ORGANIZATION') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '组织机构', 
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ORGANIZATION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ORGANIZATION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ORGANIZATION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ORGANIZATION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ORGANIZATION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ORGANIZATION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ORGANIZATION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NAME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'NAME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '名称',
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'NAME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ORGANIZATION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PARENT_ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'PARENT_ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '父级Id',
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'PARENT_ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ORGANIZATION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TELEPHONE')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'TELEPHONE'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '联系电话',
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'TELEPHONE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ORGANIZATION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LEADER')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'LEADER'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '负责人',
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'LEADER'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ORGANIZATION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'SORT')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'SORT'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '排序',
   'user', @CurrentUser, 'table', 'PUREST_ORGANIZATION', 'column', 'SORT'
go

/*==============================================================*/
/* Table: PUREST_REQUEST_LOG                                    */
/*==============================================================*/
create table PUREST_REQUEST_LOG (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   CONTROLLER_NAME      varchar(100)         null,
   ACTION_NAME          varchar(100)         null,
   REQUEST_METHOD       varchar(10)          null,
   ENVIRONMENT_NAME     varchar(20)          null,
   ELAPSED_TIME         numeric(16,0)        null,
   CLIENT_IP            varchar(20)          null,
   constraint PK_PUREST_REQUEST_LOG primary key nonclustered (ID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_REQUEST_LOG') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '请求日志表', 
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_REQUEST_LOG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_REQUEST_LOG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_REQUEST_LOG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_REQUEST_LOG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_REQUEST_LOG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_REQUEST_LOG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_REQUEST_LOG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CONTROLLER_NAME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'CONTROLLER_NAME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '控制器',
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'CONTROLLER_NAME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_REQUEST_LOG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ACTION_NAME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'ACTION_NAME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '方法名',
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'ACTION_NAME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_REQUEST_LOG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REQUEST_METHOD')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'REQUEST_METHOD'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '请求类型',
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'REQUEST_METHOD'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_REQUEST_LOG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ENVIRONMENT_NAME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'ENVIRONMENT_NAME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '服务器环境',
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'ENVIRONMENT_NAME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_REQUEST_LOG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ELAPSED_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'ELAPSED_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '执行耗时',
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'ELAPSED_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_REQUEST_LOG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CLIENT_IP')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'CLIENT_IP'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客户端IP',
   'user', @CurrentUser, 'table', 'PUREST_REQUEST_LOG', 'column', 'CLIENT_IP'
go

/*==============================================================*/
/* Table: PUREST_ROLE                                           */
/*==============================================================*/
create table PUREST_ROLE (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   NAME                 varchar(20)          not null,
   DESCRIPTION          varchar(200)         null,
   constraint PK_PUREST_ROLE primary key nonclustered (ID),
   constraint UK_PUREST_ROLE_NAME unique (NAME)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_ROLE') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_ROLE' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '角色', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NAME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'NAME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色名称',
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'NAME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DESCRIPTION')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'DESCRIPTION'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色描述',
   'user', @CurrentUser, 'table', 'PUREST_ROLE', 'column', 'DESCRIPTION'
go

/*==============================================================*/
/* Table: PUREST_ROLE_FUNCTION                                  */
/*==============================================================*/
create table PUREST_ROLE_FUNCTION (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   ROLE_ID              numeric(19,0)        null,
   FUNCTION_ID          numeric(19,0)        null,
   constraint PK_PUREST_ROLE_FUNCTION primary key nonclustered (ID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_ROLE_FUNCTION') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '角色功能表', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ROLE_ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'ROLE_ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色ID',
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'ROLE_ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_ROLE_FUNCTION')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FUNCTION_ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'FUNCTION_ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '功能ID',
   'user', @CurrentUser, 'table', 'PUREST_ROLE_FUNCTION', 'column', 'FUNCTION_ID'
go

/*==============================================================*/
/* Table: PUREST_SYSTEM_CONFIG                                  */
/*==============================================================*/
create table PUREST_SYSTEM_CONFIG (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   NAME                 varchar(20)          null,
   CONFIG_CODE          varchar(40)          null,
   CONFIG_VALUE         varchar(1000)        null,
   constraint PK_PUREST_SYSTEM_CONFIG primary key nonclustered (ID),
   constraint UK_PUREST_CONFIG_CODE unique (CONFIG_CODE)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_SYSTEM_CONFIG') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '系统配置表', 
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_SYSTEM_CONFIG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_SYSTEM_CONFIG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_SYSTEM_CONFIG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_SYSTEM_CONFIG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_SYSTEM_CONFIG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_SYSTEM_CONFIG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_SYSTEM_CONFIG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NAME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'NAME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '名称',
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'NAME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_SYSTEM_CONFIG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CONFIG_CODE')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'CONFIG_CODE'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '编码',
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'CONFIG_CODE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_SYSTEM_CONFIG')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CONFIG_VALUE')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'CONFIG_VALUE'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '值',
   'user', @CurrentUser, 'table', 'PUREST_SYSTEM_CONFIG', 'column', 'CONFIG_VALUE'
go

/*==============================================================*/
/* Table: PUREST_USER                                           */
/*==============================================================*/
create table PUREST_USER (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   ACCOUNT              varchar(36)          not null,
   PASSWORD             varchar(100)         not null,
   NAME                 varchar(20)          not null,
   TELEPHONE            varchar(11)          null,
   EMAIL                varchar(20)          null,
   AVATAR               binary(1)            null,
   STATUS               numeric              null,
   ORGANIZATION_ID      numeric(19,0)        not null,
   constraint PK_PUREST_USER primary key nonclustered (ID),
   constraint UK_PUREST_USER_ACCOUNT unique (ACCOUNT)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_USER') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_USER' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '用户', 
   'user', @CurrentUser, 'table', 'PUREST_USER'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ACCOUNT')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'ACCOUNT'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '账号',
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'ACCOUNT'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PASSWORD')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'PASSWORD'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '密码',
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'PASSWORD'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'NAME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'NAME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '真实姓名',
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'NAME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'TELEPHONE')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'TELEPHONE'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电话',
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'TELEPHONE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'EMAIL')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'EMAIL'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '邮箱',
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'EMAIL'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'AVATAR')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'AVATAR'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '头像',
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'AVATAR'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'STATUS')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'STATUS'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '状态',
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'STATUS'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ORGANIZATION_ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'ORGANIZATION_ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '组织机构Id',
   'user', @CurrentUser, 'table', 'PUREST_USER', 'column', 'ORGANIZATION_ID'
go

/*==============================================================*/
/* Table: PUREST_USER_ROLE                                      */
/*==============================================================*/
create table PUREST_USER_ROLE (
   ID                   numeric(19,0)        not null,
   CREATE_BY            numeric(19,0)        not null,
   CREATE_TIME          datetime             not null,
   UPDATE_BY            numeric(19,0)        null,
   UPDATE_TIME          datetime             null,
   REMARK               varchar(1000)        null,
   ROLE_ID              numeric(19,0)        not null,
   USER_ID              numeric(19,0)        not null,
   constraint PK_PUREST_USER_ROLE primary key nonclustered (ID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PUREST_USER_ROLE') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '用户角色', 
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键Id',
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'CREATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'CREATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CREATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'CREATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'CREATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_BY')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'UPDATE_BY'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改人',
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'UPDATE_BY'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UPDATE_TIME')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'UPDATE_TIME'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修改时间',
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'UPDATE_TIME'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'REMARK')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'REMARK'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'REMARK'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ROLE_ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'ROLE_ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色ID',
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'ROLE_ID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PUREST_USER_ROLE')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'USER_ID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'USER_ID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户ID',
   'user', @CurrentUser, 'table', 'PUREST_USER_ROLE', 'column', 'USER_ID'
go

alter table PUREST_DICT_DATA
   add constraint FK_PUREST_D_REFERENCE_PUREST_D foreign key (CATEGORY_ID)
      references PUREST_DICT_CATEGORY (ID)
go

alter table PUREST_FUNCTION_INTERFACE
   add constraint FK_PUREST_F_REFERENCE_PUREST_F foreign key (FUNCTION_ID)
      references PUREST_FUNCTION (ID)
go

alter table PUREST_FUNCTION_INTERFACE
   add constraint FK_PUREST_F_REFERENCE_PUREST_I foreign key (INTERFACE_ID)
      references PUREST_INTERFACE (ID)
go

alter table PUREST_INTERFACE
   add constraint FK_PUREST_I_REFERENCE_PUREST_I foreign key (GROUP_ID)
      references PUREST_INTERFACE_GROUP (ID)
go

alter table PUREST_NOTICE_RECORD
   add constraint FK_PUREST_N_REFERENCE_PUREST_N foreign key (NOTICE_ID)
      references PUREST_NOTICE (ID)
go

alter table PUREST_ORGANIZATION
   add constraint FK_PUREST_O_REFERENCE_PUREST_O foreign key (PARENT_ID)
      references PUREST_ORGANIZATION (ID)
go

alter table PUREST_ROLE_FUNCTION
   add constraint FK_PUREST_R_REFERENCE_PUREST_R foreign key (ROLE_ID)
      references PUREST_ROLE (ID)
go

alter table PUREST_ROLE_FUNCTION
   add constraint FK_PUREST_R_REFERENCE_PUREST_F foreign key (FUNCTION_ID)
      references PUREST_FUNCTION (ID)
go

alter table PUREST_USER
   add constraint FK_PUREST_U_REFERENCE_PUREST_O foreign key (ORGANIZATION_ID)
      references PUREST_ORGANIZATION (ID)
go

alter table PUREST_USER_ROLE
   add constraint FK_PUREST_U_REFERENCE_PUREST_R foreign key (ROLE_ID)
      references PUREST_ROLE (ID)
go

alter table PUREST_USER_ROLE
   add constraint FK_PUREST_U_REFERENCE_PUREST_U foreign key (USER_ID)
      references PUREST_USER (ID)
go

