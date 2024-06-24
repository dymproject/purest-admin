/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2024/6/24 14:53:14                           */
/*==============================================================*/


drop table if exists PUREST_BACKGROUND_JOB_RECORD;

drop table if exists PUREST_DICT_CATEGORY;

drop table if exists PUREST_DICT_DATA;

drop table if exists PUREST_FILE_RECORD;

drop table if exists PUREST_FUNCTION;

drop table if exists PUREST_FUNCTION_INTERFACE;

drop table if exists PUREST_INTERFACE;

drop table if exists PUREST_INTERFACE_GROUP;

drop table if exists PUREST_NOTICE;

drop table if exists PUREST_NOTICE_RECORD;

drop table if exists PUREST_ORGANIZATION;

drop table if exists PUREST_PROFILE_SYSTEM;

drop table if exists PUREST_REQUEST_LOG;

drop table if exists PUREST_ROLE;

drop table if exists PUREST_ROLE_FUNCTION;

drop table if exists PUREST_SYSTEM_CONFIG;

drop table if exists PUREST_USER;

drop table if exists PUREST_USER_ROLE;

drop index IX_Event_EventName_EventKey on PUREST_WF_EVENT;

drop index IX_Event_IsProcessed on PUREST_WF_EVENT;

drop index IX_Event_EventTime on PUREST_WF_EVENT;

drop index IX_Event_EventId on PUREST_WF_EVENT;

drop table if exists PUREST_WF_EVENT;

drop index IX_ExtensionAttribute_ExecutionPointerId on PUREST_WF_EXECUTION_ATTRIBUTE;

drop table if exists PUREST_WF_EXECUTION_ATTRIBUTE;

drop table if exists PUREST_WF_EXECUTION_ERROR;

drop index IX_ExecutionPointer_WorkflowId on PUREST_WF_EXECUTION_POINTER;

drop table if exists PUREST_WF_EXECUTION_POINTER;

drop index IX_ScheduledCommand_ExecuteTime on PUREST_WF_SCHEDULED_COMMAND;

drop index IX_ScheduledCommand_CommandName_Data on PUREST_WF_SCHEDULED_COMMAND;

drop table if exists PUREST_WF_SCHEDULED_COMMAND;

drop index IX_Subscription_EventName on PUREST_WF_SUBSCRIPTION;

drop index IX_Subscription_EventKey on PUREST_WF_SUBSCRIPTION;

drop index IX_Subscription_SubscriptionId on PUREST_WF_SUBSCRIPTION;

drop table if exists PUREST_WF_SUBSCRIPTION;

drop index IX_Workflow_NextExecution on PUREST_WF_WORKFLOW;

drop index IX_Workflow_InstanceId on PUREST_WF_WORKFLOW;

drop table if exists PUREST_WF_WORKFLOW;

/*==============================================================*/
/* Table: PUREST_BACKGROUND_JOB_RECORD                          */
/*==============================================================*/
create table PUREST_BACKGROUND_JOB_RECORD
(
   ID                   varchar(40) not null comment 'Id',
   JOB_NAME             varchar(128) not null comment '����',
   JOB_ARGS             longtext not null comment '����',
   TRY_COUNT            numeric comment '���Դ���',
   CREATION_TIME        datetime comment '����ʱ��',
   NEXT_TRY_TIME        datetime comment '�´�ִ��ʱ��',
   LAST_TRY_TIME        datetime comment '���ִ��ʱ��',
   IS_ABANDONED         numeric(1) comment '�Ƿ�ʱ',
   PRIORITY             numeric comment '���ȼ�',
   primary key (ID)
);

alter table PUREST_BACKGROUND_JOB_RECORD comment '��̨��ҵ��¼��';

/*==============================================================*/
/* Table: PUREST_DICT_CATEGORY                                  */
/*==============================================================*/
create table PUREST_DICT_CATEGORY
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   NAME                 varchar(20) not null comment '��������',
   CODE                 varchar(40) not null comment '�������',
   primary key (ID),
   unique key UK_PUREST_CATEGORY_CODE (CODE)
);

alter table PUREST_DICT_CATEGORY comment '�ֵ����';

/*==============================================================*/
/* Table: PUREST_DICT_DATA                                      */
/*==============================================================*/
create table PUREST_DICT_DATA
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   CATEGORY_ID          numeric(19,0) not null comment '�ֵ����ID',
   NAME                 varchar(20) not null comment '�ֵ�����',
   SORT                 numeric not null comment '����',
   primary key (ID)
);

alter table PUREST_DICT_DATA comment '�ֵ�����';

/*==============================================================*/
/* Table: PUREST_FILE_RECORD                                    */
/*==============================================================*/
create table PUREST_FILE_RECORD
(
   ID                   numeric(19,0) not null comment 'Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   FILE_NAME            varchar(100) not null comment '�ļ���',
   FILE_SIZE            numeric(10) not null comment '�ļ���С',
   FILE_EXT             varchar(10) not null comment '�ļ���չ��',
   primary key (ID)
);

alter table PUREST_FILE_RECORD comment '�ļ��ϴ���¼��';

/*==============================================================*/
/* Table: PUREST_FUNCTION                                       */
/*==============================================================*/
create table PUREST_FUNCTION
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   NAME                 varchar(20) not null comment '����',
   CODE                 varchar(40) not null comment '����',
   PARENT_ID            numeric(19,0) comment '������',
   primary key (ID),
   unique key UK_PUREST_FUNCTION_CODE (CODE)
);

alter table PUREST_FUNCTION comment '���ܱ�';

/*==============================================================*/
/* Table: PUREST_FUNCTION_INTERFACE                             */
/*==============================================================*/
create table PUREST_FUNCTION_INTERFACE
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   INTERFACE_ID         numeric(19,0) comment '�ӿ�ID',
   FUNCTION_ID          numeric(19,0) comment '����ID',
   primary key (ID)
);

alter table PUREST_FUNCTION_INTERFACE comment 'ҳ��ӿڱ�';

/*==============================================================*/
/* Table: PUREST_INTERFACE                                      */
/*==============================================================*/
create table PUREST_INTERFACE
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   NAME                 varchar(20) not null comment '�ӿ�����',
   PATH                 varchar(200) not null comment '�ӿڵ�ַ',
   REQUEST_METHOD       varchar(20) not null comment '���󷽷�',
   GROUP_ID             numeric(19,0) comment '�ӿڷ���ID',
   primary key (ID),
   unique key UK_INTERFACE_PATHMETHOD (PATH, REQUEST_METHOD)
);

alter table PUREST_INTERFACE comment '�ӿڱ�';

/*==============================================================*/
/* Table: PUREST_INTERFACE_GROUP                                */
/*==============================================================*/
create table PUREST_INTERFACE_GROUP
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   NAME                 varchar(20) comment '����',
   CODE                 varchar(40) not null comment '����',
   primary key (ID),
   unique key UK_PUREST_IG_CODE (CODE)
);

alter table PUREST_INTERFACE_GROUP comment '�ӿڷ����';

/*==============================================================*/
/* Table: PUREST_NOTICE                                         */
/*==============================================================*/
create table PUREST_NOTICE
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   TITLE                varchar(40) not null comment '����',
   CONTENT              longtext comment '����',
   NOTICE_TYPE          numeric(19,0) not null comment '����',
   LEVEL                numeric(19,0) comment '����',
   primary key (ID)
);

alter table PUREST_NOTICE comment '֪ͨ�����';

/*==============================================================*/
/* Table: PUREST_NOTICE_RECORD                                  */
/*==============================================================*/
create table PUREST_NOTICE_RECORD
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   RECEIVER             numeric(19,0) not null comment '������',
   IS_READ              numeric(1) not null comment '�Ƿ��Ѷ�',
   NOTICE_ID            numeric(19,0) not null comment '֪ͨ����Id',
   primary key (ID)
);

alter table PUREST_NOTICE_RECORD comment '֪ͨ�����¼��';

/*==============================================================*/
/* Table: PUREST_ORGANIZATION                                   */
/*==============================================================*/
create table PUREST_ORGANIZATION
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   NAME                 varchar(100) not null comment '����',
   PARENT_ID            numeric(19,0) comment '����ID',
   TELEPHONE            varchar(20) comment '��ϵ�绰',
   LEADER               varchar(20) comment '������',
   SORT                 numeric comment '����',
   primary key (ID),
   unique key UK_PUREST_ORG_NAME_PID (NAME, PARENT_ID)
);

alter table PUREST_ORGANIZATION comment '��֯����';

/*==============================================================*/
/* Table: PUREST_PROFILE_SYSTEM                                 */
/*==============================================================*/
create table PUREST_PROFILE_SYSTEM
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   NAME                 varchar(20) not null comment '����',
   CODE                 varchar(40) not null comment '����',
   FILE_ID              numeric(19,0) not null comment '�ļ�ID',
   primary key (ID),
   unique key UK_PUREST_FILESYSTEM_CODE (CODE)
);

alter table PUREST_PROFILE_SYSTEM comment 'ϵͳ�ļ���';

/*==============================================================*/
/* Table: PUREST_REQUEST_LOG                                    */
/*==============================================================*/
create table PUREST_REQUEST_LOG
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   CONTROLLER_NAME      varchar(100) comment '������',
   ACTION_NAME          varchar(100) comment '������',
   REQUEST_METHOD       varchar(10) comment '��������',
   ENVIRONMENT_NAME     varchar(20) comment '����������',
   ELAPSED_TIME         numeric(16,0) comment 'ִ�к�ʱ',
   CLIENT_IP            varchar(20) comment '�ͻ���IP',
   primary key (ID)
);

alter table PUREST_REQUEST_LOG comment '������־��';

/*==============================================================*/
/* Table: PUREST_ROLE                                           */
/*==============================================================*/
create table PUREST_ROLE
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   NAME                 varchar(20) not null comment '��ɫ����',
   DESCRIPTION          varchar(200) comment '��ɫ����',
   primary key (ID),
   unique key UK_PUREST_ROLE_NAME (NAME)
);

alter table PUREST_ROLE comment '��ɫ';

/*==============================================================*/
/* Table: PUREST_ROLE_FUNCTION                                  */
/*==============================================================*/
create table PUREST_ROLE_FUNCTION
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   ROLE_ID              numeric(19,0) comment '��ɫID',
   FUNCTION_ID          numeric(19,0) comment '����ID',
   primary key (ID)
);

alter table PUREST_ROLE_FUNCTION comment '��ɫ���ܱ�';

/*==============================================================*/
/* Table: PUREST_SYSTEM_CONFIG                                  */
/*==============================================================*/
create table PUREST_SYSTEM_CONFIG
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   NAME                 varchar(20) comment '����',
   CONFIG_CODE          varchar(40) not null comment '����',
   CONFIG_VALUE         varchar(1000) comment 'ֵ',
   primary key (ID),
   unique key UK_PUREST_CONFIG_CODE (CONFIG_CODE)
);

alter table PUREST_SYSTEM_CONFIG comment 'ϵͳ���ñ�';

/*==============================================================*/
/* Table: PUREST_USER                                           */
/*==============================================================*/
create table PUREST_USER
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   ACCOUNT              varchar(36) not null comment '�˺�',
   PASSWORD             varchar(100) not null comment '����',
   NAME                 varchar(20) not null comment '��ʵ����',
   TELEPHONE            varchar(11) comment '�绰',
   EMAIL                varchar(20) comment '����',
   AVATAR               longblob comment 'ͷ��',
   STATUS               numeric comment '״̬',
   ORGANIZATION_ID      numeric(19,0) not null comment '��֯����Id',
   primary key (ID),
   unique key UK_PUREST_USER_ACCOUNT (ACCOUNT)
);

alter table PUREST_USER comment '�û�';

/*==============================================================*/
/* Table: PUREST_USER_ROLE                                      */
/*==============================================================*/
create table PUREST_USER_ROLE
(
   ID                   numeric(19,0) not null comment '����Id',
   CREATE_BY            numeric(19,0) not null comment '������',
   CREATE_TIME          datetime not null comment '����ʱ��',
   UPDATE_BY            numeric(19,0) comment '�޸���',
   UPDATE_TIME          datetime comment '�޸�ʱ��',
   REMARK               varchar(1000) comment '��ע',
   ROLE_ID              numeric(19,0) not null comment '��ɫID',
   USER_ID              numeric(19,0) not null comment '�û�ID',
   primary key (ID)
);

alter table PUREST_USER_ROLE comment '�û���ɫ';

/*==============================================================*/
/* Table: PUREST_WF_EVENT                                       */
/*==============================================================*/
create table PUREST_WF_EVENT
(
   PERSISTENCE_ID       bigint not null,
   EVENT_ID             varchar(36) not null,
   EVENT_NAME           varchar(200),
   EVENT_KEY            varchar(200),
   EVENT_DATA           text,
   EVENT_TIME           datetime not null,
   IS_PROCESSED         boolean not null,
   primary key (PERSISTENCE_ID)
);

alter table PUREST_WF_EVENT comment '�¼�';

/*==============================================================*/
/* Index: IX_Event_EventId                                      */
/*==============================================================*/
create unique index IX_Event_EventId on PUREST_WF_EVENT
(
   EVENT_ID
);

/*==============================================================*/
/* Index: IX_Event_EventTime                                    */
/*==============================================================*/
create index IX_Event_EventTime on PUREST_WF_EVENT
(
   EVENT_TIME
);

/*==============================================================*/
/* Index: IX_Event_IsProcessed                                  */
/*==============================================================*/
create index IX_Event_IsProcessed on PUREST_WF_EVENT
(
   IS_PROCESSED
);

/*==============================================================*/
/* Index: IX_Event_EventName_EventKey                           */
/*==============================================================*/
create index IX_Event_EventName_EventKey on PUREST_WF_EVENT
(
   EVENT_NAME,
   EVENT_KEY
);

/*==============================================================*/
/* Table: PUREST_WF_EXECUTION_ATTRIBUTE                         */
/*==============================================================*/
create table PUREST_WF_EXECUTION_ATTRIBUTE
(
   PERSISTENCE_ID       bigint not null,
   ATTRIBUTE_KEY        varchar(100),
   ATTRIBUTE_VALUE      text,
   EXECUTION_POINTER_ID bigint not null,
   primary key (PERSISTENCE_ID)
);

alter table PUREST_WF_EXECUTION_ATTRIBUTE comment '�Զ�������';

/*==============================================================*/
/* Index: IX_ExtensionAttribute_ExecutionPointerId              */
/*==============================================================*/
create index IX_ExtensionAttribute_ExecutionPointerId on PUREST_WF_EXECUTION_ATTRIBUTE
(
   EXECUTION_POINTER_ID
);

/*==============================================================*/
/* Table: PUREST_WF_EXECUTION_ERROR                             */
/*==============================================================*/
create table PUREST_WF_EXECUTION_ERROR
(
   PERSISTENCE_ID       bigint not null,
   ERROR_TIME           date not null,
   EXECUTION_POINTER_ID varchar(100),
   MESSAGE              text,
   WORKFLOW_ID          varchar(100),
   primary key (PERSISTENCE_ID)
);

alter table PUREST_WF_EXECUTION_ERROR comment 'ִ���쳣';

/*==============================================================*/
/* Table: PUREST_WF_EXECUTION_POINTER                           */
/*==============================================================*/
create table PUREST_WF_EXECUTION_POINTER
(
   PERSISTENCE_ID       bigint not null,
   ACTIVE               boolean not null,
   RETRY_COUNT          integer not null,
   END_TIME             datetime,
   EVENT_DATA           text,
   EVENT_KEY            varchar(100),
   EVENT_NAME           varchar(100),
   EVENT_PUBLISHED      boolean not null,
   ID                   varchar(50),
   PERSISTENCE_DATA     text,
   SLEEP_UNTIL          datetime,
   START_TIME           datetime,
   STEP_ID              integer not null,
   STEP_NAME            varchar(100),
   WORKFLOW_ID          bigint not null,
   CHILDREN             text,
   CONTEXT_ITEM         text,
   PREDECESSOR_ID       varchar(100),
   OUTCOME              text,
   SCOPE                text,
   STATUS               integer not null,
   primary key (PERSISTENCE_ID)
);

alter table PUREST_WF_EXECUTION_POINTER comment '����';

/*==============================================================*/
/* Index: IX_ExecutionPointer_WorkflowId                        */
/*==============================================================*/
create index IX_ExecutionPointer_WorkflowId on PUREST_WF_EXECUTION_POINTER
(
   WORKFLOW_ID
);

/*==============================================================*/
/* Table: PUREST_WF_SCHEDULED_COMMAND                           */
/*==============================================================*/
create table PUREST_WF_SCHEDULED_COMMAND
(
   PERSISTENCE_ID       bigint not null,
   COMMAND_NAME         varchar(200),
   DATA                 varchar(500),
   EXECUTE_TIME         bigint not null,
   primary key (PERSISTENCE_ID)
);

alter table PUREST_WF_SCHEDULED_COMMAND comment '�ƻ�����';

/*==============================================================*/
/* Index: IX_ScheduledCommand_CommandName_Data                  */
/*==============================================================*/
create unique index IX_ScheduledCommand_CommandName_Data on PUREST_WF_SCHEDULED_COMMAND
(
   COMMAND_NAME,
   DATA
);

/*==============================================================*/
/* Index: IX_ScheduledCommand_ExecuteTime                       */
/*==============================================================*/
create index IX_ScheduledCommand_ExecuteTime on PUREST_WF_SCHEDULED_COMMAND
(
   EXECUTE_TIME
);

/*==============================================================*/
/* Table: PUREST_WF_SUBSCRIPTION                                */
/*==============================================================*/
create table PUREST_WF_SUBSCRIPTION
(
   PERSISTENCE_ID       bigint not null,
   EVENT_KEY            varchar(200),
   EVENT_NAME           varchar(200),
   STEP_ID              integer not null,
   SUBSCRIPTION_ID      varchar(36) not null,
   WORKFLOW_ID          varchar(200),
   SUBSCRIBE_AS_OF      datetime not null,
   SUBSCRIPTION_DATA    text,
   EXECUTION_POINTER_ID varchar(200),
   EXTERNAL_TOKEN       varchar(200),
   EXTERNAL_TOKEN_EXPIRY datetime,
   EXTERNAL_WORKER_ID   varchar(200),
   primary key (PERSISTENCE_ID)
);

alter table PUREST_WF_SUBSCRIPTION comment '����';

/*==============================================================*/
/* Index: IX_Subscription_SubscriptionId                        */
/*==============================================================*/
create unique index IX_Subscription_SubscriptionId on PUREST_WF_SUBSCRIPTION
(
   SUBSCRIPTION_ID
);

/*==============================================================*/
/* Index: IX_Subscription_EventKey                              */
/*==============================================================*/
create index IX_Subscription_EventKey on PUREST_WF_SUBSCRIPTION
(
   EVENT_KEY
);

/*==============================================================*/
/* Index: IX_Subscription_EventName                             */
/*==============================================================*/
create index IX_Subscription_EventName on PUREST_WF_SUBSCRIPTION
(
   EVENT_NAME
);

/*==============================================================*/
/* Table: PUREST_WF_WORKFLOW                                    */
/*==============================================================*/
create table PUREST_WF_WORKFLOW
(
   PERSISTENCE_ID       bigint not null,
   COMPLETE_TIME        datetime,
   CREATE_TIME          datetime not null,
   DATA                 text,
   DESCRIPTION          varchar(500),
   INSTANCE_ID          varchar(36) not null,
   NEXT_EXECUTION       bigint,
   STATUS               integer not null,
   VERSION              integer not null,
   WORKFLOW_DEFINITION_ID varchar(200),
   REFERENCE            varchar(200),
   primary key (PERSISTENCE_ID)
);

alter table PUREST_WF_WORKFLOW comment '��������';

/*==============================================================*/
/* Index: IX_Workflow_InstanceId                                */
/*==============================================================*/
create index IX_Workflow_InstanceId on PUREST_WF_WORKFLOW
(
   INSTANCE_ID
);

/*==============================================================*/
/* Index: IX_Workflow_NextExecution                             */
/*==============================================================*/
create index IX_Workflow_NextExecution on PUREST_WF_WORKFLOW
(
   NEXT_EXECUTION
);

alter table PUREST_DICT_DATA add constraint FK_Reference_5 foreign key (CATEGORY_ID)
      references PUREST_DICT_CATEGORY (ID) on delete restrict on update restrict;

alter table PUREST_FUNCTION_INTERFACE add constraint FK_Reference_11 foreign key (FUNCTION_ID)
      references PUREST_FUNCTION (ID) on delete restrict on update restrict;

alter table PUREST_FUNCTION_INTERFACE add constraint FK_Reference_12 foreign key (INTERFACE_ID)
      references PUREST_INTERFACE (ID) on delete restrict on update restrict;

alter table PUREST_INTERFACE add constraint FK_Reference_17 foreign key (GROUP_ID)
      references PUREST_INTERFACE_GROUP (ID) on delete restrict on update restrict;

alter table PUREST_NOTICE_RECORD add constraint FK_Reference_13 foreign key (NOTICE_ID)
      references PUREST_NOTICE (ID) on delete restrict on update restrict;

alter table PUREST_ORGANIZATION add constraint FK_Reference_14 foreign key (PARENT_ID)
      references PUREST_ORGANIZATION (ID) on delete restrict on update restrict;

alter table PUREST_PROFILE_SYSTEM add constraint FK_Reference_18 foreign key (FILE_ID)
      references PUREST_FILE_RECORD (ID) on delete restrict on update restrict;

alter table PUREST_ROLE_FUNCTION add constraint FK_Reference_15 foreign key (ROLE_ID)
      references PUREST_ROLE (ID) on delete restrict on update restrict;

alter table PUREST_ROLE_FUNCTION add constraint FK_Reference_16 foreign key (FUNCTION_ID)
      references PUREST_FUNCTION (ID) on delete restrict on update restrict;

alter table PUREST_USER add constraint FK_Reference_7 foreign key (ORGANIZATION_ID)
      references PUREST_ORGANIZATION (ID) on delete restrict on update restrict;

alter table PUREST_USER_ROLE add constraint FK_Reference_4 foreign key (ROLE_ID)
      references PUREST_ROLE (ID) on delete restrict on update restrict;

alter table PUREST_USER_ROLE add constraint FK_Reference_6 foreign key (USER_ID)
      references PUREST_USER (ID) on delete restrict on update restrict;

alter table PUREST_WF_EXECUTION_ATTRIBUTE add constraint FK_ExtensionAttribute_ExecutionPointer_ExecutionPointerId foreign key (EXECUTION_POINTER_ID)
      references PUREST_WF_EXECUTION_POINTER (PERSISTENCE_ID) on delete restrict on update restrict;

alter table PUREST_WF_EXECUTION_POINTER add constraint FK_ExecutionPointer_Workflow_WorkflowId foreign key (WORKFLOW_ID)
      references PUREST_WF_WORKFLOW (PERSISTENCE_ID) on delete restrict on update restrict;

