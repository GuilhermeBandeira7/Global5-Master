CREATE DATABASE[Global5DBT]
GO

USE[Global5DBT] 
GO


CREATE TABLE [Fornecedor]
(
    [Id] INT NOT NULL IDENTITY(1,1),
    [CNPJ] NVARCHAR(20) NOT NULL UNIQUE,
    [RazaoSocial] NVARCHAR(120) NOT NULL,
    CONSTRAINT [PK_Fornecedor] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Fornecedor_CNPJ] ON [Fornecedor]([CNPJ])
GO

INSERT INTO Fornecedor VALUES 
('86.706.049/0001-81', 'Fornecedor 1'),
('65.837.066/0001-86', 'Fornecedor 2'),
('32.895.440/0001-40', 'Fornecedor 3');
GO

CREATE TABLE [Material]
(
    [Id] INT NOT NULL IDENTITY(1,1),
    [Codigo] NVARCHAR(20) NOT NULL,
    [Nome] NVARCHAR(50) NOT NULL,
    [UnidadeMedida] NVARCHAR(10) NOT NULL,
    CONSTRAINT [PK_Material] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Material_Codigo] ON [Material]([Codigo])
GO

INSERT INTO Material VALUES 
('10000A1', 'Produto 1','Kg'),
('10000A2', 'Produto 2', 'Kg'),
('10000A3', 'Produto 3', 'L');

CREATE TABLE [Estoque]
(
    [Id] INT NOT NULL IDENTITY(1,1),
    [Data] DATETIME NOT NULL,
    [FornecedorId] INT NOT NULL,
    [MaterialId] INT NOT NULL,
    [Quantidade] INT NOT NULL,
    [ValorTotal] MONEY NOT NULL,
    [TipoOperacao] NVARCHAR(20) NOT NULL,
    CONSTRAINT [PK_Estoque] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Estoque_Fornecedor] FOREIGN KEY([FornecedorId])
        REFERENCES [Fornecedor]([Id]),
    CONSTRAINT [FK_Estoque_Material] FOREIGN KEY([MaterialId])
        REFERENCES [Material]([Id])
);
GO

INSERT INTO Estoque VALUES
('2024-08-06 13:11:58.663', 1, 1, 50, 15500, 'Venda'),
('2024-08-07 13:11:58.663', 1, 2, 20, 80000, 'Venda'),
('2024-08-07 13:11:58.663', 2, 1, 50, 15500, 'Venda'),
('2024-08-08 13:11:58.663', 3, 3, 4, 60000, 'Venda');

-- UPDATE
CREATE PROCEDURE sp_update
     @TableName NVARCHAR(20),
     @ColumnName NVARCHAR(20),
     @Id INT,
     @NewValue NVARCHAR(50)
AS
BEGIN
     DECLARE @SQL NVARCHAR(150);

     SET @SQL = N'UPDATE ' + @TableName + 
               N' SET ' + @ColumnName + N' = @NewValue' +
               N' WHERE Id = @Id';
    EXEC sp_executesql @SQL,
                      N'@TableName NVARCHAR(128), @ColumnName NVARCHAR(128), @Id INT, @NewValue NVARCHAR(MAX)',
                      @TableName = @TableName,
                      @ColumnName = @ColumnName,
                      @NewValue = @NewValue;
END;

--GET
    
CREATE OR ALTER PROCEDURE sp_get
    @TableName NVARCHAR(20)
AS
BEGIN
     DECLARE @SQL NVARCHAR(150);

     SET @SQL = N'SELECT * FROM ' + @TableName;
    EXEC sp_executesql @SQL
END;

--DELETE
CREATE OR ALTER PROCEDURE sp_delete
     @TableName NVARCHAR(20),
     @Id INT
AS
BEGIN
     DECLARE @SQL NVARCHAR(150);

     SET @SQL = N'DELETE FROM ' + @TableName + 
               N' WHERE Id = @Id';
    EXEC sp_executesql @SQL,
     N'@TableName NVARCHAR(20), @Id INT',
                      @TableName = @TableName,
                      @Id = @Id;
END;

--INSERT 
CREATE OR ALTER PROCEDURE sp_insert_fornecedores
    @cnpj NVARCHAR(20),
    @razaoSocial NVARCHAR(120)
AS 
BEGIN
    INSERT INTO [Fornecedor] VALUES (@cnpj, @razaoSocial);
END;

CREATE OR ALTER PROCEDURE sp_insert_materiais
    @codigo NVARCHAR(20),
    @nome NVARCHAR(50),
    @unidadeMedida NVARCHAR(10)
AS 
BEGIN
    INSERT INTO [Material] VALUES (@codigo, @nome, @unidadeMedida);
END;

CREATE OR ALTER PROCEDURE sp_insert_estoque
    @datetime DATETIME,
    @fornecedorId INT,
    @materialId INT,
    @quantidade INT,
    @valorTotal MONEY,
    @tipoOperacao NVARCHAR(20)
AS 
BEGIN
     IF @quantidade <= 0 
        RAISERROR('Quantidade deve ser maior que zero', 16, 1);

    BEGIN TRY
        INSERT INTO [Estoque] ([Data], FornecedorId, MaterialId, Quantidade, ValorTotal, TipoOperacao)
        VALUES (@datetime, @fornecedorId, @materialId, @quantidade, @valorTotal, @tipoOperacao);
    END TRY
    BEGIN CATCH
        -- Registrar erro em um log (opcional)
        PRINT ERROR_MESSAGE();
    END CATCH
END;

CREATE OR ALTER PROCEDURE sp_get_by_id
    @TableName NVARCHAR(20),
    @Id INT
AS
BEGIN
     DECLARE @SQL NVARCHAR(150);

     SET @SQL = N'SELECT * FROM ' + @TableName + ' WHERE Id = @Id';
    EXEC sp_executesql @SQL,
    N'@TableName NVARCHAR(20), @Id INT',
                      @TableName = @TableName,
                      @Id = @Id;
END;

CREATE OR ALTER PROCEDURE sp_relatorio
    @fornecedorId INT,
    @materialId INT
AS
BEGIN
    SELECT
        FornecedorId AS FORNECEDOR, 
        MaterialId AS MATERIAL,
        SUM(Quantidade) AS QUANTIDADE, 
        (SUM(ValorTotal) / SUM(Quantidade)) AS PRECO_UNITARIO ,
        SUM(ValorTotal) AS TOTAL 
    FROM 
        Estoque 
    GROUP BY 
        FornecedorId,
        MaterialId
    HAVING 
        FornecedorId =  @fornecedorId and MaterialId = @materialId
END;










