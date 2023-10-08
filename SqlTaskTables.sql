--CREATE DATABASE SaiKumarDataBeat
--USE SaiKumarDataBeat
--===========Requirement===============================================================
--Employees table - id, name, department_id, hire_date, salary, manager_id
--Products table - product_id, product_name, category_id
--Sales table - sale_id, product_id, employee_id, sale_date, amount
--=====================================================================================


--CREATE TABLE Department(
--Dept_Id  INT PRIMARY KEY IDENTITY(1,1),
--DepartmentName VARCHAR(50))

--CREATE TABLE Category(
--Caty_Id  INT PRIMARY KEY IDENTITY(1,1),
--CategoryName VARCHAR(50))

--CREATE TABLE Manager(
--Mnag_Id  INT PRIMARY KEY IDENTITY(1,1),
--ManagerName VARCHAR(50),
--Manager_Rank FLOAT
--)
--CREATE TABLE Asst_Manager(
--Asst_Mnag_Id  INT PRIMARY KEY IDENTITY(1,1),
--Asst_ManagerName VARCHAR(50),
--Asst_Manager_Rank FLOAT,
--Manager_Id INT FOREIGN KEY REFERENCES Manager(Mnag_Id)
--)

--CREATE TABLE Products(
--Product_id  INT PRIMARY KEY IDENTITY(1,1),
--Product_Name VARCHAR(50),
--Category_id INT FOREIGN KEY REFERENCES Category(Caty_Id)
--)

--CREATE TABLE Employees(
--Emp_id  INT PRIMARY KEY IDENTITY(1,1),
--Emp_name VARCHAR(50),
--department_id INT FOREIGN KEY REFERENCES Department(Dept_Id),
--Hire_date DATE,
--Salary BIGINT,
--Ass_manager_id INT  FOREIGN KEY REFERENCES Asst_Manager(Asst_Mnag_Id)
--)


--CREATE TABLE Sales(
--Sale_id  INT PRIMARY KEY IDENTITY(1,1),
--Product_id INT FOREIGN KEY REFERENCES Products(Product_id),
--Employee_id INT FOREIGN KEY REFERENCES Employees(Emp_id),
--sale_date DATE,
--Amount BIGINT
--)


--INSERT INTO Manager VALUES ('Rakesh',8),('Uday', 8)
--INSERT INTO Asst_Manager VALUES ('Shashi',6,1),('Vasu',5,2)
--INSERT INTO Department VALUES('HumanResource'),('Finance'),('Sales')
--INSERT INTO Employees VALUES('Srinu',3,'03-04-2023',30000,1),('Vedit',1,'03-01-2023',35000,1),
--('Sravan',3,'03-01-2023',20000,2),('AmmiReddy',3,'03-02-2023',25000,2)
--INSERT INTO Category VALUES('Electronics'),('Computers & Accessories')
--INSERT INTO Products VALUES('Laptop',1),('Printer',2),('HDMI Cable',2),('TABLET PC',1)
--INSERT INTO Sales VALUES (1,5,'2023-1-22',40000),(2,7,'2023-01-13',5000),
--(3,5,'2023-3-12',400),(4,8,'2023-4-02',3000),(3,8,'2023-2-01',1000),(4,5,'2023-03-15',6000)

--==========================================================================================================
--====Hierarchical Structure Query with Rank:=====
WITH EmployeeHierarchy AS (
  SELECT
    E1.Emp_id AS EmployeeId,
    E1.Emp_name AS EmployeeName,
    E1.Ass_manager_id AS ManagerId,
    1 AS Rank
  FROM Employees AS E1
  WHERE E1.Ass_manager_id IS NOT NULL 

  UNION ALL

  SELECT
    E2.Emp_id AS EmployeeId,
    E2.Emp_name AS EmployeeName,
    E2.Ass_manager_id AS ManagerId,
    EH.Rank + 1 AS Rank
  FROM EmployeeHierarchy AS EH
  INNER JOIN Employees AS E2
  ON E2.Ass_manager_id = EH.EmployeeId
)

SELECT
  EH.EmployeeId,
  EH.EmployeeName,
  EH.Rank,
  M1.Asst_ManagerName AS Manager,
  M2.ManagerName AS ManagerOfManager
FROM EmployeeHierarchy AS EH
LEFT JOIN Asst_Manager AS M1
ON M1.Asst_Mnag_Id = EH.ManagerId
LEFT JOIN Manager AS M2
ON M2.Mnag_Id = M1.Manager_Id;
---============================Cumulative Sum Query:=========

SELECT
    p.product_id,
    p.product_name,
    s.sale_date,
    s.amount,
    SUM(s.amount) OVER (PARTITION BY p.product_id ORDER BY s.sale_date) AS cumulative_sum
FROM Products p
JOIN Sales s ON p.product_id = s.product_id
WHERE p.category_id = 1;

--==========================Pivot Query:====================
SELECT
    product_id,
    [1] AS Jan,
    [2] AS Feb,
    [3] AS Mar,
    [4] AS Apr,
    [5] AS May,
    [6] AS Jun,
    [7] AS Jul,
    [8] AS Aug,
    [9] AS Sep,
    [10] AS Oct,
    [11] AS Nov,
    [12] AS Dec
FROM (
    SELECT
        product_id,
        DATEPART(MONTH, sale_date) AS [Month],
        amount
    FROM Sales
) AS SourceTable
PIVOT (
    SUM(amount)
    FOR [Month] IN ([1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12])
) AS PivotTable;
--===================Ranking Employees Query: ==========================
WITH RankedSales AS (
    SELECT
        e.Emp_name AS employee_name,
        e.department_id,
        s.amount,
        RANK() OVER (PARTITION BY e.department_id ORDER BY s.amount DESC) AS sales_rank
    FROM Employees e
    JOIN Sales s ON e.Emp_id = s.employee_id
)
SELECT
    employee_name,
    department_id,
    amount
FROM RankedSales
WHERE sales_rank <= 3;
---=====================Products with Above-Average Sales Query:==============
SELECT p.product_id, p.product_name, s.sale_date, s.amount
FROM Products p
JOIN Sales s ON p.product_id = s.product_id
WHERE s.amount > (
    SELECT AVG(s2.amount)
    FROM Sales s2
    WHERE s2.product_id = p.product_id
);
