/*
 Navicat Premium Data Transfer

 Source Server         : liuxin
 Source Server Type    : SQL Server
 Source Server Version : 15002000
 Source Host           : .:1433
 Source Catalog        : CargoDb
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 15002000
 File Encoding         : 65001

 Date: 27/04/2023 17:02:35
*/


-- ----------------------------
-- Table structure for Cargo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Cargo]') AND type IN ('U'))
	DROP TABLE [dbo].[Cargo]
GO

CREATE TABLE [dbo].[Cargo] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Name] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [MaterialType] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Amount] int  NOT NULL,
  [Price] decimal(10,2)  NOT NULL,
  [Tag] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreateTime] datetime  NOT NULL,
  [UserId] int  NOT NULL,
  [UserName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[Cargo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'物资表',
'SCHEMA', N'dbo',
'TABLE', N'Cargo'
GO


-- ----------------------------
-- Table structure for CargoType
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[CargoType]') AND type IN ('U'))
	DROP TABLE [dbo].[CargoType]
GO

CREATE TABLE [dbo].[CargoType] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Name] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Tag] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreateTime] datetime  NULL,
  [UserId] int  NOT NULL,
  [UserName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[CargoType] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'物资类型表',
'SCHEMA', N'dbo',
'TABLE', N'CargoType'
GO


-- ----------------------------
-- Table structure for FlowMeter
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[FlowMeter]') AND type IN ('U'))
	DROP TABLE [dbo].[FlowMeter]
GO

CREATE TABLE [dbo].[FlowMeter] (
  [CargoId] int  IDENTITY(1,1) NOT NULL,
  [CargoName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Number] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Tag] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreateTime] datetime  NULL,
  [UserId] int  NOT NULL,
  [UserName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[FlowMeter] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'物资流水表',
'SCHEMA', N'dbo',
'TABLE', N'FlowMeter'
GO


-- ----------------------------
-- Table structure for User
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type IN ('U'))
	DROP TABLE [dbo].[User]
GO

CREATE TABLE [dbo].[User] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Name] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Password] nvarchar(32) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Role] nvarchar(32) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [CreateTime] datetime  NOT NULL
)
GO

ALTER TABLE [dbo].[User] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户表',
'SCHEMA', N'dbo',
'TABLE', N'User'
GO


-- ----------------------------
-- Auto increment value for Cargo
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Cargo]', RESEED, 23)
GO


-- ----------------------------
-- Primary Key structure for table Cargo
-- ----------------------------
ALTER TABLE [dbo].[Cargo] ADD CONSTRAINT [PK_Cargo_Id] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for CargoType
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[CargoType]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table CargoType
-- ----------------------------
ALTER TABLE [dbo].[CargoType] ADD CONSTRAINT [PK_CargoType_Id] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for FlowMeter
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[FlowMeter]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table FlowMeter
-- ----------------------------
ALTER TABLE [dbo].[FlowMeter] ADD CONSTRAINT [PK_FlowMeter_CargoId] PRIMARY KEY CLUSTERED ([CargoId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for User
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[User]', RESEED, 6)
GO


-- ----------------------------
-- Primary Key structure for table User
-- ----------------------------
ALTER TABLE [dbo].[User] ADD CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

