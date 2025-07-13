-- Question 4: Nested TRY...CATCH with RAISERROR
GO
CREATE OR ALTER PROCEDURE TransferEmployee_Q4
    @EmployeeID INT,
    @NewDepartmentID INT
AS
BEGIN
    BEGIN TRY
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
