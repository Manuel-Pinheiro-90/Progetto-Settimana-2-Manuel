CREATE TABLE [dbo].[Users] (
    [IdUser]       INT            IDENTITY (1, 1) NOT NULL,
    [Username]     NVARCHAR (50)  NOT NULL,
    [PasswordHash] NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdUser] ASC)
);

