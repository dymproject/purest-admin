/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2023/9/5 15:31:21                            */
/*==============================================================*/


drop table if exists PUREST_DICT_CATEGORY;

drop table if exists PUREST_DICT_DATA;

drop table if exists PUREST_FILE_RECORD;

drop table if exists PUREST_FUNCTION;

drop table if exists PUREST_FUNCTION_INTERFACE;

drop table if exists PUREST_INTERFACE;

drop table if exists PUREST_INTERFACE_GROUP;

drop table if exists PUREST_ORGANIZATION;

drop table if exists PUREST_ROLE;

drop table if exists PUREST_ROLE_FUNCTION;

drop table if exists PUREST_ROLE_INTERFACE;

drop table if exists PUREST_SYSTEM_CONFIG;

drop table if exists PUREST_USER;

drop table if exists PUREST_USER_ROLE;

/*==============================================================*/
/* Table: PUREST_DICT_CATEGORY                                  */
/*==============================================================*/
create table PUREST_DICT_CATEGORY
(
   ID                   numeric(19,0) not null comment '主键Id',
   CREATE_BY            numeric(19,0) not null comment '创建人',
   CREATE_TIME          datetime not null comment '创建时间',
   UPDATE_BY            numeric(19,0) comment '修改人',
   UPDATE_TIME          datetime comment '修改时间',
   REMARK               varchar(1000) comment '备注',
   NAME                 varchar(20) not null comment '分类名称',
   CODE                 varchar(40) not null comment '分类编码',
   primary key (ID),
   key AK_DIC_CODE (CODE)
);

alter table PUREST_DICT_CATEGORY comment '字典分类';

/*==============================================================*/
/* Table: PUREST_DICT_DATA                                      */
/*==============================================================*/
create table PUREST_DICT_DATA
(
   ID                   numeric(19,0) not null comment '主键Id',
   CREATE_BY            numeric(19,0) not null comment '创建人',
   CREATE_TIME          datetime not null comment '创建时间',
   UPDATE_BY            numeric(19,0) comment '修改人',
   UPDATE_TIME          datetime comment '修改时间',
   REMARK               varchar(1000) comment '备注',
   CATEGORY_ID          numeric(19,0) not null comment '字典分类ID',
   NAME                 varchar(20) not null comment '字典名称',
   SORT                 numeric(8,0) not null comment '排序',
   primary key (ID)
);

alter table PUREST_DICT_DATA comment '字典数据';

/*==============================================================*/
/* Table: PUREST_FILE_RECORD                                    */
/*==============================================================*/
create table PUREST_FILE_RECORD
(
   ID                   numeric(19,0) not null comment 'Id',
   CREATE_BY            numeric(19,0) not null comment '创建人',
   CREATE_TIME          datetime not null comment '创建时间',
   UPDATE_BY            numeric(19,0) comment '修改人',
   UPDATE_TIME          datetime comment '修改时间',
   REMARK               varchar(1000) comment '备注',
   FILE_NAME            varchar(1000) not null comment '文件名',
   FILE_SIZE            numeric(10) not null comment '文件大小',
   FILE_EXT             varchar(10) not null comment '文件扩展名',
   FULL_PATH            varchar(1000) not null comment '完整路径',
   primary key (ID)
);

alter table PUREST_FILE_RECORD comment '文件上传记录表';

/*==============================================================*/
/* Table: PUREST_FUNCTION                                       */
/*==============================================================*/
create table PUREST_FUNCTION
(
   ID                   numeric(19,0) not null comment '主键Id',
   CREATE_BY            numeric(19,0) not null comment '创建人',
   CREATE_TIME          datetime not null comment '创建时间',
   UPDATE_BY            numeric(19,0) comment '修改人',
   UPDATE_TIME          datetime comment '修改时间',
   REMARK               varchar(1000) comment '备注',
   NAME                 varchar(20) not null comment '名称',
   CODE                 varchar(40) not null comment '编码',
   PARENT_ID            numeric(19,0) comment '隶属于',
   primary key (ID)
);

alter table PUREST_FUNCTION comment '功能表';

/*==============================================================*/
/* Table: PUREST_FUNCTION_INTERFACE                             */
/*==============================================================*/
create table PUREST_FUNCTION_INTERFACE
(
   ID                   numeric(19,0) not null comment '主键Id',
   CREATE_BY            numeric(19,0) not null comment '创建人',
   CREATE_TIME          datetime not null comment '创建时间',
   UPDATE_BY            numeric(19,0) comment '修改人',
   UPDATE_TIME          datetime comment '修改时间',
   REMARK               varchar(1000) comment '备注',
   INTERFACE_ID         numeric(19,0) comment '接口ID',
   FUNCTION_ID          numeric(19,0) comment '功能ID',
   primary key (ID)
);

alter table PUREST_FUNCTION_INTERFACE comment '页面接口表';

/*==============================================================*/
/* Table: PUREST_INTERFACE                                      */
/*==============================================================*/
create table PUREST_INTERFACE
(
   ID                   numeric(19,0) not null comment '主键Id',
   CREATE_BY            numeric(19,0) not null comment '创建人',
   CREATE_TIME          datetime not null comment '创建时间',
   UPDATE_BY            numeric(19,0) comment '修改人',
   UPDATE_TIME          datetime comment '修改时间',
   REMARK               varchar(1000) comment '备注',
   NAME                 varchar(20) not null comment '接口名称',
   PATH                 varchar(200) not null comment '接口地址',
   REQUEST_METHOD       varchar(20) comment '请求方法',
   GROUP_ID             numeric(19,0) comment '接口分组ID',
   primary key (ID),
   key AK_Key_2 (PATH, REQUEST_METHOD)
);

alter table PUREST_INTERFACE comment '接口表';

/*==============================================================*/
/* Table: PUREST_INTERFACE_GROUP                                */
/*==============================================================*/
create table PUREST_INTERFACE_GROUP
(
   ID                   numeric(19,0) not null comment '主键Id',
   CREATE_BY            numeric(19,0) not null comment '创建人',
   CREATE_TIME          datetime not null comment '创建时间',
   UPDATE_BY            numeric(19,0) comment '修改人',
   UPDATE_TIME          datetime comment '修改时间',
   REMARK               varchar(1000) comment '备注',
   NAME                 varchar(20) comment '名称',
   CODE                 varchar(40) not null comment '编码',
   primary key (ID)
);

alter table PUREST_INTERFACE_GROUP comment '接口分组表';

/*==============================================================*/
/* Table: PUREST_ORGANIZATION                                   */
/*==============================================================*/
create table PUREST_ORGANIZATION
(
   ID                   numeric(19,0) not null comment '主键Id',
   CREATE_BY            numeric(19,0) not null comment '创建人',
   CREATE_TIME          datetime not null comment '创建时间',
   UPDATE_BY            numeric(19,0) comment '修改人',
   UPDATE_TIME          datetime comment '修改时间',
   REMARK               varchar(1000) comment '备注',
   NAME                 varchar(100) not null comment '名称',
   PARENT_ID            numeric(19,0) comment '父级Id',
   TELEPHONE            varchar(20) comment '联系电话',
   LEADER               varchar(20) comment '负责人',
   SORT                 int comment '排序',
   primary key (ID)
);

alter table PUREST_ORGANIZATION comment '组织机构';

/*==============================================================*/
/* Table: PUREST_ROLE                                           */
/*==============================================================*/
create table PUREST_ROLE
(
   ID                   numeric(19,0) not null comment '主键Id',
   CREATE_BY            numeric(19,0) not null comment '创建人',
   CREATE_TIME          datetime not null comment '创建时间',
   UPDATE_BY            numeric(19,0) comment '修改人',
   UPDATE_TIME          datetime comment '修改时间',
   REMARK               varchar(1000) comment '备注',
   NAME                 varchar(20) not null comment '角色名称',
   DESCRIPTION          varchar(200) comment '角色描述',
   primary key (ID),
   key AK_ROLE_ROLENAME (NAME)
);

alter table PUREST_ROLE comment '角色';

/*==============================================================*/
/* Table: PUREST_ROLE_FUNCTION                                  */
/*==============================================================*/
create table PUREST_ROLE_FUNCTION
(
   ID                   numeric(19,0) not null comment '主键Id',
   CREATE_BY            numeric(19,0) not null comment '创建人',
   CREATE_TIME          datetime not null comment '创建时间',
   UPDATE_BY            numeric(19,0) comment '修改人',
   UPDATE_TIME          datetime comment '修改时间',
   REMARK               varchar(1000) comment '备注',
   ROLE_ID              numeric(19,0) comment '角色ID',
   FUNCTION_ID          numeric(19,0) comment '功能ID',
   primary key (ID)
);

alter table PUREST_ROLE_FUNCTION comment '角色功能表';

/*==============================================================*/
/* Table: PUREST_ROLE_INTERFACE                                 */
/*==============================================================*/
create table PUREST_ROLE_INTERFACE
(
   ID                   numeric(19,0) not null comment '主键Id',
   CREATE_BY            numeric(19,0) not null comment '创建人',
   CREATE_TIME          datetime not null comment '创建时间',
   UPDATE_BY            numeric(19,0) comment '修改人',
   UPDATE_TIME          datetime comment '修改时间',
   REMARK               varchar(1000) comment '备注',
   ROLE_ID              numeric(19,0) comment '角色ID',
   INTERFACE_ID         numeric(19,0) comment '接口ID',
   primary key (ID)
);

alter table PUREST_ROLE_INTERFACE comment '角色接口表';

/*==============================================================*/
/* Table: PUREST_SYSTEM_CONFIG                                  */
/*==============================================================*/
create table PUREST_SYSTEM_CONFIG
(
   ID                   numeric(19,0) not null comment '主键Id',
   CREATE_BY            numeric(19,0) not null comment '创建人',
   CREATE_TIME          datetime not null comment '创建时间',
   UPDATE_BY            numeric(19,0) comment '修改人',
   UPDATE_TIME          datetime comment '修改时间',
   REMARK               varchar(1000) comment '备注',
   NAME                 varchar(20) comment '名称',
   CONFIG_CODE          varchar(40) comment '编码',
   CONFIG_VALUE         varchar(1000) comment '值',
   primary key (ID)
);

alter table PUREST_SYSTEM_CONFIG comment '系统配置表';

/*==============================================================*/
/* Table: PUREST_USER                                           */
/*==============================================================*/
create table PUREST_USER
(
   ID                   numeric(19,0) not null comment '主键Id',
   CREATE_BY            numeric(19,0) not null comment '创建人',
   CREATE_TIME          datetime not null comment '创建时间',
   UPDATE_BY            numeric(19,0) comment '修改人',
   UPDATE_TIME          datetime comment '修改时间',
   REMARK               varchar(1000) comment '备注',
   ACCOUNT              varchar(36) not null comment '账号',
   PASSWORD             varchar(100) not null comment '密码',
   NAME                 varchar(20) not null comment '真实姓名',
   TELEPHONE            varchar(11) comment '电话',
   EMAIL                varchar(20) comment '邮箱',
   AVATAR               longblob comment '头像',
   ORGANIZATION_ID      numeric(19,0) not null comment '组织机构Id',
   primary key (ID),
   key AK_USER_ACCOUNT (ACCOUNT)
);

alter table PUREST_USER comment '用户';

/*==============================================================*/
/* Table: PUREST_USER_ROLE                                      */
/*==============================================================*/
create table PUREST_USER_ROLE
(
   ID                   numeric(19,0) not null comment '主键Id',
   CREATE_BY            numeric(19,0) not null comment '创建人',
   CREATE_TIME          datetime not null comment '创建时间',
   UPDATE_BY            numeric(19,0) comment '修改人',
   UPDATE_TIME          datetime comment '修改时间',
   REMARK               varchar(1000) comment '备注',
   ROLE_ID              numeric(19,0) not null comment '角色ID',
   USER_ID              numeric(19,0) not null comment '用户ID',
   primary key (ID)
);

alter table PUREST_USER_ROLE comment '用户角色';

alter table PUREST_DICT_DATA add constraint FK_Reference_5 foreign key (CATEGORY_ID)
      references PUREST_DICT_CATEGORY (ID) on delete restrict on update restrict;

alter table PUREST_FUNCTION_INTERFACE add constraint FK_Reference_11 foreign key (FUNCTION_ID)
      references PUREST_FUNCTION (ID) on delete restrict on update restrict;

alter table PUREST_FUNCTION_INTERFACE add constraint FK_Reference_12 foreign key (INTERFACE_ID)
      references PUREST_INTERFACE (ID) on delete restrict on update restrict;

alter table PUREST_INTERFACE add constraint FK_Reference_17 foreign key (GROUP_ID)
      references PUREST_INTERFACE_GROUP (ID) on delete restrict on update restrict;

alter table PUREST_ORGANIZATION add constraint FK_Reference_8 foreign key (PARENT_ID)
      references PUREST_ORGANIZATION (ID) on delete restrict on update restrict;

alter table PUREST_ROLE_FUNCTION add constraint FK_Reference_15 foreign key (ROLE_ID)
      references PUREST_ROLE (ID) on delete restrict on update restrict;

alter table PUREST_ROLE_FUNCTION add constraint FK_Reference_16 foreign key (FUNCTION_ID)
      references PUREST_FUNCTION (ID) on delete restrict on update restrict;

alter table PUREST_ROLE_INTERFACE add constraint FK_Reference_13 foreign key (ROLE_ID)
      references PUREST_ROLE (ID) on delete restrict on update restrict;

alter table PUREST_ROLE_INTERFACE add constraint FK_Reference_14 foreign key (INTERFACE_ID)
      references PUREST_INTERFACE (ID) on delete restrict on update restrict;

alter table PUREST_USER add constraint FK_Reference_7 foreign key (ORGANIZATION_ID)
      references PUREST_ORGANIZATION (ID) on delete restrict on update restrict;

alter table PUREST_USER_ROLE add constraint FK_Reference_4 foreign key (ROLE_ID)
      references PUREST_ROLE (ID) on delete restrict on update restrict;

alter table PUREST_USER_ROLE add constraint FK_Reference_6 foreign key (USER_ID)
      references PUREST_USER (ID) on delete restrict on update restrict;

