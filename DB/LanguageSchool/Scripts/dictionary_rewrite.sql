DROP TABLE [Administration].[Holidays]
DROP TABLE [Users].[WorkTimes]
GO

ALTER TABLE Administration.[Messages] DROP CONSTRAINT FK_Messages_Roles ;
ALTER TABLE Administration.[Messages] DROP CONSTRAINT FK_Messages_MessageTypes ;
ALTER TABLE Users.Users DROP CONSTRAINT FK_Users_Roles ;
ALTER TABLE Courses.Courses DROP CONSTRAINT FK_Courses_LanguageProficencies ;
ALTER TABLE Courses.GroupTimes DROP CONSTRAINT FK_GroupTimes_DaysOfWeek ;
ALTER TABLE Users.UsersTests DROP CONSTRAINT FK_UsersTests_Marks ;
GO

DROP TABLE [Users].[Roles]
DROP TABLE [Administration].[MessageTypes]
DROP TABLE [Administration].[LanguageProficencies]
DROP TABLE [Administration].[DaysOfWeek]
DROP TABLE [Administration].[Marks]

CREATE TABLE [Administration].[DictionaryItems](
	[Id] [int] NOT NULL,
	[PLName] [nvarchar](30) NOT NULL,
	[ENName] [nvarchar](30) NOT NULL,
	[PLDescription] [nvarchar](250) NULL,
	[ENDDescription] [nvarchar](250) NULL,
 CONSTRAINT [PK_DictionaryItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE Administration.[Messages] ADD CONSTRAINT FK_Messages_Roles FOREIGN KEY (RoleId) REFERENCES [Administration].[DictionaryItems](Id);
ALTER TABLE Users.Users ADD CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES [Administration].[DictionaryItems](Id);
ALTER TABLE Administration.[Messages] ADD CONSTRAINT FK_Messages_MessageTypes FOREIGN KEY (MessageTypeId) REFERENCES [Administration].[DictionaryItems](Id);
ALTER TABLE Courses.Courses ADD CONSTRAINT FK_Courses_LanguageProficencies FOREIGN KEY (LanguageProficencyId) REFERENCES [Administration].[DictionaryItems](Id);
ALTER TABLE Courses.GroupTimes ADD CONSTRAINT FK_GroupTimes_DaysOfWeek FOREIGN KEY (DayOfWeekId) REFERENCES [Administration].[DictionaryItems](Id);
ALTER TABLE Users.UsersTests ADD CONSTRAINT FK_UsersTests_Marks FOREIGN KEY (MarkId) REFERENCES [Administration].[DictionaryItems](Id);