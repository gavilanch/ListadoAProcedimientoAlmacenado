/*Crea el tipo*/
CREATE TYPE ListadoEnteros as Table (Id int);

/*Crea la tabla de ejemplo*/
CREATE TABLE [dbo].[Valores](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Valor] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Valores] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/*Crea el procedimiento almacenado*/
CREATE PROCEDURE Valores_ObtenerListado
    @ListadoIDs ListadoEnteros READONLY
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
 
    SELECT *
    FROM Valores
    WHERE Id IN (SELECT ID FROM @ListadoIDs)
END
GO