CREATE TABLE events (
  [Id] int NOT NULL IDENTITY,
  [Title] varchar(max),
  [Description] varchar(max),
  [When] datetime2(0) NOT NULL,
  [Cost] int NOT NULL,
  PRIMARY KEY ([Id])
);

CREATE TABLE tickets (
  [Id] int NOT NULL IDENTITY,
  [UserID] int NOT NULL,
  [Type] int NOT NULL,
  [Title] varchar(max),
  [Message] varchar(max),
  [Timestamp] datetime2(0) NOT NULL,
  PRIMARY KEY ([Id])
);


CREATE TABLE families (
  [Id] int NOT NULL IDENTITY,
  [ParentID] int NOT NULL,
  [Token] varchar(max),
  PRIMARY KEY ([Id])
);

CREATE TABLE ideas (
  [Id] int NOT NULL IDENTITY,
  [Title] varchar(max),
  [Description] varchar(max),
  PRIMARY KEY ([Id])
);

CREATE TABLE invoices (
  [Id] int NOT NULL IDENTITY,
  [UserID] int NOT NULL,
  [Amount] int NOT NULL,
  [Timestamp] datetime2(0) NOT NULL,
  [EventIDs] varchar(max),
  [Payed] int NOT NULL,
  [AdditionsCount] int NOT NULL,
  [PayID] varchar(max),
  PRIMARY KEY ([Id])
);

CREATE TABLE logging (
  [Id] int NOT NULL IDENTITY,
  [Username] varchar(max),
  [Type] int NOT NULL,
  [Date] datetime2(0) NOT NULL,
  [Ip] varchar(max),
  PRIMARY KEY ([Id])
);

CREATE TABLE payments (
  [Id] int NOT NULL IDENTITY,
  [PaymentID] varchar(max),
  [InvoiceID] varchar(max),
  [UserID] int NOT NULL,
  [Status] int DEFAULT NULL,
  [PlacedAt] datetime2(0) NOT NULL,
  [PayedAt] datetime2(0) DEFAULT NULL,
  PRIMARY KEY ([Id])
);

CREATE TABLE accounts (
  [Id] int NOT NULL IDENTITY,
  [Sex] int NOT NULL,
  [Type] int NOT NULL,
  [Username] varchar(max) NOT NULL,
  [Password] varchar(max) NOT NULL,
  [Email] varchar(max) NOT NULL,
  [Firstname] varchar(max),
  [Lastname] varchar(max),
  [Token] varchar(max),
  [Adress] varchar(max),
  [Postalcode] varchar(max),
  [Residence] varchar(max),
  [Birthdate] datetime2(0) NOT NULL,
  [MollieID] varchar(max),
  [TwoFactor] varchar(max),
  [FamilyId] int DEFAULT NULL,
  [DenyTickets] int NOT NULL,
  PRIMARY KEY ([Id])
 ,
  CONSTRAINT [FK_Accounts_Families_FamilyId] FOREIGN KEY ([FamilyId]) REFERENCES families ([Id]) ON DELETE CASCADE
) ;

CREATE INDEX [IX_Accounts_FamilyId] ON accounts ([FamilyId]);

CREATE TABLE participate (
  [UserID] int NOT NULL,
  [EventID] int NOT NULL,
  [Payed] int NOT NULL,
  [ConsumptionTax] int NOT NULL,
  [ConsumptionCount] int NOT NULL,
  PRIMARY KEY ([EventID],[UserID])
 ,
  CONSTRAINT [FK_Participate_Accounts_UserID] FOREIGN KEY ([UserID]) REFERENCES accounts ([Id]) ON DELETE CASCADE,
  CONSTRAINT [FK_Participate_Events_EventID] FOREIGN KEY ([EventID]) REFERENCES events ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Participate_UserID] ON participate ([UserID]);


CREATE TABLE vote (
  [IdeaID] int NOT NULL,
  [UserID] int NOT NULL,
  [Upvote] int NOT NULL,
  PRIMARY KEY ([IdeaID],[UserID])
 ,
  CONSTRAINT [FK_Vote_Accounts_UserID] FOREIGN KEY ([UserID]) REFERENCES accounts ([Id]) ON DELETE CASCADE,
  CONSTRAINT [FK_Vote_Ideas_IdeaID] FOREIGN KEY ([IdeaID]) REFERENCES ideas ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Vote_UserID] ON vote ([UserID]);
