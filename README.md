used stored procedures:

CREATE PROCEDURE [dbo].[GetHighestEarnerPerJobLevel]
AS
SELECT *
FROM (
    SELECT *, ROW_NUMBER() OVER (PARTITION BY PoziomStanowiska ORDER BY Zarobki DESC) AS rn
    FROM Employees
) t
WHERE rn = 1

CREATE PROCEDURE [dbo].[GetLowestEarnerPerCityAfterTax]
AS
SELECT *
FROM (
    SELECT *, ROW_NUMBER() OVER (PARTITION BY MiejsceZamieszkania ORDER BY (Zarobki * 1.19)) AS rn
    FROM Employees
) t
WHERE rn = 1
