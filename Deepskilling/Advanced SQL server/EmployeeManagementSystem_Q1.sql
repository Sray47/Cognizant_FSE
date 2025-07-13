-- Question 1: Basic TRY...CATCH for Error Logging
GO
CREATE OR ALTER PROCEDURE AddEmployee_Q1
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Salary DECIMAL(10,2),
    @DepartmentID INT
AS
BEGIN
    BEGIN TRY
        INSERT INTO Employees (FirstName, LastName, Email, Salary, DepartmentID)
        VALUES (@FirstName, @LastName, @Email, @Salary, @DepartmentID);
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        INSERT INTO AuditLog (Action, ErrorMessage)
        VALUES ('AddEmployee', @ErrorMessage);
    END CATCH
END
GO
