/*==============================================================*/
/* DBMS name:      ORACLE Version 11g                           */
/* Created on:     2024/9/11 9:36:32                            */
/*==============================================================*/
alter table PUREST_DICT_DATA
   drop constraint FK_PUREST_D_REFERENCE_PUREST_D;

alter table PUREST_FUNCTION_INTERFACE
   drop constraint FK_PUREST_F_REFERENCE_PUREST_F;

alter table PUREST_FUNCTION_INTERFACE
   drop constraint FK_PUREST_F_REFERENCE_PUREST_I;

alter table PUREST_INTERFACE
   drop constraint FK_PUREST_I_REFERENCE_PUREST_I;

alter table PUREST_NOTICE_RECORD
   drop constraint FK_PUREST_N_REFERENCE_PUREST_N;

alter table PUREST_OAUTH2_USER
   drop constraint FK_PUREST_O_REFERENCE_PUREST_U;

alter table PUREST_PROFILE_SYSTEM
   drop constraint FK_PUREST_P_REFERENCE_PUREST_F;

alter table PUREST_ROLE_FUNCTION
   drop constraint FK_PUREST_R_REFERENCE_PUREST_R;

alter table PUREST_ROLE_FUNCTION
   drop constraint FK_PUREST_R_REFERENCE_PUREST_F;

alter table PUREST_USER
   drop constraint FK_PUREST_U_REFERENCE_PUREST_O;

alter table PUREST_USER_ROLE
   drop constraint FK_PUREST_U_REFERENCE_PUREST_R;

alter table PUREST_USER_ROLE
   drop constraint FK_PUREST_U_REFERENCE_PUREST_U;

alter table PUREST_WF_EXECUTION_ATTRIBUTE
   drop constraint "FK_Reference_20";

alter table PUREST_WF_EXECUTION_POINTER
   drop constraint "FK_Reference_19";

alter table PUREST_WORKFLOW_INSTANCE
   drop constraint "FK_Reference_23";

alter table PUREST_WORKFLOW_INSTANCE
   drop constraint "FK_Reference_24";

drop table PUREST_BACKGROUND_JOB_RECORD cascade constraints;

drop table PUREST_DICT_CATEGORY cascade constraints;

drop table PUREST_DICT_DATA cascade constraints;

drop table PUREST_FILE_RECORD cascade constraints;

drop table PUREST_FUNCTION cascade constraints;

drop table PUREST_FUNCTION_INTERFACE cascade constraints;

drop table PUREST_INTERFACE cascade constraints;

drop table PUREST_INTERFACE_GROUP cascade constraints;

drop table PUREST_NOTICE cascade constraints;

drop table PUREST_NOTICE_RECORD cascade constraints;

drop table PUREST_OAUTH2_USER cascade constraints;

drop table PUREST_ORGANIZATION cascade constraints;

drop table PUREST_PROFILE_SYSTEM cascade constraints;

drop table PUREST_REQUEST_LOG cascade constraints;

drop table PUREST_ROLE cascade constraints;

drop table PUREST_ROLE_FUNCTION cascade constraints;

drop table PUREST_SYSTEM_CONFIG cascade constraints;

drop table PUREST_USER cascade constraints;

drop table PUREST_USER_ROLE cascade constraints;

drop table PUREST_WF_AUDITING_RECORD cascade constraints;

drop table PUREST_WF_DEFINITION cascade constraints;

drop index "IX_Event_EventName_EventKey";

drop index "IX_Event_IsProcessed";

drop index "IX_Event_EventTime";

drop index "IX_Event_EventId";

drop table PUREST_WF_EVENT cascade constraints;

drop index "IX_ExecutionPointerId";

drop table PUREST_WF_EXECUTION_ATTRIBUTE cascade constraints;

drop table PUREST_WF_EXECUTION_ERROR cascade constraints;

drop index "IX_ExecutionPointer_WorkflowId";

drop table PUREST_WF_EXECUTION_POINTER cascade constraints;

drop index "IX_ExecuteTime";

drop index "IX_CommandName_Data";

drop table PUREST_WF_SCHEDULED_COMMAND cascade constraints;

drop index "IX_Subscription_EventName";

drop index "IX_Subscription_EventKey";

drop index "IX_Subscription_SubscriptionId";

drop table PUREST_WF_SUBSCRIPTION cascade constraints;

drop table PUREST_WF_WAITING_POINTER cascade constraints;

drop index "IX_Workflow_NextExecution";

drop index "IX_Workflow_InstanceId";

drop table PUREST_WF_WORKFLOW cascade constraints;

drop table PUREST_WORKFLOW_INSTANCE cascade constraints;

/*==============================================================*/
/* Table: PUREST_BACKGROUND_JOB_RECORD                          */
/*==============================================================*/
create table PUREST_BACKGROUND_JOB_RECORD 
(
   ID                   VARCHAR2(40)         not null,
   JOB_NAME             VARCHAR2(128)        not null,
   JOB_ARGS             CLOB                 not null,
   TRY_COUNT            NUMBER,
   CREATION_TIME        DATETIME,
   NEXT_TRY_TIME        DATETIME,
   LAST_TRY_TIME        DATETIME,
   IS_ABANDONED         NUMBER(1),
   PRIORITY             NUMBER,
   constraint PK_PUREST_BACKGROUND_JOB_RECOR primary key (ID)
);

comment on table PUREST_BACKGROUND_JOB_RECORD is
'后台作业记录表';

comment on column PUREST_BACKGROUND_JOB_RECORD.ID is
'Id';

comment on column PUREST_BACKGROUND_JOB_RECORD.JOB_NAME is
'名称';

comment on column PUREST_BACKGROUND_JOB_RECORD.JOB_ARGS is
'参数';

comment on column PUREST_BACKGROUND_JOB_RECORD.TRY_COUNT is
'重试次数';

comment on column PUREST_BACKGROUND_JOB_RECORD.CREATION_TIME is
'创建时间';

comment on column PUREST_BACKGROUND_JOB_RECORD.NEXT_TRY_TIME is
'下次执行时间';

comment on column PUREST_BACKGROUND_JOB_RECORD.LAST_TRY_TIME is
'最后执行时间';

comment on column PUREST_BACKGROUND_JOB_RECORD.IS_ABANDONED is
'是否超时';

comment on column PUREST_BACKGROUND_JOB_RECORD.PRIORITY is
'优先级';

/*==============================================================*/
/* Table: PUREST_DICT_CATEGORY                                  */
/*==============================================================*/
create table PUREST_DICT_CATEGORY 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   NAME                 VARCHAR2(20)         not null,
   CODE                 VARCHAR2(40)         not null,
   constraint PK_PUREST_DICT_CATEGORY primary key (ID),
   constraint UK_PUREST_CATEGORY_CODE unique (CODE)
);

comment on table PUREST_DICT_CATEGORY is
'字典分类';

comment on column PUREST_DICT_CATEGORY.ID is
'主键Id';

comment on column PUREST_DICT_CATEGORY.CREATE_BY is
'创建人';

comment on column PUREST_DICT_CATEGORY.CREATE_TIME is
'创建时间';

comment on column PUREST_DICT_CATEGORY.UPDATE_BY is
'修改人';

comment on column PUREST_DICT_CATEGORY.UPDATE_TIME is
'修改时间';

comment on column PUREST_DICT_CATEGORY.REMARK is
'备注';

comment on column PUREST_DICT_CATEGORY.NAME is
'分类名称';

comment on column PUREST_DICT_CATEGORY.CODE is
'分类编码';

/*==============================================================*/
/* Table: PUREST_DICT_DATA                                      */
/*==============================================================*/
create table PUREST_DICT_DATA 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   CATEGORY_ID          NUMBER(19)              not null,
   NAME                 VARCHAR2(20)         not null,
   SORT                 NUMBER               not null,
   constraint PK_PUREST_DICT_DATA primary key (ID)
);

comment on table PUREST_DICT_DATA is
'字典数据';

comment on column PUREST_DICT_DATA.ID is
'主键Id';

comment on column PUREST_DICT_DATA.CREATE_BY is
'创建人';

comment on column PUREST_DICT_DATA.CREATE_TIME is
'创建时间';

comment on column PUREST_DICT_DATA.UPDATE_BY is
'修改人';

comment on column PUREST_DICT_DATA.UPDATE_TIME is
'修改时间';

comment on column PUREST_DICT_DATA.REMARK is
'备注';

comment on column PUREST_DICT_DATA.CATEGORY_ID is
'字典分类ID';

comment on column PUREST_DICT_DATA.NAME is
'字典名称';

comment on column PUREST_DICT_DATA.SORT is
'排序';

/*==============================================================*/
/* Table: PUREST_FILE_RECORD                                    */
/*==============================================================*/
create table PUREST_FILE_RECORD 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   FILE_NAME            VARCHAR2(100)        not null,
   FILE_SIZE            NUMBER(10)           not null,
   FILE_EXT             VARCHAR2(10)         not null,
   constraint PK_PUREST_FILE_RECORD primary key (ID)
);

comment on table PUREST_FILE_RECORD is
'文件上传记录表';

comment on column PUREST_FILE_RECORD.ID is
'Id';

comment on column PUREST_FILE_RECORD.CREATE_BY is
'创建人';

comment on column PUREST_FILE_RECORD.CREATE_TIME is
'创建时间';

comment on column PUREST_FILE_RECORD.UPDATE_BY is
'修改人';

comment on column PUREST_FILE_RECORD.UPDATE_TIME is
'修改时间';

comment on column PUREST_FILE_RECORD.REMARK is
'备注';

comment on column PUREST_FILE_RECORD.FILE_NAME is
'文件名';

comment on column PUREST_FILE_RECORD.FILE_SIZE is
'文件大小';

comment on column PUREST_FILE_RECORD.FILE_EXT is
'文件扩展名';

/*==============================================================*/
/* Table: PUREST_FUNCTION                                       */
/*==============================================================*/
create table PUREST_FUNCTION 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   NAME                 VARCHAR2(20)         not null,
   CODE                 VARCHAR2(40)         not null,
   PARENT_ID            NUMBER(19),
   constraint PK_PUREST_FUNCTION primary key (ID),
   constraint UK_PUREST_FUNCTION_CODE unique (CODE)
);

comment on table PUREST_FUNCTION is
'功能表';

comment on column PUREST_FUNCTION.ID is
'主键Id';

comment on column PUREST_FUNCTION.CREATE_BY is
'创建人';

comment on column PUREST_FUNCTION.CREATE_TIME is
'创建时间';

comment on column PUREST_FUNCTION.UPDATE_BY is
'修改人';

comment on column PUREST_FUNCTION.UPDATE_TIME is
'修改时间';

comment on column PUREST_FUNCTION.REMARK is
'备注';

comment on column PUREST_FUNCTION.NAME is
'名称';

comment on column PUREST_FUNCTION.CODE is
'编码';

comment on column PUREST_FUNCTION.PARENT_ID is
'隶属于';

/*==============================================================*/
/* Table: PUREST_FUNCTION_INTERFACE                             */
/*==============================================================*/
create table PUREST_FUNCTION_INTERFACE 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   INTERFACE_ID         NUMBER(19),
   FUNCTION_ID          NUMBER(19),
   constraint PK_PUREST_FUNCTION_INTERFACE primary key (ID)
);

comment on table PUREST_FUNCTION_INTERFACE is
'页面接口表';

comment on column PUREST_FUNCTION_INTERFACE.ID is
'主键Id';

comment on column PUREST_FUNCTION_INTERFACE.CREATE_BY is
'创建人';

comment on column PUREST_FUNCTION_INTERFACE.CREATE_TIME is
'创建时间';

comment on column PUREST_FUNCTION_INTERFACE.UPDATE_BY is
'修改人';

comment on column PUREST_FUNCTION_INTERFACE.UPDATE_TIME is
'修改时间';

comment on column PUREST_FUNCTION_INTERFACE.REMARK is
'备注';

comment on column PUREST_FUNCTION_INTERFACE.INTERFACE_ID is
'接口ID';

comment on column PUREST_FUNCTION_INTERFACE.FUNCTION_ID is
'功能ID';

/*==============================================================*/
/* Table: PUREST_INTERFACE                                      */
/*==============================================================*/
create table PUREST_INTERFACE 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   NAME                 VARCHAR2(20)         not null,
   PATH                 VARCHAR2(200)        not null,
   REQUEST_METHOD       VARCHAR2(20)         not null,
   GROUP_ID             NUMBER(19),
   constraint PK_PUREST_INTERFACE primary key (ID),
   constraint UK_INTERFACE_PATHMETHOD unique (PATH, REQUEST_METHOD)
);

comment on table PUREST_INTERFACE is
'接口表';

comment on column PUREST_INTERFACE.ID is
'主键Id';

comment on column PUREST_INTERFACE.CREATE_BY is
'创建人';

comment on column PUREST_INTERFACE.CREATE_TIME is
'创建时间';

comment on column PUREST_INTERFACE.UPDATE_BY is
'修改人';

comment on column PUREST_INTERFACE.UPDATE_TIME is
'修改时间';

comment on column PUREST_INTERFACE.REMARK is
'备注';

comment on column PUREST_INTERFACE.NAME is
'接口名称';

comment on column PUREST_INTERFACE.PATH is
'接口地址';

comment on column PUREST_INTERFACE.REQUEST_METHOD is
'请求方法';

comment on column PUREST_INTERFACE.GROUP_ID is
'接口分组ID';

/*==============================================================*/
/* Table: PUREST_INTERFACE_GROUP                                */
/*==============================================================*/
create table PUREST_INTERFACE_GROUP 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   NAME                 VARCHAR2(20),
   CODE                 VARCHAR2(40)         not null,
   constraint PK_PUREST_INTERFACE_GROUP primary key (ID),
   constraint UK_PUREST_IG_CODE unique (CODE)
);

comment on table PUREST_INTERFACE_GROUP is
'接口分组表';

comment on column PUREST_INTERFACE_GROUP.ID is
'主键Id';

comment on column PUREST_INTERFACE_GROUP.CREATE_BY is
'创建人';

comment on column PUREST_INTERFACE_GROUP.CREATE_TIME is
'创建时间';

comment on column PUREST_INTERFACE_GROUP.UPDATE_BY is
'修改人';

comment on column PUREST_INTERFACE_GROUP.UPDATE_TIME is
'修改时间';

comment on column PUREST_INTERFACE_GROUP.REMARK is
'备注';

comment on column PUREST_INTERFACE_GROUP.NAME is
'名称';

comment on column PUREST_INTERFACE_GROUP.CODE is
'编码';

/*==============================================================*/
/* Table: PUREST_NOTICE                                         */
/*==============================================================*/
create table PUREST_NOTICE 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   TITLE                VARCHAR2(40)         not null,
   CONTENT              CLOB,
   NOTICE_TYPE          NUMBER(19)              not null,
   "LEVEL"              NUMBER(19),
   constraint PK_PUREST_NOTICE primary key (ID)
);

comment on table PUREST_NOTICE is
'通知公告表';

comment on column PUREST_NOTICE.ID is
'主键Id';

comment on column PUREST_NOTICE.CREATE_BY is
'创建人';

comment on column PUREST_NOTICE.CREATE_TIME is
'创建时间';

comment on column PUREST_NOTICE.UPDATE_BY is
'修改人';

comment on column PUREST_NOTICE.UPDATE_TIME is
'修改时间';

comment on column PUREST_NOTICE.REMARK is
'备注';

comment on column PUREST_NOTICE.TITLE is
'主题';

comment on column PUREST_NOTICE.CONTENT is
'内容';

comment on column PUREST_NOTICE.NOTICE_TYPE is
'类型';

comment on column PUREST_NOTICE."LEVEL" is
'级别';

/*==============================================================*/
/* Table: PUREST_NOTICE_RECORD                                  */
/*==============================================================*/
create table PUREST_NOTICE_RECORD 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   RECEIVER             NUMBER(19)              not null,
   IS_READ              NUMBER(1)            not null,
   NOTICE_ID            NUMBER(19)              not null,
   constraint PK_PUREST_NOTICE_RECORD primary key (ID)
);

comment on table PUREST_NOTICE_RECORD is
'通知公告记录表';

comment on column PUREST_NOTICE_RECORD.ID is
'主键Id';

comment on column PUREST_NOTICE_RECORD.CREATE_BY is
'创建人';

comment on column PUREST_NOTICE_RECORD.CREATE_TIME is
'创建时间';

comment on column PUREST_NOTICE_RECORD.UPDATE_BY is
'修改人';

comment on column PUREST_NOTICE_RECORD.UPDATE_TIME is
'修改时间';

comment on column PUREST_NOTICE_RECORD.REMARK is
'备注';

comment on column PUREST_NOTICE_RECORD.RECEIVER is
'接收人';

comment on column PUREST_NOTICE_RECORD.IS_READ is
'是否已读';

comment on column PUREST_NOTICE_RECORD.NOTICE_ID is
'通知公告Id';

/*==============================================================*/
/* Table: PUREST_OAUTH2_USER                                    */
/*==============================================================*/
create table PUREST_OAUTH2_USER 
(
   PERSISTENCE_ID       NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   ID                   NUMBER(19),
   NAME                 VARCHAR2(20),
   TYPE                 VARCHAR2(20)         not null,
   USER_ID              NUMBER(19),
   constraint PK_PUREST_OAUTH2_USER primary key (PERSISTENCE_ID)
);

comment on table PUREST_OAUTH2_USER is
'OAUTH2用户';

comment on column PUREST_OAUTH2_USER.PERSISTENCE_ID is
'主键Id';

comment on column PUREST_OAUTH2_USER.CREATE_TIME is
'创建时间';

comment on column PUREST_OAUTH2_USER.ID is
'ID';

comment on column PUREST_OAUTH2_USER.NAME is
'认证中心名';

comment on column PUREST_OAUTH2_USER.TYPE is
'TYPE';

comment on column PUREST_OAUTH2_USER.USER_ID is
'用户ID';

/*==============================================================*/
/* Table: PUREST_ORGANIZATION                                   */
/*==============================================================*/
create table PUREST_ORGANIZATION 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   NAME                 VARCHAR2(100)        not null,
   PARENT_ID            NUMBER(19),
   TELEPHONE            VARCHAR2(20),
   LEADER               VARCHAR2(20),
   SORT                 NUMBER,
   constraint PK_PUREST_ORGANIZATION primary key (ID),
   constraint UK_PUREST_ORG_NAME_PID unique (NAME, PARENT_ID)
);

comment on table PUREST_ORGANIZATION is
'组织机构';

comment on column PUREST_ORGANIZATION.ID is
'主键Id';

comment on column PUREST_ORGANIZATION.CREATE_BY is
'创建人';

comment on column PUREST_ORGANIZATION.CREATE_TIME is
'创建时间';

comment on column PUREST_ORGANIZATION.UPDATE_BY is
'修改人';

comment on column PUREST_ORGANIZATION.UPDATE_TIME is
'修改时间';

comment on column PUREST_ORGANIZATION.REMARK is
'备注';

comment on column PUREST_ORGANIZATION.NAME is
'名称';

comment on column PUREST_ORGANIZATION.PARENT_ID is
'父级ID';

comment on column PUREST_ORGANIZATION.TELEPHONE is
'联系电话';

comment on column PUREST_ORGANIZATION.LEADER is
'负责人';

comment on column PUREST_ORGANIZATION.SORT is
'排序';

/*==============================================================*/
/* Table: PUREST_PROFILE_SYSTEM                                 */
/*==============================================================*/
create table PUREST_PROFILE_SYSTEM 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   NAME                 VARCHAR2(20)         not null,
   CODE                 VARCHAR2(40)         not null,
   FILE_ID              NUMBER(19)              not null,
   constraint PK_PUREST_PROFILE_SYSTEM primary key (ID),
   constraint UK_PUREST_FILESYSTEM_CODE unique (CODE)
);

comment on table PUREST_PROFILE_SYSTEM is
'系统文件表';

comment on column PUREST_PROFILE_SYSTEM.ID is
'主键Id';

comment on column PUREST_PROFILE_SYSTEM.CREATE_BY is
'创建人';

comment on column PUREST_PROFILE_SYSTEM.CREATE_TIME is
'创建时间';

comment on column PUREST_PROFILE_SYSTEM.UPDATE_BY is
'修改人';

comment on column PUREST_PROFILE_SYSTEM.UPDATE_TIME is
'修改时间';

comment on column PUREST_PROFILE_SYSTEM.REMARK is
'备注';

comment on column PUREST_PROFILE_SYSTEM.NAME is
'名称';

comment on column PUREST_PROFILE_SYSTEM.CODE is
'编码';

comment on column PUREST_PROFILE_SYSTEM.FILE_ID is
'文件ID';

/*==============================================================*/
/* Table: PUREST_REQUEST_LOG                                    */
/*==============================================================*/
create table PUREST_REQUEST_LOG 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   CONTROLLER_NAME      VARCHAR2(100),
   ACTION_NAME          VARCHAR2(100),
   REQUEST_METHOD       VARCHAR2(10),
   ENVIRONMENT_NAME     VARCHAR2(20),
   ELAPSED_TIME         NUMBER(16,0),
   CLIENT_IP            VARCHAR2(20),
   constraint PK_PUREST_REQUEST_LOG primary key (ID)
);

comment on table PUREST_REQUEST_LOG is
'请求日志表';

comment on column PUREST_REQUEST_LOG.ID is
'主键Id';

comment on column PUREST_REQUEST_LOG.CREATE_BY is
'创建人';

comment on column PUREST_REQUEST_LOG.CREATE_TIME is
'创建时间';

comment on column PUREST_REQUEST_LOG.UPDATE_BY is
'修改人';

comment on column PUREST_REQUEST_LOG.UPDATE_TIME is
'修改时间';

comment on column PUREST_REQUEST_LOG.REMARK is
'备注';

comment on column PUREST_REQUEST_LOG.CONTROLLER_NAME is
'控制器';

comment on column PUREST_REQUEST_LOG.ACTION_NAME is
'方法名';

comment on column PUREST_REQUEST_LOG.REQUEST_METHOD is
'请求类型';

comment on column PUREST_REQUEST_LOG.ENVIRONMENT_NAME is
'服务器环境';

comment on column PUREST_REQUEST_LOG.ELAPSED_TIME is
'执行耗时';

comment on column PUREST_REQUEST_LOG.CLIENT_IP is
'客户端IP';

/*==============================================================*/
/* Table: PUREST_ROLE                                           */
/*==============================================================*/
create table PUREST_ROLE 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   NAME                 VARCHAR2(20)         not null,
   DESCRIPTION          VARCHAR2(200),
   constraint PK_PUREST_ROLE primary key (ID),
   constraint UK_PUREST_ROLE_NAME unique (NAME)
);

comment on table PUREST_ROLE is
'角色';

comment on column PUREST_ROLE.ID is
'主键Id';

comment on column PUREST_ROLE.CREATE_BY is
'创建人';

comment on column PUREST_ROLE.CREATE_TIME is
'创建时间';

comment on column PUREST_ROLE.UPDATE_BY is
'修改人';

comment on column PUREST_ROLE.UPDATE_TIME is
'修改时间';

comment on column PUREST_ROLE.REMARK is
'备注';

comment on column PUREST_ROLE.NAME is
'角色名称';

comment on column PUREST_ROLE.DESCRIPTION is
'角色描述';

/*==============================================================*/
/* Table: PUREST_ROLE_FUNCTION                                  */
/*==============================================================*/
create table PUREST_ROLE_FUNCTION 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   ROLE_ID              NUMBER(19),
   FUNCTION_ID          NUMBER(19),
   constraint PK_PUREST_ROLE_FUNCTION primary key (ID)
);

comment on table PUREST_ROLE_FUNCTION is
'角色功能表';

comment on column PUREST_ROLE_FUNCTION.ID is
'主键Id';

comment on column PUREST_ROLE_FUNCTION.CREATE_BY is
'创建人';

comment on column PUREST_ROLE_FUNCTION.CREATE_TIME is
'创建时间';

comment on column PUREST_ROLE_FUNCTION.UPDATE_BY is
'修改人';

comment on column PUREST_ROLE_FUNCTION.UPDATE_TIME is
'修改时间';

comment on column PUREST_ROLE_FUNCTION.REMARK is
'备注';

comment on column PUREST_ROLE_FUNCTION.ROLE_ID is
'角色ID';

comment on column PUREST_ROLE_FUNCTION.FUNCTION_ID is
'功能ID';

/*==============================================================*/
/* Table: PUREST_SYSTEM_CONFIG                                  */
/*==============================================================*/
create table PUREST_SYSTEM_CONFIG 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   NAME                 VARCHAR2(20),
   CONFIG_CODE          VARCHAR2(40)         not null,
   CONFIG_VALUE         VARCHAR2(1000),
   constraint PK_PUREST_SYSTEM_CONFIG primary key (ID),
   constraint UK_PUREST_CONFIG_CODE unique (CONFIG_CODE)
);

comment on table PUREST_SYSTEM_CONFIG is
'系统配置表';

comment on column PUREST_SYSTEM_CONFIG.ID is
'主键Id';

comment on column PUREST_SYSTEM_CONFIG.CREATE_BY is
'创建人';

comment on column PUREST_SYSTEM_CONFIG.CREATE_TIME is
'创建时间';

comment on column PUREST_SYSTEM_CONFIG.UPDATE_BY is
'修改人';

comment on column PUREST_SYSTEM_CONFIG.UPDATE_TIME is
'修改时间';

comment on column PUREST_SYSTEM_CONFIG.REMARK is
'备注';

comment on column PUREST_SYSTEM_CONFIG.NAME is
'名称';

comment on column PUREST_SYSTEM_CONFIG.CONFIG_CODE is
'编码';

comment on column PUREST_SYSTEM_CONFIG.CONFIG_VALUE is
'值';

/*==============================================================*/
/* Table: PUREST_USER                                           */
/*==============================================================*/
create table PUREST_USER 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   ACCOUNT              VARCHAR2(36)         not null,
   PASSWORD             VARCHAR2(100)        not null,
   NAME                 VARCHAR2(20)         not null,
   TELEPHONE            VARCHAR2(11),
   EMAIL                VARCHAR2(20),
   AVATAR               BLOB,
   STATUS               NUMBER,
   ORGANIZATION_ID      NUMBER(19)              not null,
   constraint PK_PUREST_USER primary key (ID),
   constraint UK_PUREST_USER_ACCOUNT unique (ACCOUNT)
);

comment on table PUREST_USER is
'用户';

comment on column PUREST_USER.ID is
'主键Id';

comment on column PUREST_USER.CREATE_BY is
'创建人';

comment on column PUREST_USER.CREATE_TIME is
'创建时间';

comment on column PUREST_USER.UPDATE_BY is
'修改人';

comment on column PUREST_USER.UPDATE_TIME is
'修改时间';

comment on column PUREST_USER.REMARK is
'备注';

comment on column PUREST_USER.ACCOUNT is
'账号';

comment on column PUREST_USER.PASSWORD is
'密码';

comment on column PUREST_USER.NAME is
'真实姓名';

comment on column PUREST_USER.TELEPHONE is
'电话';

comment on column PUREST_USER.EMAIL is
'邮箱';

comment on column PUREST_USER.AVATAR is
'头像';

comment on column PUREST_USER.STATUS is
'状态';

comment on column PUREST_USER.ORGANIZATION_ID is
'组织机构Id';

/*==============================================================*/
/* Table: PUREST_USER_ROLE                                      */
/*==============================================================*/
create table PUREST_USER_ROLE 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   ROLE_ID              NUMBER(19)              not null,
   USER_ID              NUMBER(19)              not null,
   constraint PK_PUREST_USER_ROLE primary key (ID)
);

comment on table PUREST_USER_ROLE is
'用户角色';

comment on column PUREST_USER_ROLE.ID is
'主键Id';

comment on column PUREST_USER_ROLE.CREATE_BY is
'创建人';

comment on column PUREST_USER_ROLE.CREATE_TIME is
'创建时间';

comment on column PUREST_USER_ROLE.UPDATE_BY is
'修改人';

comment on column PUREST_USER_ROLE.UPDATE_TIME is
'修改时间';

comment on column PUREST_USER_ROLE.REMARK is
'备注';

comment on column PUREST_USER_ROLE.ROLE_ID is
'角色ID';

comment on column PUREST_USER_ROLE.USER_ID is
'用户ID';

/*==============================================================*/
/* Table: PUREST_WF_AUDITING_RECORD                             */
/*==============================================================*/
create table PUREST_WF_AUDITING_RECORD 
(
   ID                   NUMBER(19)              not null,
   EXECUTION_POINTER_ID NUMBER(19)              not null,
   AUDITING_TIME        DATETIME                 not null,
   AUDITOR              NUMBER(19)              not null,
   AUDITOR_NAME         VARCHAR2(40),
   AUDITING_OPINION     CLOB,
   IS_AGREE             SMALLINT             not null,
   constraint PK_PUREST_WF_AUDITING_RECORD primary key (ID)
);

comment on table PUREST_WF_AUDITING_RECORD is
'流程审批记录';

comment on column PUREST_WF_AUDITING_RECORD.ID is
'主键Id';

comment on column PUREST_WF_AUDITING_RECORD.EXECUTION_POINTER_ID is
'步骤Id';

comment on column PUREST_WF_AUDITING_RECORD.AUDITING_TIME is
'审批时间';

comment on column PUREST_WF_AUDITING_RECORD.AUDITOR is
'审批人';

comment on column PUREST_WF_AUDITING_RECORD.AUDITOR_NAME is
'审批人姓名';

comment on column PUREST_WF_AUDITING_RECORD.AUDITING_OPINION is
'审批意见';

comment on column PUREST_WF_AUDITING_RECORD.IS_AGREE is
'是否同意';

/*==============================================================*/
/* Table: PUREST_WF_DEFINITION                                  */
/*==============================================================*/
create table PUREST_WF_DEFINITION 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   NAME                 VARCHAR2(20)         not null,
   DEFINITION_ID        VARCHAR2(36)         not null,
   WORKFLOW_CONTENT     CLOB                 not null,
   DESIGNS_CONTENT      CLOB                 not null,
   FORM_CONTENT         CLOB                 not null,
   VERSION              NUMBER(19)              not null,
   IS_LOCKED            SMALLINT             not null,
   constraint PK_PUREST_WF_DEFINITION primary key (ID),
   constraint UK_WORKFLOW_CODE unique (DEFINITION_ID)
);

comment on table PUREST_WF_DEFINITION is
'流程定义';

comment on column PUREST_WF_DEFINITION.ID is
'主键Id';

comment on column PUREST_WF_DEFINITION.CREATE_BY is
'创建人';

comment on column PUREST_WF_DEFINITION.CREATE_TIME is
'创建时间';

comment on column PUREST_WF_DEFINITION.UPDATE_BY is
'修改人';

comment on column PUREST_WF_DEFINITION.UPDATE_TIME is
'修改时间';

comment on column PUREST_WF_DEFINITION.REMARK is
'备注';

comment on column PUREST_WF_DEFINITION.NAME is
'名称';

comment on column PUREST_WF_DEFINITION.DEFINITION_ID is
'定义ID';

comment on column PUREST_WF_DEFINITION.WORKFLOW_CONTENT is
'流程内容';

comment on column PUREST_WF_DEFINITION.DESIGNS_CONTENT is
'设计器内容';

comment on column PUREST_WF_DEFINITION.FORM_CONTENT is
'表单内容';

comment on column PUREST_WF_DEFINITION.VERSION is
'版本';

comment on column PUREST_WF_DEFINITION.IS_LOCKED is
'是否锁定';

/*==============================================================*/
/* Table: PUREST_WF_EVENT                                       */
/*==============================================================*/
create table PUREST_WF_EVENT 
(
   PERSISTENCE_ID       NUMBER(19)              not null,
   EVENT_ID             VARCHAR2(36)         not null,
   EVENT_NAME           VARCHAR2(200),
   EVENT_KEY            VARCHAR2(200),
   EVENT_DATA           CLOB,
   EVENT_TIME           DATETIME                 not null,
   IS_PROCESSED         SMALLINT             not null,
   constraint PK_PUREST_WF_EVENT primary key (PERSISTENCE_ID)
);

comment on table PUREST_WF_EVENT is
'事件';

/*==============================================================*/
/* Index: "IX_Event_EventId"                                    */
/*==============================================================*/
create unique index "IX_Event_EventId" on PUREST_WF_EVENT (
   EVENT_ID ASC
);

/*==============================================================*/
/* Index: "IX_Event_EventTime"                                  */
/*==============================================================*/
create index "IX_Event_EventTime" on PUREST_WF_EVENT (
   EVENT_TIME ASC
);

/*==============================================================*/
/* Index: "IX_Event_IsProcessed"                                */
/*==============================================================*/
create index "IX_Event_IsProcessed" on PUREST_WF_EVENT (
   IS_PROCESSED ASC
);

/*==============================================================*/
/* Index: "IX_Event_EventName_EventKey"                         */
/*==============================================================*/
create index "IX_Event_EventName_EventKey" on PUREST_WF_EVENT (
   EVENT_NAME ASC,
   EVENT_KEY ASC
);

/*==============================================================*/
/* Table: PUREST_WF_EXECUTION_ATTRIBUTE                         */
/*==============================================================*/
create table PUREST_WF_EXECUTION_ATTRIBUTE 
(
   PERSISTENCE_ID       NUMBER(19)              not null,
   ATTRIBUTE_KEY        VARCHAR2(100),
   ATTRIBUTE_VALUE      CLOB,
   EXECUTION_POINTER_ID NUMBER(19)              not null,
   constraint PK_PUREST_WF_EXECUTION_ATTRIBU primary key (PERSISTENCE_ID)
);

comment on table PUREST_WF_EXECUTION_ATTRIBUTE is
'自定义属性';

/*==============================================================*/
/* Index: "IX_ExecutionPointerId"                               */
/*==============================================================*/
create index "IX_ExecutionPointerId" on PUREST_WF_EXECUTION_ATTRIBUTE (
   EXECUTION_POINTER_ID ASC
);

/*==============================================================*/
/* Table: PUREST_WF_EXECUTION_ERROR                             */
/*==============================================================*/
create table PUREST_WF_EXECUTION_ERROR 
(
   PERSISTENCE_ID       NUMBER(19)              not null,
   ERROR_TIME           DATETIME                 not null,
   EXECUTION_POINTER_ID VARCHAR2(100),
   MESSAGE              CLOB,
   WORKFLOW_ID          VARCHAR2(100),
   constraint PK_PUREST_WF_EXECUTION_ERROR primary key (PERSISTENCE_ID)
);

comment on table PUREST_WF_EXECUTION_ERROR is
'执行异常';

/*==============================================================*/
/* Table: PUREST_WF_EXECUTION_POINTER                           */
/*==============================================================*/
create table PUREST_WF_EXECUTION_POINTER 
(
   PERSISTENCE_ID       NUMBER(19)              not null,
   WORKFLOW_ID          NUMBER(19)              not null,
   ID                   VARCHAR2(36),
   START_TIME           DATETIME,
   END_TIME             DATETIME,
   ACTIVE               SMALLINT             not null,
   EVENT_KEY            VARCHAR2(100),
   EVENT_NAME           VARCHAR2(100),
   EVENT_DATA           CLOB,
   EVENT_PUBLISHED      SMALLINT             not null,
   PERSISTENCE_DATA     CLOB,
   SLEEP_UNTIL          DATETIME,
   STEP_ID              NUMBER(19)              not null,
   STEP_NAME            VARCHAR2(100),
   CHILDREN             CLOB,
   CONTEXT_ITEM         CLOB,
   PREDECESSOR_ID       VARCHAR2(100),
   OUTCOME              CLOB,
   SCOPE                CLOB,
   RETRY_COUNT          NUMBER(19)              not null,
   STATUS               NUMBER(19)              not null,
   constraint PK_PUREST_WF_EXECUTION_POINTER primary key (PERSISTENCE_ID)
);

comment on table PUREST_WF_EXECUTION_POINTER is
'步骤';

/*==============================================================*/
/* Index: "IX_ExecutionPointer_WorkflowId"                      */
/*==============================================================*/
create index "IX_ExecutionPointer_WorkflowId" on PUREST_WF_EXECUTION_POINTER (
   WORKFLOW_ID ASC
);

/*==============================================================*/
/* Table: PUREST_WF_SCHEDULED_COMMAND                           */
/*==============================================================*/
create table PUREST_WF_SCHEDULED_COMMAND 
(
   PERSISTENCE_ID       NUMBER(19)              not null,
   COMMAND_NAME         VARCHAR2(200),
   DATA                 VARCHAR2(500),
   EXECUTE_TIME         NUMBER(19)              not null,
   constraint PK_PUREST_WF_SCHEDULED_COMMAND primary key (PERSISTENCE_ID)
);

comment on table PUREST_WF_SCHEDULED_COMMAND is
'计划命令';

/*==============================================================*/
/* Index: "IX_CommandName_Data"                                 */
/*==============================================================*/
create unique index "IX_CommandName_Data" on PUREST_WF_SCHEDULED_COMMAND (
   COMMAND_NAME ASC,
   DATA ASC
);

/*==============================================================*/
/* Index: "IX_ExecuteTime"                                      */
/*==============================================================*/
create index "IX_ExecuteTime" on PUREST_WF_SCHEDULED_COMMAND (
   EXECUTE_TIME ASC
);

/*==============================================================*/
/* Table: PUREST_WF_SUBSCRIPTION                                */
/*==============================================================*/
create table PUREST_WF_SUBSCRIPTION 
(
   PERSISTENCE_ID       NUMBER(19)              not null,
   EVENT_KEY            VARCHAR2(200),
   EVENT_NAME           VARCHAR2(200),
   STEP_ID              NUMBER(19)              not null,
   SUBSCRIPTION_ID      VARCHAR2(36)         not null,
   WORKFLOW_ID          VARCHAR2(200),
   SUBSCRIBE_AS_OF      DATETIME                 not null,
   SUBSCRIPTION_DATA    CLOB,
   EXECUTION_POINTER_ID VARCHAR2(200),
   EXTERNAL_TOKEN       VARCHAR2(200),
   EXTERNAL_TOKEN_EXPIRY DATETIME,
   EXTERNAL_WORKER_ID   VARCHAR2(200),
   constraint PK_PUREST_WF_SUBSCRIPTION primary key (PERSISTENCE_ID)
);

comment on table PUREST_WF_SUBSCRIPTION is
'订阅';

/*==============================================================*/
/* Index: "IX_Subscription_SubscriptionId"                      */
/*==============================================================*/
create unique index "IX_Subscription_SubscriptionId" on PUREST_WF_SUBSCRIPTION (
   SUBSCRIPTION_ID ASC
);

/*==============================================================*/
/* Index: "IX_Subscription_EventKey"                            */
/*==============================================================*/
create index "IX_Subscription_EventKey" on PUREST_WF_SUBSCRIPTION (
   EVENT_KEY ASC
);

/*==============================================================*/
/* Index: "IX_Subscription_EventName"                           */
/*==============================================================*/
create index "IX_Subscription_EventName" on PUREST_WF_SUBSCRIPTION (
   EVENT_NAME ASC
);

/*==============================================================*/
/* Table: PUREST_WF_WAITING_POINTER                             */
/*==============================================================*/
create table PUREST_WF_WAITING_POINTER 
(
   ID                   NUMBER(19)              not null,
   USER_ID              NUMBER(19)              not null,
   POINTER_ID           VARCHAR2(36)         not null,
   constraint PK_PUREST_WF_WAITING_POINTER primary key (ID)
);

comment on table PUREST_WF_WAITING_POINTER is
'待审核步骤';

comment on column PUREST_WF_WAITING_POINTER.ID is
'主键Id';

comment on column PUREST_WF_WAITING_POINTER.USER_ID is
'用户Id';

comment on column PUREST_WF_WAITING_POINTER.POINTER_ID is
'步骤Id';

/*==============================================================*/
/* Table: PUREST_WF_WORKFLOW                                    */
/*==============================================================*/
create table PUREST_WF_WORKFLOW 
(
   PERSISTENCE_ID       NUMBER(19)              not null,
   COMPLETE_TIME        DATETIME,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   "DATA"                 CLOB,
   DESCRIPTION          VARCHAR2(500),
   INSTANCE_ID          VARCHAR2(36)         not null,
   NEXT_EXECUTION       NUMBER(19),
   STATUS               NUMBER(19)              not null,
   "VERSION"              NUMBER(19)              not null,
   WORKFLOW_DEFINITION_ID VARCHAR2(36)         not null,
   "REFERENCE"            VARCHAR2(200),
   REMARK               VARCHAR2(1000),
   constraint PK_PUREST_WF_WORKFLOW primary key (PERSISTENCE_ID)
);

comment on table PUREST_WF_WORKFLOW is
'工作流程';

/*==============================================================*/
/* Index: "IX_Workflow_InstanceId"                              */
/*==============================================================*/
create index "IX_Workflow_InstanceId" on PUREST_WF_WORKFLOW (
   INSTANCE_ID ASC
);

/*==============================================================*/
/* Index: "IX_Workflow_NextExecution"                           */
/*==============================================================*/
create index "IX_Workflow_NextExecution" on PUREST_WF_WORKFLOW (
   NEXT_EXECUTION ASC
);

/*==============================================================*/
/* Table: PUREST_WORKFLOW_INSTANCE                              */
/*==============================================================*/
create table PUREST_WORKFLOW_INSTANCE 
(
   ID                   NUMBER(19)              not null,
   CREATE_BY            NUMBER(19)              not null,
   CREATE_TIME          DATETIME                 not null,
   UPDATE_BY            NUMBER(19),
   UPDATE_TIME          DATETIME,
   REMARK               VARCHAR2(1000),
   WF_ID                NUMBER(19)              not null,
   SCHEME_ID            NUMBER(19)              not null,
   FORM_DATA            CLOB                 not null,
   CURRENT_NODE         NUMBER(19),
   CURRENT_NODE_TYPE    NUMBER(19)              not null,
   STATUS               NUMBER(19)              not null,
   constraint PK_PUREST_WORKFLOW_INSTANCE primary key (ID)
);

comment on table PUREST_WORKFLOW_INSTANCE is
'流程实例';

comment on column PUREST_WORKFLOW_INSTANCE.ID is
'主键Id';

comment on column PUREST_WORKFLOW_INSTANCE.CREATE_BY is
'创建人';

comment on column PUREST_WORKFLOW_INSTANCE.CREATE_TIME is
'创建时间';

comment on column PUREST_WORKFLOW_INSTANCE.UPDATE_BY is
'修改人';

comment on column PUREST_WORKFLOW_INSTANCE.UPDATE_TIME is
'修改时间';

comment on column PUREST_WORKFLOW_INSTANCE.REMARK is
'备注';

comment on column PUREST_WORKFLOW_INSTANCE.WF_ID is
'流程ID';

comment on column PUREST_WORKFLOW_INSTANCE.SCHEME_ID is
'设计ID';

comment on column PUREST_WORKFLOW_INSTANCE.FORM_DATA is
'表单值';

comment on column PUREST_WORKFLOW_INSTANCE.CURRENT_NODE is
'当前节点';

comment on column PUREST_WORKFLOW_INSTANCE.CURRENT_NODE_TYPE is
'当前节点类型';

comment on column PUREST_WORKFLOW_INSTANCE.STATUS is
'状态';

alter table PUREST_DICT_DATA
   add constraint FK_PUREST_D_REFERENCE_PUREST_D foreign key (CATEGORY_ID)
      references PUREST_DICT_CATEGORY (ID)
      on delete cascade;

alter table PUREST_FUNCTION_INTERFACE
   add constraint FK_PUREST_F_REFERENCE_PUREST_F foreign key (FUNCTION_ID)
      references PUREST_FUNCTION (ID)
      on delete cascade;

alter table PUREST_FUNCTION_INTERFACE
   add constraint FK_PUREST_F_REFERENCE_PUREST_I foreign key (INTERFACE_ID)
      references PUREST_INTERFACE (ID)
      on delete cascade;

alter table PUREST_INTERFACE
   add constraint FK_PUREST_I_REFERENCE_PUREST_I foreign key (GROUP_ID)
      references PUREST_INTERFACE_GROUP (ID)
      on delete cascade;

alter table PUREST_NOTICE_RECORD
   add constraint FK_PUREST_N_REFERENCE_PUREST_N foreign key (NOTICE_ID)
      references PUREST_NOTICE (ID)
      on delete cascade;

alter table PUREST_OAUTH2_USER
   add constraint FK_PUREST_O_REFERENCE_PUREST_U foreign key (USER_ID)
      references PUREST_USER (ID)
      on delete cascade;

alter table PUREST_PROFILE_SYSTEM
   add constraint FK_PUREST_P_REFERENCE_PUREST_F foreign key (FILE_ID)
      references PUREST_FILE_RECORD (ID)
      on delete cascade;

alter table PUREST_ROLE_FUNCTION
   add constraint FK_PUREST_R_REFERENCE_PUREST_R foreign key (ROLE_ID)
      references PUREST_ROLE (ID)
      on delete cascade;

alter table PUREST_ROLE_FUNCTION
   add constraint FK_PUREST_R_REFERENCE_PUREST_F foreign key (FUNCTION_ID)
      references PUREST_FUNCTION (ID)
      on delete cascade;

alter table PUREST_USER
   add constraint FK_PUREST_U_REFERENCE_PUREST_O foreign key (ORGANIZATION_ID)
      references PUREST_ORGANIZATION (ID)
      on delete cascade;

alter table PUREST_USER_ROLE
   add constraint FK_PUREST_U_REFERENCE_PUREST_R foreign key (ROLE_ID)
      references PUREST_ROLE (ID)
      on delete cascade;

alter table PUREST_USER_ROLE
   add constraint FK_PUREST_U_REFERENCE_PUREST_U foreign key (USER_ID)
      references PUREST_USER (ID)
      on delete cascade;

alter table PUREST_WF_EXECUTION_ATTRIBUTE
   add constraint "FK_Reference_20" foreign key (EXECUTION_POINTER_ID)
      references PUREST_WF_EXECUTION_POINTER (PERSISTENCE_ID)
      on delete cascade;

alter table PUREST_WF_EXECUTION_POINTER
   add constraint "FK_Reference_19" foreign key (WORKFLOW_ID)
      references PUREST_WF_WORKFLOW (PERSISTENCE_ID)
      on delete cascade;

alter table PUREST_WORKFLOW_INSTANCE
   add constraint "FK_Reference_23" foreign key (WF_ID)
      references PUREST_WF_WORKFLOW (PERSISTENCE_ID);

alter table PUREST_WORKFLOW_INSTANCE
   add constraint "FK_Reference_24" foreign key (SCHEME_ID)
      references PUREST_WF_DEFINITION (ID);

