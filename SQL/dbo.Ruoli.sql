CREATE TABLE [dbo].[Ruoli] (
    [IdRuolo] INT           IDENTITY (1, 1) NOT NULL,
    [Nome]    NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdRuolo] ASC),
    UNIQUE NONCLUSTERED ([Nome] ASC)
);

