/*
 Navicat Premium Data Transfer

 Source Server         : mysql-localhost
 Source Server Type    : MySQL
 Source Server Version : 80011
 Source Host           : localhost:3306
 Source Schema         : purest

 Target Server Type    : MySQL
 Target Server Version : 80011
 File Encoding         : 65001

 Date: 12/09/2025 13:08:18
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for purest_background_job_record
-- ----------------------------
DROP TABLE IF EXISTS `purest_background_job_record`;
CREATE TABLE `purest_background_job_record`  (
  `ID` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'Id',
  `JOB_NAME` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '名称',
  `JOB_ARGS` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '参数',
  `TRY_COUNT` decimal(10, 0) NULL DEFAULT NULL COMMENT '重试次数',
  `CREATION_TIME` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
  `NEXT_TRY_TIME` datetime(0) NULL DEFAULT NULL COMMENT '下次执行时间',
  `LAST_TRY_TIME` datetime(0) NULL DEFAULT NULL COMMENT '最后执行时间',
  `IS_ABANDONED` decimal(1, 0) NULL DEFAULT NULL COMMENT '是否超时',
  `PRIORITY` decimal(10, 0) NULL DEFAULT NULL COMMENT '优先级',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '后台作业记录表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_dict_category
-- ----------------------------
DROP TABLE IF EXISTS `purest_dict_category`;
CREATE TABLE `purest_dict_category`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `NAME` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '分类名称',
  `CODE` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '分类编码',
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `UK_PUREST_CATEGORY_CODE`(`CODE`) USING BTREE,
  INDEX `ID`(`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '字典分类' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_dict_data
-- ----------------------------
DROP TABLE IF EXISTS `purest_dict_data`;
CREATE TABLE `purest_dict_data`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `CATEGORY_ID` bigint(20) NOT NULL COMMENT '字典分类ID',
  `NAME` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '字典名称',
  `CODE` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '编码',
  `SORT` decimal(10, 0) NOT NULL COMMENT '排序',
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `FK_Reference_5`(`CATEGORY_ID`) USING BTREE,
  CONSTRAINT `FK_Reference_5` FOREIGN KEY (`CATEGORY_ID`) REFERENCES `purest_dict_category` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '字典数据' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_file_record
-- ----------------------------
DROP TABLE IF EXISTS `purest_file_record`;
CREATE TABLE `purest_file_record`  (
  `ID` bigint(20) NOT NULL COMMENT 'Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `FILE_NAME` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '文件名',
  `FILE_SIZE` decimal(10, 0) NOT NULL COMMENT '文件大小',
  `FILE_EXT` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '文件扩展名',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '文件上传记录表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_function
-- ----------------------------
DROP TABLE IF EXISTS `purest_function`;
CREATE TABLE `purest_function`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `NAME` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '名称',
  `CODE` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '编码',
  `PARENT_ID` bigint(20) NULL DEFAULT NULL COMMENT '隶属于',
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `UK_PUREST_FUNCTION_CODE`(`CODE`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '功能表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_function_interface
-- ----------------------------
DROP TABLE IF EXISTS `purest_function_interface`;
CREATE TABLE `purest_function_interface`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `INTERFACE_ID` bigint(20) NULL DEFAULT NULL COMMENT '接口ID',
  `FUNCTION_ID` bigint(20) NULL DEFAULT NULL COMMENT '功能ID',
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `FK_Reference_11`(`FUNCTION_ID`) USING BTREE,
  INDEX `FK_Reference_12`(`INTERFACE_ID`) USING BTREE,
  CONSTRAINT `FK_Reference_11` FOREIGN KEY (`FUNCTION_ID`) REFERENCES `purest_function` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_Reference_12` FOREIGN KEY (`INTERFACE_ID`) REFERENCES `purest_interface` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '页面接口表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_interface
-- ----------------------------
DROP TABLE IF EXISTS `purest_interface`;
CREATE TABLE `purest_interface`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `NAME` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '接口名称',
  `PATH` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '接口地址',
  `REQUEST_METHOD` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '请求方法',
  `GROUP_ID` bigint(20) NULL DEFAULT NULL COMMENT '接口分组ID',
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `UK_INTERFACE_PATHMETHOD`(`PATH`, `REQUEST_METHOD`) USING BTREE,
  INDEX `FK_Reference_17`(`GROUP_ID`) USING BTREE,
  CONSTRAINT `FK_Reference_17` FOREIGN KEY (`GROUP_ID`) REFERENCES `purest_interface_group` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '接口表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_interface_group
-- ----------------------------
DROP TABLE IF EXISTS `purest_interface_group`;
CREATE TABLE `purest_interface_group`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `NAME` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '名称',
  `CODE` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '编码',
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `UK_PUREST_IG_CODE`(`CODE`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '接口分组表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_notice
-- ----------------------------
DROP TABLE IF EXISTS `purest_notice`;
CREATE TABLE `purest_notice`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `TITLE` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '主题',
  `CONTENT` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL COMMENT '内容',
  `NOTICE_TYPE` bigint(20) NOT NULL COMMENT '类型',
  `LEVEL` bigint(20) NULL DEFAULT NULL COMMENT '级别',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '通知公告表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_notice_record
-- ----------------------------
DROP TABLE IF EXISTS `purest_notice_record`;
CREATE TABLE `purest_notice_record`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `RECEIVER` bigint(20) NOT NULL COMMENT '接收人',
  `IS_READ` decimal(1, 0) NOT NULL COMMENT '是否已读',
  `NOTICE_ID` bigint(20) NOT NULL COMMENT '通知公告Id',
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `FK_Reference_13`(`NOTICE_ID`) USING BTREE,
  CONSTRAINT `FK_Reference_13` FOREIGN KEY (`NOTICE_ID`) REFERENCES `purest_notice` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '通知公告记录表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_oauth2_user
-- ----------------------------
DROP TABLE IF EXISTS `purest_oauth2_user`;
CREATE TABLE `purest_oauth2_user`  (
  `PERSISTENCE_ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `ID` bigint(20) NULL DEFAULT NULL COMMENT 'ID',
  `NAME` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '认证中心名',
  `TYPE` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT 'TYPE',
  `USER_ID` bigint(20) NULL DEFAULT NULL COMMENT '用户ID',
  PRIMARY KEY (`PERSISTENCE_ID`) USING BTREE,
  INDEX `FK_Reference_26`(`USER_ID`) USING BTREE,
  CONSTRAINT `FK_Reference_26` FOREIGN KEY (`USER_ID`) REFERENCES `purest_user` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = 'OAUTH2用户' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_organization
-- ----------------------------
DROP TABLE IF EXISTS `purest_organization`;
CREATE TABLE `purest_organization`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `NAME` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '名称',
  `PARENT_ID` bigint(20) NULL DEFAULT NULL COMMENT '父级ID',
  `TELEPHONE` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '联系电话',
  `LEADER` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '负责人',
  `SORT` decimal(10, 0) NULL DEFAULT NULL COMMENT '排序',
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `UK_PUREST_ORG_NAME_PID`(`NAME`, `PARENT_ID`) USING BTREE,
  INDEX `FK_Reference_14`(`PARENT_ID`) USING BTREE,
  CONSTRAINT `FK_Reference_14` FOREIGN KEY (`PARENT_ID`) REFERENCES `purest_organization` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '组织机构' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_profile_system
-- ----------------------------
DROP TABLE IF EXISTS `purest_profile_system`;
CREATE TABLE `purest_profile_system`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `NAME` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '名称',
  `CODE` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '编码',
  `FILE_ID` bigint(20) NOT NULL COMMENT '文件ID',
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `UK_PUREST_FILESYSTEM_CODE`(`CODE`) USING BTREE,
  INDEX `FK_Reference_18`(`FILE_ID`) USING BTREE,
  CONSTRAINT `FK_Reference_18` FOREIGN KEY (`FILE_ID`) REFERENCES `purest_file_record` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '系统文件表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_request_log
-- ----------------------------
DROP TABLE IF EXISTS `purest_request_log`;
CREATE TABLE `purest_request_log`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `CONTROLLER_NAME` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '控制器',
  `ACTION_NAME` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '方法名',
  `REQUEST_METHOD` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '请求类型',
  `ENVIRONMENT_NAME` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '服务器环境',
  `ELAPSED_TIME` decimal(16, 0) NULL DEFAULT NULL COMMENT '执行耗时',
  `CLIENT_IP` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '客户端IP',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '请求日志表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_role
-- ----------------------------
DROP TABLE IF EXISTS `purest_role`;
CREATE TABLE `purest_role`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `NAME` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '角色名称',
  `DESCRIPTION` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '角色描述',
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `UK_PUREST_ROLE_NAME`(`NAME`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '角色' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_role_function
-- ----------------------------
DROP TABLE IF EXISTS `purest_role_function`;
CREATE TABLE `purest_role_function`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `ROLE_ID` bigint(20) NULL DEFAULT NULL COMMENT '角色ID',
  `FUNCTION_ID` bigint(20) NULL DEFAULT NULL COMMENT '功能ID',
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `FK_Reference_15`(`ROLE_ID`) USING BTREE,
  INDEX `FK_Reference_16`(`FUNCTION_ID`) USING BTREE,
  CONSTRAINT `FK_Reference_15` FOREIGN KEY (`ROLE_ID`) REFERENCES `purest_role` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_Reference_16` FOREIGN KEY (`FUNCTION_ID`) REFERENCES `purest_function` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '角色功能表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_system_config
-- ----------------------------
DROP TABLE IF EXISTS `purest_system_config`;
CREATE TABLE `purest_system_config`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `NAME` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '名称',
  `CONFIG_CODE` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '编码',
  `CONFIG_VALUE` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '值',
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `UK_PUREST_CONFIG_CODE`(`CONFIG_CODE`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '系统配置表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_user
-- ----------------------------
DROP TABLE IF EXISTS `purest_user`;
CREATE TABLE `purest_user`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `ACCOUNT` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '账号',
  `PASSWORD` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '密码',
  `NAME` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '真实姓名',
  `TELEPHONE` varchar(11) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '电话',
  `EMAIL` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '邮箱',
  `AVATAR` longblob NULL COMMENT '头像',
  `STATUS` decimal(10, 0) NULL DEFAULT NULL COMMENT '状态',
  `ORGANIZATION_ID` bigint(20) NOT NULL COMMENT '组织机构Id',
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `UK_PUREST_USER_ACCOUNT`(`ACCOUNT`) USING BTREE,
  INDEX `FK_Reference_7`(`ORGANIZATION_ID`) USING BTREE,
  CONSTRAINT `FK_Reference_7` FOREIGN KEY (`ORGANIZATION_ID`) REFERENCES `purest_organization` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '用户' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_user_role
-- ----------------------------
DROP TABLE IF EXISTS `purest_user_role`;
CREATE TABLE `purest_user_role`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `ROLE_ID` bigint(20) NOT NULL COMMENT '角色ID',
  `USER_ID` bigint(20) NOT NULL COMMENT '用户ID',
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `FK_Reference_4`(`ROLE_ID`) USING BTREE,
  INDEX `FK_Reference_6`(`USER_ID`) USING BTREE,
  CONSTRAINT `FK_Reference_4` FOREIGN KEY (`ROLE_ID`) REFERENCES `purest_role` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_Reference_6` FOREIGN KEY (`USER_ID`) REFERENCES `purest_user` (`id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '用户角色' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_wf_auditing_record
-- ----------------------------
DROP TABLE IF EXISTS `purest_wf_auditing_record`;
CREATE TABLE `purest_wf_auditing_record`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `EXECUTION_POINTER_ID` bigint(20) NOT NULL COMMENT '步骤Id',
  `AUDITING_TIME` datetime(0) NOT NULL COMMENT '审批时间',
  `AUDITOR` bigint(20) NOT NULL COMMENT '审批人',
  `AUDITOR_NAME` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '审批人姓名',
  `AUDITING_OPINION` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL COMMENT '审批意见',
  `IS_AGREE` tinyint(1) NOT NULL COMMENT '是否同意',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '流程审批记录' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_wf_definition
-- ----------------------------
DROP TABLE IF EXISTS `purest_wf_definition`;
CREATE TABLE `purest_wf_definition`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `NAME` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '名称',
  `DEFINITION_ID` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '定义ID',
  `WORKFLOW_CONTENT` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '流程内容',
  `DESIGNS_CONTENT` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '设计器内容',
  `FORM_CONTENT` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '表单内容',
  `VERSION` int(11) NOT NULL COMMENT '版本',
  `IS_LOCKED` tinyint(1) NOT NULL COMMENT '是否锁定',
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `UK_WORKFLOW_CODE`(`DEFINITION_ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '流程定义' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_wf_event
-- ----------------------------
DROP TABLE IF EXISTS `purest_wf_event`;
CREATE TABLE `purest_wf_event`  (
  `PERSISTENCE_ID` bigint(20) NOT NULL,
  `EVENT_ID` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EVENT_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `EVENT_KEY` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `EVENT_DATA` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `EVENT_TIME` datetime(0) NOT NULL,
  `IS_PROCESSED` tinyint(1) NOT NULL,
  PRIMARY KEY (`PERSISTENCE_ID`) USING BTREE,
  UNIQUE INDEX `IX_Event_EventId`(`EVENT_ID`) USING BTREE,
  INDEX `IX_Event_EventTime`(`EVENT_TIME`) USING BTREE,
  INDEX `IX_Event_IsProcessed`(`IS_PROCESSED`) USING BTREE,
  INDEX `IX_Event_EventName_EventKey`(`EVENT_NAME`, `EVENT_KEY`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '事件' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_wf_execution_attribute
-- ----------------------------
DROP TABLE IF EXISTS `purest_wf_execution_attribute`;
CREATE TABLE `purest_wf_execution_attribute`  (
  `PERSISTENCE_ID` bigint(20) NOT NULL,
  `ATTRIBUTE_KEY` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ATTRIBUTE_VALUE` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `EXECUTION_POINTER_ID` bigint(20) NOT NULL,
  PRIMARY KEY (`PERSISTENCE_ID`) USING BTREE,
  INDEX `IX_ExtensionAttribute_ExecutionPointerId`(`EXECUTION_POINTER_ID`) USING BTREE,
  CONSTRAINT `FK_ExtensionAttribute_ExecutionPointer_ExecutionPointerId` FOREIGN KEY (`EXECUTION_POINTER_ID`) REFERENCES `purest_wf_execution_pointer` (`persistence_id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '自定义属性' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_wf_execution_error
-- ----------------------------
DROP TABLE IF EXISTS `purest_wf_execution_error`;
CREATE TABLE `purest_wf_execution_error`  (
  `PERSISTENCE_ID` bigint(20) NOT NULL,
  `ERROR_TIME` date NOT NULL,
  `EXECUTION_POINTER_ID` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `MESSAGE` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `WORKFLOW_ID` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`PERSISTENCE_ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '执行异常' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_wf_execution_pointer
-- ----------------------------
DROP TABLE IF EXISTS `purest_wf_execution_pointer`;
CREATE TABLE `purest_wf_execution_pointer`  (
  `PERSISTENCE_ID` bigint(20) NOT NULL,
  `WORKFLOW_ID` bigint(20) NOT NULL,
  `ID` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `START_TIME` datetime(0) NULL DEFAULT NULL,
  `END_TIME` datetime(0) NULL DEFAULT NULL,
  `ACTIVE` tinyint(1) NOT NULL,
  `EVENT_KEY` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `EVENT_NAME` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `EVENT_DATA` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `EVENT_PUBLISHED` tinyint(1) NOT NULL,
  `PERSISTENCE_DATA` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `SLEEP_UNTIL` datetime(0) NULL DEFAULT NULL,
  `STEP_ID` int(11) NOT NULL,
  `STEP_NAME` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CHILDREN` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CONTEXT_ITEM` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `PREDECESSOR_ID` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `OUTCOME` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `SCOPE` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `RETRY_COUNT` int(11) NOT NULL,
  `STATUS` int(11) NOT NULL,
  PRIMARY KEY (`PERSISTENCE_ID`) USING BTREE,
  INDEX `IX_ExecutionPointer_WorkflowId`(`WORKFLOW_ID`) USING BTREE,
  CONSTRAINT `FK_ExecutionPointer_Workflow_WorkflowId` FOREIGN KEY (`WORKFLOW_ID`) REFERENCES `purest_wf_workflow` (`persistence_id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '步骤' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_wf_scheduled_command
-- ----------------------------
DROP TABLE IF EXISTS `purest_wf_scheduled_command`;
CREATE TABLE `purest_wf_scheduled_command`  (
  `PERSISTENCE_ID` bigint(20) NOT NULL,
  `COMMAND_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DATA` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `EXECUTE_TIME` bigint(20) NOT NULL,
  PRIMARY KEY (`PERSISTENCE_ID`) USING BTREE,
  UNIQUE INDEX `IX_ScheduledCommand_CommandName_Data`(`COMMAND_NAME`, `DATA`) USING BTREE,
  INDEX `IX_ScheduledCommand_ExecuteTime`(`EXECUTE_TIME`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '计划命令' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_wf_subscription
-- ----------------------------
DROP TABLE IF EXISTS `purest_wf_subscription`;
CREATE TABLE `purest_wf_subscription`  (
  `PERSISTENCE_ID` bigint(20) NOT NULL,
  `EVENT_KEY` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `EVENT_NAME` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `STEP_ID` int(11) NOT NULL,
  `SUBSCRIPTION_ID` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `WORKFLOW_ID` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `SUBSCRIBE_AS_OF` datetime(0) NOT NULL,
  `SUBSCRIPTION_DATA` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `EXECUTION_POINTER_ID` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `EXTERNAL_TOKEN` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `EXTERNAL_TOKEN_EXPIRY` datetime(0) NULL DEFAULT NULL,
  `EXTERNAL_WORKER_ID` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`PERSISTENCE_ID`) USING BTREE,
  UNIQUE INDEX `IX_Subscription_SubscriptionId`(`SUBSCRIPTION_ID`) USING BTREE,
  INDEX `IX_Subscription_EventKey`(`EVENT_KEY`) USING BTREE,
  INDEX `IX_Subscription_EventName`(`EVENT_NAME`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '订阅' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_wf_waiting_pointer
-- ----------------------------
DROP TABLE IF EXISTS `purest_wf_waiting_pointer`;
CREATE TABLE `purest_wf_waiting_pointer`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `USER_ID` bigint(20) NOT NULL COMMENT '用户Id',
  `POINTER_ID` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '步骤Id',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '待审核步骤' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_wf_workflow
-- ----------------------------
DROP TABLE IF EXISTS `purest_wf_workflow`;
CREATE TABLE `purest_wf_workflow`  (
  `PERSISTENCE_ID` bigint(20) NOT NULL,
  `COMPLETE_TIME` datetime(0) NULL DEFAULT NULL,
  `CREATE_BY` bigint(20) NOT NULL,
  `CREATE_TIME` datetime(0) NOT NULL,
  `DATA` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `DESCRIPTION` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `INSTANCE_ID` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `NEXT_EXECUTION` bigint(20) NULL DEFAULT NULL,
  `STATUS` int(11) NOT NULL,
  `VERSION` int(11) NOT NULL,
  `WORKFLOW_DEFINITION_ID` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `REFERENCE` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`PERSISTENCE_ID`) USING BTREE,
  INDEX `IX_Workflow_InstanceId`(`INSTANCE_ID`) USING BTREE,
  INDEX `IX_Workflow_NextExecution`(`NEXT_EXECUTION`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '工作流程' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for purest_workflow_instance
-- ----------------------------
DROP TABLE IF EXISTS `purest_workflow_instance`;
CREATE TABLE `purest_workflow_instance`  (
  `ID` bigint(20) NOT NULL COMMENT '主键Id',
  `CREATE_BY` bigint(20) NOT NULL COMMENT '创建人',
  `CREATE_TIME` datetime(0) NOT NULL COMMENT '创建时间',
  `UPDATE_BY` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UPDATE_TIME` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  `REMARK` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '备注',
  `WF_ID` bigint(20) NOT NULL COMMENT '流程ID',
  `SCHEME_ID` bigint(20) NOT NULL COMMENT '设计ID',
  `FORM_DATA` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '表单值',
  `CURRENT_NODE` bigint(20) NULL DEFAULT NULL COMMENT '当前节点',
  `CURRENT_NODE_TYPE` int(11) NOT NULL COMMENT '当前节点类型',
  `STATUS` int(11) NOT NULL COMMENT '状态',
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `FK_Reference_23`(`WF_ID`) USING BTREE,
  INDEX `FK_Reference_24`(`SCHEME_ID`) USING BTREE,
  CONSTRAINT `FK_Reference_23` FOREIGN KEY (`WF_ID`) REFERENCES `purest_wf_workflow` (`persistence_id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Reference_24` FOREIGN KEY (`SCHEME_ID`) REFERENCES `purest_wf_definition` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '流程实例' ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
