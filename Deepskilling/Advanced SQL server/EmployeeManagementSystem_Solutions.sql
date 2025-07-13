-- Employee Management System SQL Server Exercises Solutions
-- Place all solutions in this file as per the requirements

-- Question 1, 2, 3, 6: AddEmployee procedure with error handling, logging, custom errors, and dynamic severity
GO
CREATE OR ALTER PROCEDURE AddEmployee
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Salary DECIMAL(10,2),
    @DepartmentID INT
AS
BEGIN
    BEGIN TRY
        -- Salary validation (Question 3 & 6)
        IF @Salary < 0
        BEGIN
            RAISERROR('Salary cannot be negative.', 16, 1);
            RETURN;
        END
        ELSE IF @Salary < 1000
        BEGIN
            RAISERROR('Salary is too low.', 10, 1);
            RETURN;
        END
        ELSE IF @Salary = 0
        BEGIN
            RAISERROR('Salary must be greater than zero.', 16, 1);
            RETURN;
        END

        INSERT INTO Employees (FirstName, LastName, Email, Salary, DepartmentID)
        VALUES (@FirstName, @LastName, @Email, @Salary, @DepartmentID);
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        INSERT INTO AuditLog (Action, ErrorMessage)
        VALUES ('AddEmployee', @ErrorMessage);
        -- Question 2: Re-raise error
        THROW;
    END CATCH
END
GO

-- Question 4: TransferEmployee with nested TRY...CATCH and custom error
GO
CREATE OR ALTER PROCEDURE TransferEmployee
    @EmployeeID INT,
    @NewDepartmentID INT
AS
BEGIN
    BEGIN TRY
        -- Check if department exists
        IF NOT EXISTS (SELECT 1 FROM Departments WHERE DepartmentID = @NewDepartmentID)
        BEGIN
            BEGIN TRY
                RAISERROR('Target department does not exist.', 16, 1);
            END TRY
            BEGIN CATCH
                INSERT INTO AuditLog (Action, ErrorMessage)
                VALUES ('TransferEmployee', ERROR_MESSAGE());
                THROW;
            END CATCH
            RETURN;
        END
        -- Update department
        UPDATE Employees
        SET DepartmentID = @NewDepartmentID
        WHERE EmployeeID = @EmployeeID;
    END TRY
    BEGIN CATCH
        INSERT INTO AuditLog (Action, ErrorMessage)
        VALUES ('TransferEmployee', ERROR_MESSAGE());
        THROW;
    END CATCH
END
GO

-- Question 5: BatchInsertEmployees with transaction and error logging
GO
CREATE OR ALTER PROCEDURE BatchInsertEmployees
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

-- End of solutions
