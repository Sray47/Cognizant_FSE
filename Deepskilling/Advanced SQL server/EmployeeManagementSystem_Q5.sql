-- Question 5: Logging All Errors in a Transaction
GO
CREATE OR ALTER PROCEDURE BatchInsertEmployees_Q5
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        -- Simulate multiple inserts (replace with actual parameters or temp table as needed)
        INSERT INTO Employees (FirstName, LastName, Email, Salary, DepartmentID)
        VALUES ('John', 'Doe', 'john.doe@example.com', 2000, 1);
        INSERT INTO Employees (FirstName, LastName, Email, Salary, DepartmentID)
        VALUES ('Jane', 'Smith', 'jane.smith@example.com', 2500, 2);
        -- Add more inserts as needed
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        INSERT INTO AuditLog (Action, ErrorMessage)
        VALUES ('BatchInsertEmployees', ERROR_MESSAGE());
        THROW;
    END CATCH
END
GO
