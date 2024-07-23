CREATE TABLE [dbo].[UsersRuoli] (
    [IdUsersRuoli] INT IDENTITY (1, 1) NOT NULL,
    [IdUser]       INT NOT NULL,
    [IdRuolo]      INT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdUsersRuoli] ASC),
    CONSTRAINT [FK_UsersRuoli_Ruoli] FOREIGN KEY ([IdRuolo]) REFERENCES [dbo].[Ruoli] ([IdRuolo])
);

