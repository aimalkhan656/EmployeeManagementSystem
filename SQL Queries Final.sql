-- DEPARTMENTS TABLE
CREATE TABLE Departments (
    DepartmentCode INT PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(255)
);

-- EMPLOYEES TABLE
CREATE TABLE Employees (
    EmployeeCode INT PRIMARY KEY,
    EmployeeName NVARCHAR(100) NOT NULL,
    DepartmentCode INT,
    Salary FLOAT CHECK (Salary >= 0),
    Role NVARCHAR(50) CHECK (Role IN ('Admin','HR','Employee')),
    Password NVARCHAR(100) NOT NULL,

    FOREIGN KEY (DepartmentCode)
    REFERENCES Departments(DepartmentCode)
);

-- PAYROLL TABLE
CREATE TABLE Payroll (
    PayrollID INT IDENTITY PRIMARY KEY,
    EmployeeCode INT,
    BasicSalary FLOAT,
    Allowances FLOAT,
    Deductions FLOAT,
    NetSalary FLOAT,

    FOREIGN KEY (EmployeeCode)
    REFERENCES Employees(EmployeeCode)
);

-- ATTENDANCE TABLE
CREATE TABLE Attendance (
    AttendanceID INT IDENTITY PRIMARY KEY,
    EmployeeCode INT,
    Dated DATE DEFAULT GETDATE(),
    Status NVARCHAR(20) CHECK (Status IN ('Present','Absent')),

    FOREIGN KEY (EmployeeCode)
    REFERENCES Employees(EmployeeCode)
);

-- LEAVES TABLE
CREATE TABLE Leaves (
    LeaveID INT IDENTITY PRIMARY KEY,
    EmployeeCode INT,
    LeaveType NVARCHAR(50),
    FromDate DATE,
    ToDate DATE,
    Status NVARCHAR(20) DEFAULT 'Pending',

    FOREIGN KEY (EmployeeCode)
    REFERENCES Employees(EmployeeCode)
);

-- INDEXES (FOR PERFORMANCE)
CREATE INDEX idx_emp_name ON Employees(EmployeeName);
CREATE INDEX idx_emp_dept ON Employees(DepartmentCode);
CREATE INDEX idx_payroll_emp ON Payroll(EmployeeCode);
CREATE INDEX idx_attendance_emp ON Attendance(EmployeeCode);

-- FUNCTION: CALCULATE NET SALARY
CREATE FUNCTION fn_CalculateNetSalary
(
    @basic FLOAT,
    @allow FLOAT,
    @ded FLOAT
)
RETURNS FLOAT
AS
BEGIN
    RETURN (@basic + @allow - @ded);
END;
GO

-- FUNCTION: AVERAGE SALARY
CREATE FUNCTION fn_GetAverageSalary()
RETURNS FLOAT
AS
BEGIN
    RETURN (SELECT AVG(Salary) FROM Employees);
END;
GO

-- FUNCTION: EMPLOYEE COUNT IN DEPARTMENT
CREATE FUNCTION fn_DepartmentEmployeeCount(@DeptCode INT)
RETURNS INT
AS
BEGIN
    RETURN (
        SELECT COUNT(*) 
        FROM Employees 
        WHERE DepartmentCode = @DeptCode
    );
END;
GO

-- VIEW: EMPLOYEE DETAILS
CREATE VIEW vw_EmployeeDetails AS
SELECT 
    E.EmployeeCode,
    E.EmployeeName,
    D.DepartmentName,
    E.Salary,
    E.Role
FROM Employees E
LEFT JOIN Departments D
ON E.DepartmentCode = D.DepartmentCode;
GO

-- VIEW: DEPARTMENT DETAILS
CREATE VIEW vw_DepartmentDetails AS
SELECT 
    D.DepartmentCode,
    D.DepartmentName,
    D.Description,
    COUNT(E.EmployeeCode) AS TotalEmployees
FROM Departments D
LEFT JOIN Employees E
ON D.DepartmentCode = E.DepartmentCode
GROUP BY D.DepartmentCode, D.DepartmentName, D.Description;
GO

-- VIEW: LEAVES
CREATE VIEW vw_Leaves AS
SELECT * FROM Leaves;
GO

-- VIEW: ATTENDANCE
CREATE VIEW vw_Attendance AS
SELECT * FROM Attendance;
GO

-- VIEW: PAYROLL
CREATE VIEW vw_PayrollDetails AS
SELECT * FROM Payroll;
GO


-- VIEW: PAYSLIP
CREATE VIEW vw_Payslip AS
SELECT 
    E.EmployeeName,
    D.DepartmentName,
    P.BasicSalary,
    P.Allowances,
    P.Deductions,
    P.NetSalary,
    E.EmployeeCode
FROM Payroll P
JOIN Employees E ON P.EmployeeCode = E.EmployeeCode
JOIN Departments D ON E.DepartmentCode = D.DepartmentCode;
GO

-- LOGIN PROCEDURE
CREATE PROCEDURE sp_LoginUser
    @EmployeeCode INT,
    @Password NVARCHAR(100)
AS
BEGIN
    SELECT EmployeeName, Role
    FROM Employees
    WHERE EmployeeCode = @EmployeeCode
    AND Password = @Password;
END;
GO

-- INSERT EMPLOYEE
CREATE PROCEDURE sp_InsertEmployee
    @EmployeeCode INT,
    @EmployeeName NVARCHAR(100),
    @DepartmentCode INT,
    @Salary FLOAT,
    @Role NVARCHAR(50),
    @Password NVARCHAR(100)
AS
BEGIN
    INSERT INTO Employees
    VALUES (@EmployeeCode,@EmployeeName,@DepartmentCode,@Salary,@Role,@Password);
END;
GO

-- UPDATE EMPLOYEE
CREATE PROCEDURE sp_UpdateEmployee
    @EmployeeCode INT,
    @EmployeeName NVARCHAR(100),
    @DepartmentCode INT,
    @Salary FLOAT,
    @Role NVARCHAR(50)
AS
BEGIN
    UPDATE Employees
    SET 
        EmployeeName = @EmployeeName,
        DepartmentCode = @DepartmentCode,
        Salary = @Salary,
        Role = @Role
    WHERE EmployeeCode = @EmployeeCode;
END;
GO

-- DELETE EMPLOYEE
CREATE PROCEDURE sp_DeleteEmployee
    @EmployeeCode INT
AS
BEGIN
    DELETE FROM Employees WHERE EmployeeCode = @EmployeeCode;
END;
GO

-- DEPARTMENT PROCEDURES
CREATE PROCEDURE sp_AddDepartment
    @DepartmentCode INT,
    @DepartmentName NVARCHAR(100),
    @Description NVARCHAR(255)
AS
BEGIN
    INSERT INTO Departments VALUES (@DepartmentCode,@DepartmentName,@Description);
END;
GO

CREATE PROCEDURE sp_UpdateDepartment
    @DepartmentCode INT,
    @DepartmentName NVARCHAR(100),
    @Description NVARCHAR(255)
AS
BEGIN
    UPDATE Departments
    SET DepartmentName=@DepartmentName, Description=@Description
    WHERE DepartmentCode=@DepartmentCode;
END;
GO

CREATE PROCEDURE sp_DeleteDepartment
    @DepartmentCode INT
AS
BEGIN
    DELETE FROM Departments WHERE DepartmentCode=@DepartmentCode;
END;
GO


-- LEAVE PROCEDURES
CREATE PROCEDURE sp_ApplyLeave
    @EmployeeCode INT,
    @LeaveType NVARCHAR(50),
    @FromDate DATE,
    @ToDate DATE
AS
BEGIN
    INSERT INTO Leaves(EmployeeCode,LeaveType,FromDate,ToDate,Status)
    VALUES (@EmployeeCode,@LeaveType,@FromDate,@ToDate,'Pending');
END;
GO

CREATE PROCEDURE sp_UpdateLeaveStatus
    @LeaveID INT,
    @Status NVARCHAR(20)
AS
BEGIN
    UPDATE Leaves SET Status=@Status WHERE LeaveID=@LeaveID;
END;
GO

-- ATTENDANCE
CREATE PROCEDURE sp_MarkAttendance
    @EmployeeCode INT,
    @Status NVARCHAR(20)
AS
BEGIN
    INSERT INTO Attendance(EmployeeCode,Status)
    VALUES (@EmployeeCode,@Status);
END;
GO

-- PAYROLL
CREATE PROCEDURE sp_InsertPayroll
    @EmployeeCode INT,
    @BasicSalary FLOAT,
    @Allowances FLOAT,
    @Deductions FLOAT
AS
BEGIN
    INSERT INTO Payroll(EmployeeCode,BasicSalary,Allowances,Deductions,NetSalary)
    VALUES (
        @EmployeeCode,
        @BasicSalary,
        @Allowances,
        @Deductions,
        dbo.fn_CalculateNetSalary(@BasicSalary,@Allowances,@Deductions)
    );
END;
GO

CREATE PROCEDURE sp_UpdatePayroll
    @EmployeeCode INT,
    @BasicSalary FLOAT,
    @Allowances FLOAT,
    @Deductions FLOAT
AS
BEGIN
    UPDATE Payroll
    SET BasicSalary=@BasicSalary,
        Allowances=@Allowances,
        Deductions=@Deductions,
        NetSalary=dbo.fn_CalculateNetSalary(@BasicSalary,@Allowances,@Deductions)
    WHERE EmployeeCode=@EmployeeCode;
END;
GO

CREATE PROCEDURE sp_DeletePayroll
    @EmployeeCode INT
AS
BEGIN
    DELETE FROM Payroll WHERE EmployeeCode=@EmployeeCode;
END;
GO

-- AUTO CALCULATE SALARY
CREATE TRIGGER trg_CalcSalary
ON Payroll
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE P
    SET NetSalary = I.BasicSalary + I.Allowances - I.Deductions
    FROM Payroll P
    JOIN inserted I ON P.PayrollID = I.PayrollID;
END;
GO

-- PREVENT DELETING DEPARTMENT WITH EMPLOYEES

CREATE TRIGGER trg_PreventDeptDelete
ON Departments
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (
        SELECT 1 FROM Employees E
        JOIN deleted D ON E.DepartmentCode = D.DepartmentCode
    )
    BEGIN
        RAISERROR('Cannot delete department with employees',16,1);
        RETURN;
    END

    DELETE FROM Departments
    WHERE DepartmentCode IN (SELECT DepartmentCode FROM deleted);
END;
GO


-- INSERT DEPARTMENTS
INSERT INTO Departments (DepartmentCode, DepartmentName, [Description]) VALUES
(1, 'Finance', 'Finance Department'),
(3, 'Customer Support', 'Customer Handling'),
(4, 'Sales', 'Sales Department'),
(5, 'Management', 'Management Department'),
(6, 'General', 'General Department'),
(7, 'IT', 'IT Department'),
(8, 'Sales Support', 'Sales Assistance');


-- INSERT EMPLOYEES
-- Default Password = 123

INSERT INTO Employees 
(EmployeeCode, EmployeeName, DepartmentCode, Role, Salary, Password)
VALUES

(100, 'Sami Rao', 1, 'Admin', 0, '123'),
(47, 'Samiullah', 1, 'Admin', 0, '123'),
(53, 'Aimal Mazhar', 5, 'Admin', 99999, '123'),
(21, 'Aqsa Ubaid', 7, 'Admin', 41245, '123'),

(69, 'Umer Jahngir', 1, 'HR', 111, '123'),
(23, 'Salar Sikandar', 8, 'Employee', 52300, '123'),
(45, 'Hasham Jabeel', 3, 'HR', 42000, '123'),
(54, 'Bilal Khan', 5, 'Employee', 82000, '123'),
(32, 'Haya Suleman', 4, 'Employee', 43211.55, '123'),
(13, 'Imama Hashim', 1, 'Employee', 23456.35, '123'),
(55, 'Jihan Sikandar', 1, 'Employee', 34234.35, '123'),
(16, 'Imaan Farooq', 4, 'Employee', 65765.40, '123'),
(35, 'Hamdan Ilyas', 5, 'HR', 76523.45, '123'),
(78, 'Faris Ghazi', 4, 'Employee', 45654.54, '123'),
(39, 'Rayyan Malik', 8, 'Employee', 87654.46, '123'),
(86, 'Faraz Ahmed', 4, 'HR', 45666.00, '123'),
(43, 'Haider Raza', 5, 'Employee', 65484.54, '123'),
(24, 'Saad Haroon', 7, 'Employee', 56789.65, '123'),
(26, 'Anaya Salman', 7, 'Employee', 87654.67, '123'),
(63, 'Hashim Kardar', 5, 'Employee', 57765.60, '123'),
(20, 'Zumar Yusuf', 4, 'HR', 87654.60, '123'),
(34, 'Mehmil Ibrahim', 3, 'Employee', 98765.46, '123'),
(49, 'Ufaq Arsalan', 3, 'HR', 76543.77, '123'),
(64, 'Parishey Jahanzeb', 5, 'Employee', 76543.90, '123'),
(33, 'Khurram', 4, 'Employee', 76575.70, '123'),
(87, 'Ahmar Khan', 3, 'Employee', 65326.65, '123'),
(89, 'Durre Shehwar', 7, 'HR', 23456.00, '123'),
(30, 'Saad Zafar', 1, 'Employee', 23456.60, '123'),
(99, 'Abdul Aali', 4, 'Employee', 31655.46, '123'),
(75, 'Momina Sultan', 4, 'Employee', 23456.54, '123'),
(73, 'Mahnoor Ali', 5, 'HR', 76432.34, '123'),
(150, 'Jahnzaib', 6, 'HR', 5555.00, '123');

--Payroll data
INSERT INTO Payroll (EmployeeCode, BasicSalary, Allowances, Deductions, NetSalary)
SELECT 
    EmployeeCode,
    Salary,
    Salary * 0.10,
    Salary * 0.05,
    Salary + (Salary * 0.10) - (Salary * 0.05)
FROM Employees;

-- 🔥 STORED PROCEDURE 1: sp_AttendanceRisk (Admin)
CREATE PROCEDURE sp_AttendanceRisk
AS
BEGIN
    SELECT TOP 10 
        e.EmployeeCode, 
        e.EmployeeName AS Name, 
        ISNULL(d.DepartmentName, 'No Dept') AS Department,
        COUNT(a.AttendanceID) AS TotalDays,
        SUM(CASE WHEN a.Status='Absent' THEN 1 ELSE 0 END) AS AbsentDays,
        CASE WHEN COUNT(a.AttendanceID)=0 THEN 0 
             ELSE ROUND(SUM(CASE WHEN a.Status='Absent' THEN 1 ELSE 0 END)*100.0/COUNT(a.AttendanceID),1) END AS RiskPercent
    FROM Employees e 
    LEFT JOIN Attendance a ON e.EmployeeCode = a.EmployeeCode
    LEFT JOIN Departments d ON e.DepartmentCode = d.DepartmentCode
    WHERE a.Dated >= DATEADD(MONTH, -1, GETDATE()) OR a.Dated IS NULL
    GROUP BY e.EmployeeCode, e.EmployeeName, d.DepartmentName
    ORDER BY RiskPercent DESC;
END
GO

--  STORED PROCEDURE 2: sp_LeaveTrends (HR)
CREATE PROCEDURE sp_LeaveTrends
AS
BEGIN
    SELECT DATENAME(MONTH, l.FromDate) AS Month, 
           COUNT(*) AS TotalLeaves, 
           l.LeaveType
    FROM Leaves l 
    WHERE l.FromDate >= DATEADD(MONTH, -3, GETDATE())
    GROUP BY DATENAME(MONTH, l.FromDate), l.LeaveType
    ORDER BY TotalLeaves DESC;
END
GO

--  VIEW 1: vw_SalaryOutliers (Admin)
CREATE VIEW vw_SalaryOutliers AS
SELECT e.EmployeeCode, e.EmployeeName AS Name, 
       ISNULL(d.DepartmentName,'N/A') AS Department,
       ISNULL(p.BasicSalary, e.Salary) AS BasicSalary,
       CASE WHEN ISNULL(p.BasicSalary, e.Salary) > 50000 THEN 'HIGH' 
            WHEN ISNULL(p.BasicSalary, e.Salary) < 20000 THEN 'LOW'
            ELSE 'NORMAL' END AS Status
FROM Employees e 
LEFT JOIN Payroll p ON e.EmployeeCode = p.EmployeeCode
LEFT JOIN Departments d ON e.DepartmentCode = d.DepartmentCode;
GO

--  VIEW 2: vw_DepartmentWorkload (HR)
CREATE VIEW vw_DepartmentWorkload AS
SELECT ISNULL(d.DepartmentName,'Total') AS DepartmentName,
       COUNT(e.EmployeeCode) AS TotalEmployees,
       COUNT(a.AttendanceID) AS TotalAttendance,
       COUNT(CASE WHEN a.Status='Present' THEN 1 END) AS PresentDays
FROM Departments d
LEFT JOIN Employees e ON d.DepartmentCode = e.DepartmentCode
LEFT JOIN Attendance a ON e.EmployeeCode = a.EmployeeCode
GROUP BY d.DepartmentName WITH ROLLUP;
GO



CREATE PROCEDURE sp_AddEmployee
(
    @EmployeeCode INT,
    @EmployeeName VARCHAR(100),
    @DepartmentCode INT,
    @Salary FLOAT,
    @Role VARCHAR(50),
    @Password VARCHAR(100)
)
AS
BEGIN
    INSERT INTO Employees
    (
        EmployeeCode,
        EmployeeName,
        DepartmentCode,
        Salary,
        Role,
        Password
    )
    VALUES
    (
        @EmployeeCode,
        @EmployeeName,
        @DepartmentCode,
        @Salary,
        @Role,
        @Password
    )
END


CREATE PROCEDURE sp_UpdateEmployee
(
    @EmployeeCode INT,
    @EmployeeName VARCHAR(100),
    @DepartmentCode INT,
    @Salary FLOAT,
    @Role VARCHAR(50)
)
AS
BEGIN
    UPDATE Employees
    SET
        EmployeeName = @EmployeeName,
        DepartmentCode = @DepartmentCode,
        Salary = @Salary,
        Role = @Role
    WHERE EmployeeCode = @EmployeeCode
END


CREATE PROCEDURE sp_DeleteEmployee
(
    @EmployeeCode INT
)
AS
BEGIN
    DELETE FROM Employees
    WHERE EmployeeCode = @EmployeeCode
END



ALTER VIEW vw_EmployeeDetails
AS
SELECT
    e.EmployeeCode,
    e.EmployeeName,
    d.DepartmentName,
    e.Salary,
    e.Role,
    e.Password
FROM Employees e
INNER JOIN Departments d
ON e.DepartmentCode = d.DepartmentCode;



ALTER PROCEDURE sp_MarkAttendance
(
    @EmployeeCode INT,
    @AttendanceDate DATE,
    @Status VARCHAR(20)
)
AS
BEGIN
    INSERT INTO Attendance
    (
        EmployeeCode,
        Dated,
        Status
    )
    VALUES
    (
        @EmployeeCode,
        @AttendanceDate,
        @Status
    )
END


CREATE PROCEDURE sp_SortPayrollBySalary
(
    @Role VARCHAR(50),
    @EmployeeCode INT,
    @SortOrder VARCHAR(10)
)
AS
BEGIN

    -- EMPLOYEE
    IF @Role = 'Employee'
    BEGIN
        SELECT *
        FROM Payroll
        WHERE EmployeeCode = @EmployeeCode
        ORDER BY
        CASE
            WHEN @SortOrder = 'Ascending'
            THEN NetSalary
        END ASC,

        CASE
            WHEN @SortOrder = 'Descending'
            THEN NetSalary
        END DESC
    END

    -- ADMIN / HR
    ELSE
    BEGIN
        SELECT *
        FROM Payroll
        ORDER BY
        CASE
            WHEN @SortOrder = 'Ascending'
            THEN NetSalary
        END ASC,

        CASE
            WHEN @SortOrder = 'Descending'
            THEN NetSalary
        END DESC
    END

END
GO


ALTER PROCEDURE sp_DeleteEmployee
(
    @EmployeeCode INT
)
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY

        DELETE FROM Payroll
        WHERE EmployeeCode = @EmployeeCode;

        DELETE FROM Attendance
        WHERE EmployeeCode = @EmployeeCode;

        DELETE FROM Leaves
        WHERE EmployeeCode = @EmployeeCode;

        DELETE FROM Employees
        WHERE EmployeeCode = @EmployeeCode;

        COMMIT TRANSACTION;

    END TRY

    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO

EXEC sp_helptext 'sp_DeleteEmployee'

