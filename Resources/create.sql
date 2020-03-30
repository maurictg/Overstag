DROP DATABASE IF EXISTS overstag
GO
CREATE DATABASE overstag
GO
USE overstag
GO


CREATE TABLE [Events] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [When] datetime2 NOT NULL,
    [Cost] int NOT NULL,
    [Type] tinyint NOT NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Families] (
    [Id] int NOT NULL IDENTITY,
    [ParentID] int NOT NULL,
    [Token] nvarchar(max) NULL,
    CONSTRAINT [PK_Families] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Files] (
    [Id] int NOT NULL IDENTITY,
    [Token] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [Mimetype] nvarchar(max) NULL,
    CONSTRAINT [PK_Files] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Ideas] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Cost] nvarchar(max) NULL,
    CONSTRAINT [PK_Ideas] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Accounts] (
    [Id] int NOT NULL IDENTITY,
    [Gender] bit NOT NULL,
    [Type] tinyint NOT NULL,
    [Username] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Firstname] nvarchar(max) NULL,
    [Lastname] nvarchar(max) NULL,
    [Phone] nvarchar(12) NULL,
    [Token] nvarchar(max) NULL,
    [Adress] nvarchar(max) NULL,
    [Postalcode] nvarchar(max) NULL,
    [Residence] nvarchar(max) NULL,
    [Birthdate] datetime2 NOT NULL,
    [RegisterDate] datetime2 NOT NULL,
    [MollieID] nvarchar(max) NULL,
    [TwoFactor] nvarchar(max) NULL,
    [TwoFactorCodes] nvarchar(max) NULL,
    [FamilyId] int NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Accounts_Families_FamilyId] FOREIGN KEY ([FamilyId]) REFERENCES [Families] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Auths] (
    [Id] int NOT NULL IDENTITY,
    [Token] nvarchar(max) NULL,
    [IP] nvarchar(max) NULL,
    [Registered] datetime2 NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Auths] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Auths_Accounts_UserId] FOREIGN KEY ([UserId]) REFERENCES [Accounts] ([Id]) ON DELETE NO ACTION                                                      
);

GO

CREATE TABLE [Invoices] (
    [Id] int NOT NULL IDENTITY,
    [Amount] int NOT NULL,
    [Timestamp] datetime2 NOT NULL,
    [EventIDs] nvarchar(max) NULL,
    [AdditionsCost] int NOT NULL,
    [Payed] bit NOT NULL,
    [InvoiceID] nvarchar(max) NULL,
    [UserID] int NOT NULL,
    CONSTRAINT [PK_Invoices] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Invoices_Accounts_UserID] FOREIGN KEY ([UserID]) REFERENCES [Accounts] ([Id]) ON DELETE NO ACTION                                                   
);

GO

CREATE TABLE [Participate] (
    [UserID] int NOT NULL,
    [EventID] int NOT NULL,
    [Payed] bit NOT NULL,
    [AdditionsCost] int NOT NULL,
    [FriendCount] tinyint NOT NULL,
    CONSTRAINT [PK_Participate] PRIMARY KEY ([EventID], [UserID]),
    CONSTRAINT [FK_Participate_Events_EventID] FOREIGN KEY ([EventID]) REFERENCES [Events] ([Id]) ON DELETE CASCADE,                                                       CONSTRAINT [FK_Participate_Accounts_UserID] FOREIGN KEY ([UserID]) REFERENCES [Accounts] ([Id]) ON DELETE CASCADE                                                  );

GO

CREATE TABLE [Transactions] (
    [Id] int NOT NULL IDENTITY,
    [Amount] int NOT NULL,
    [Type] int NULL,
    [When] datetime2 NOT NULL,
    [Timestamp] datetime2 NOT NULL,
    [Description] nvarchar(max) NULL,
    [Payed] bit NOT NULL,
    [Metadata] nvarchar(max) NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transactions_Accounts_UserId] FOREIGN KEY ([UserId]) REFERENCES [Accounts] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Vote] (
    [IdeaID] int NOT NULL,
    [UserID] int NOT NULL,
    [Upvote] bit NOT NULL,
    CONSTRAINT [PK_Vote] PRIMARY KEY ([IdeaID], [UserID]),
    CONSTRAINT [FK_Vote_Ideas_IdeaID] FOREIGN KEY ([IdeaID]) REFERENCES [Ideas] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Vote_Accounts_UserID] FOREIGN KEY ([UserID]) REFERENCES [Accounts] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Payments] (
    [Id] int NOT NULL IDENTITY,
    [PaymentID] nvarchar(max) NULL,
    [Status] int NULL,
    [PlacedAt] datetime2 NOT NULL,
    [PayedAt] datetime2 NULL,
    [UserId] int NULL,
    [InvoiceId] int NOT NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Payments_Invoices_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoices] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Payments_Accounts_UserId] FOREIGN KEY ([UserId]) REFERENCES [Accounts] ([Id]) ON DELETE NO ACTION
);

GO