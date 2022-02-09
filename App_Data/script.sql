-- Script to create the database tables
CREATE TABLE [dbo].[Accounts] (
    [Id]                   CHAR (36)     NOT NULL,
    [first_name]           VARCHAR (50)  NULL,
    [last_name]            VARCHAR (50)  NULL,
    [email]                VARCHAR (255) NOT NULL,
    [emailConfirmed]       BIT           NULL,
    [isAdmin]              BIT           NOT NULL,
    [password]             VARCHAR (128) NOT NULL,
    [mobile_number]        CHAR (8)      NULL,
    [multi_factor_enabled] BIT           NULL,
    [secret_key]           VARCHAR (255) NULL,
    [address1]             TEXT          NULL,
    [address2]             TEXT          NULL,
    [zipcode]              CHAR (6)      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Category] (
    [Id]    INT          NOT NULL,
    [Name]  VARCHAR (50) NOT NULL,
    [count] INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Clothes] (
    [Id]          CHAR (36)     NOT NULL,
    [name]        VARCHAR (100) NOT NULL,
    [quantity]    INT           NOT NULL,
    [price]       MONEY         NOT NULL,
    [overview]    TEXT          NOT NULL,
    [gender]      CHAR (1)      NOT NULL,
    [category_id] INT           NOT NULL,
    [link]        VARCHAR (225) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Clothes_ToTable] FOREIGN KEY ([category_id]) REFERENCES [dbo].[Category] ([Id])
);

CREATE TABLE [dbo].[Countries] (
    [Id]      CHAR (36)    NOT NULL,
    [country] VARCHAR (75) NOT NULL,
    [count]   INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([country] ASC)
);

CREATE TABLE [dbo].[Ratings] (
    [Id]         CHAR (36)  NOT NULL,
    [account_id] CHAR (36)  NOT NULL,
    [product_id] CHAR (36)  NOT NULL,
    [stars]      FLOAT (53) NOT NULL,
    [review]     TEXT       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Ratings_Accounts] FOREIGN KEY ([account_id]) REFERENCES [dbo].[Accounts] ([Id]),
    CONSTRAINT [FK_Ratings_Clothes] FOREIGN KEY ([product_id]) REFERENCES [dbo].[Clothes] ([Id])
);

CREATE TABLE [dbo].[Sales] (
    [Id]          CHAR (36) NOT NULL,
    [paymentdate] DATE      NOT NULL,
    [amount]      MONEY     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

