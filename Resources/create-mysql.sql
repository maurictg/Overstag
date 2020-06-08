CREATE TABLE `Events` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `When` datetime(6) NOT NULL,
    `Cost` int NOT NULL,
    `Type` tinyint unsigned NOT NULL,
    CONSTRAINT `PK_Events` PRIMARY KEY (`Id`)
);

CREATE TABLE `Families` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ParentID` int NOT NULL,
    `Token` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Families` PRIMARY KEY (`Id`)
);

CREATE TABLE `Files` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Token` longtext CHARACTER SET utf8mb4 NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Mimetype` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Files` PRIMARY KEY (`Id`)
);

CREATE TABLE `Ideas` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Cost` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Ideas` PRIMARY KEY (`Id`)
);

CREATE TABLE `Accounts` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Gender` tinyint(1) NOT NULL,
    `Type` tinyint unsigned NOT NULL,
    `Username` longtext CHARACTER SET utf8mb4 NULL,
    `Password` longtext CHARACTER SET utf8mb4 NULL,
    `Email` longtext CHARACTER SET utf8mb4 NULL,
    `Firstname` longtext CHARACTER SET utf8mb4 NULL,
    `Lastname` longtext CHARACTER SET utf8mb4 NULL,
    `Phone` varchar(12) CHARACTER SET utf8mb4 NULL,
    `Token` longtext CHARACTER SET utf8mb4 NULL,
    `Adress` longtext CHARACTER SET utf8mb4 NULL,
    `Postalcode` longtext CHARACTER SET utf8mb4 NULL,
    `Residence` longtext CHARACTER SET utf8mb4 NULL,
    `Birthdate` datetime(6) NOT NULL,
    `RegisterDate` datetime(6) NOT NULL,
    `MollieID` longtext CHARACTER SET utf8mb4 NULL,
    `TwoFactor` longtext CHARACTER SET utf8mb4 NULL,
    `TwoFactorCodes` longtext CHARACTER SET utf8mb4 NULL,
    `FamilyId` int NULL,
    CONSTRAINT `PK_Accounts` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Accounts_Families_FamilyId` FOREIGN KEY (`FamilyId`) REFERENCES `Families` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Auths` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Token` longtext CHARACTER SET utf8mb4 NULL,
    `IP` longtext CHARACTER SET utf8mb4 NULL,
    `Registered` datetime(6) NOT NULL,
    `UserId` int NOT NULL,
    CONSTRAINT `PK_Auths` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Auths_Accounts_UserId` FOREIGN KEY (`UserId`) REFERENCES `Accounts` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Invoices` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Amount` int NOT NULL,
    `Timestamp` datetime(6) NOT NULL,
    `EventIDs` longtext CHARACTER SET utf8mb4 NULL,
    `AdditionsCost` int NOT NULL,
    `Paid` tinyint(1) NOT NULL,
    `InvoiceID` longtext CHARACTER SET utf8mb4 NULL,
    `UserID` int NOT NULL,
    CONSTRAINT `PK_Invoices` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Invoices_Accounts_UserID` FOREIGN KEY (`UserID`) REFERENCES `Accounts` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Participate` (
    `UserID` int NOT NULL,
    `EventID` int NOT NULL,
    `Paid` tinyint(1) NOT NULL,
    `AdditionsCost` int NOT NULL,
    `FriendCount` tinyint unsigned NOT NULL,
    CONSTRAINT `PK_Participate` PRIMARY KEY (`EventID`, `UserID`),
    CONSTRAINT `FK_Participate_Events_EventID` FOREIGN KEY (`EventID`) REFERENCES `Events` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Participate_Accounts_UserID` FOREIGN KEY (`UserID`) REFERENCES `Accounts` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Transactions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Amount` int NOT NULL,
    `Type` int NULL,
    `When` datetime(6) NOT NULL,
    `Timestamp` datetime(6) NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `Paid` tinyint(1) NOT NULL,
    `Metadata` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` int NOT NULL,
    CONSTRAINT `PK_Transactions` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Transactions_Accounts_UserId` FOREIGN KEY (`UserId`) REFERENCES `Accounts` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Vote` (
    `IdeaID` int NOT NULL,
    `UserID` int NOT NULL,
    `Upvote` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Vote` PRIMARY KEY (`IdeaID`, `UserID`),
    CONSTRAINT `FK_Vote_Ideas_IdeaID` FOREIGN KEY (`IdeaID`) REFERENCES `Ideas` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Vote_Accounts_UserID` FOREIGN KEY (`UserID`) REFERENCES `Accounts` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Payments` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `PaymentId` longtext CHARACTER SET utf8mb4 NULL,
    `PlacedAt` datetime(6) NOT NULL,
    `PaidAt` datetime(6) NULL,
    `PayType` int NOT NULL,
    `Status` int NULL,
    `Metadata` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` int NOT NULL,
    `InvoiceId` int NOT NULL,
    CONSTRAINT `PK_Payments` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Payments_Invoices_InvoiceId` FOREIGN KEY (`InvoiceId`) REFERENCES `Invoices` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Payments_Accounts_UserId` FOREIGN KEY (`UserId`) REFERENCES `Accounts` (`Id`) ON DELETE RESTRICT
);

CREATE INDEX `IX_Accounts_FamilyId` ON `Accounts` (`FamilyId`);
CREATE INDEX `IX_Auths_UserId` ON `Auths` (`UserId`);
CREATE INDEX `IX_Invoices_UserID` ON `Invoices` (`UserID`);
CREATE INDEX `IX_Participate_UserID` ON `Participate` (`UserID`);
CREATE UNIQUE INDEX `IX_Payments_InvoiceId` ON `Payments` (`InvoiceId`);
CREATE INDEX `IX_Payments_UserId` ON `Payments` (`UserId`);
CREATE INDEX `IX_Transactions_UserId` ON `Transactions` (`UserId`);
CREATE INDEX `IX_Vote_UserID` ON `Vote` (`UserID`);