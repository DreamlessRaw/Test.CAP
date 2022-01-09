create database CAP;
use CAP;
create table Account
(
    Id         bigint        not null primary key,
    Code       nvarchar(50)  not null,
    Name       nvarchar(50)  not null,
    Password   nvarchar(50)  not null,
    CreateId   bigint        not null,
    CreateName nvarchar(50)  not null,
    CreateTime DATETIME2     not null,
    ModifyId   bigint        not null,
    ModifyName nvarchar(50)  not null,
    ModifyTime DATETIME2     not null,
    IsDelete   bit default 0 not null,
);

create table LoginLog
(
    Id         bigint        not null primary key,
    AccountId  bigint        not null,
    CreateId   bigint        not null,
    CreateName nvarchar(50)  not null,
    CreateTime DATETIME2     not null,
    ModifyId   bigint        not null,
    ModifyName nvarchar(50)  not null,
    ModifyTime DATETIME2     not null,
    IsDelete   bit default 0 not null,
);

