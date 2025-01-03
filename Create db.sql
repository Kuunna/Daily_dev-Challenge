CREATE TABLE [Provider] (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [Name] NVARCHAR(255),
  [Source] NVARCHAR(255),
  [ProcessId] INT NULL, 
  [Status] NVARCHAR(50) DEFAULT 'Not Started', 
  [ProcessAt] DATETIME NULL
)
GO

CREATE TABLE [Category] (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [Name] NVARCHAR(255),
  [ProviderId] INT,
  [Source] NVARCHAR(255)
)
GO

CREATE TABLE [Item] (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [Title] NVARCHAR(255),
  [Link] NVARCHAR(255),
  [Guid] NVARCHAR(255),
  [PubDate] DATETIME,
  [Image] NVARCHAR(255),
  [CategoryId] INT,
  [author] NVARCHAR(255),
  [summary] NVARCHAR(MAX),
  [comments] NVARCHAR(255)
)
GO

CREATE TABLE [Tag] (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [Name] NVARCHAR(255),
  [description] NVARCHAR(255)
)
GO

CREATE TABLE [NewTag] (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [NewId] INT,
  [TagId] INT
)
  
GO

CREATE TABLE [User] (
    Id INT PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(100) UNIQUE,
    Password NVARCHAR(256),
    Email NVARCHAR(100),
    FullName NVARCHAR(100),
    DOB DATE,
    --Role NVARCHAR(50) CHECK (Role IN ('Admin', 'RegisterUser', 'ClientUser')) -- Thêm Role để phân quyền
);

GO

CREATE TABLE [UserCategory] (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [UserId] INT,
  [CategoryId] INT
)
GO

CREATE TABLE [UserProvider] (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [UserId] INT,
  [CategoryId] INT
)
GO

CREATE TABLE [UserTag] (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [UserId] INT,
  [TagId] INT
)
GO

CREATE TABLE UserItem (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT,
    ItemId INT,
    IsLiked BIT DEFAULT 0,
    IsBookmarked BIT DEFAULT 0,
    LikeDate DATETIME NULL,
    BookmarkDate DATETIME NULL
)

CREATE TABLE ItemComment (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT,
    ItemId INT,
    Content NVARCHAR(255),
    ParentId INT,
    CreateAt DATETIME
)

CREATE TABLE [TableConfig] (
  [Id] INT IDENTITY(1,1) PRIMARY KEY,
  [UserId] INT,
  [MostLiked] INT,
  [MostRead] INT,
  [MostTagged] INT,
  [FavoriteCategory] INT
)