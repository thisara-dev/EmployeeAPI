CREATE PROCEDURE GetEmployeeStatistics
AS
BEGIN
    -- Total Employees
    DECLARE @TotalEmployees INT
    SELECT @TotalEmployees = COUNT(*) FROM Employees;

    -- New Employees Today
    DECLARE @NewEmployeesToday INT
    SELECT @NewEmployeesToday = COUNT(*) FROM Employees WHERE CONVERT(DATE, RegisteredDate) = CONVERT(DATE, GETDATE());

    -- New Employees This Week
    DECLARE @NewEmployeesThisWeek INT
    SELECT @NewEmployeesThisWeek = COUNT(*) FROM Employees WHERE DATEPART(WEEK, RegisteredDate) = DATEPART(WEEK, GETDATE());

    -- Active Employees
    DECLARE @ActiveEmployees INT
    SELECT @ActiveEmployees = COUNT(*) FROM Employees WHERE isActive = 1;

    -- Return the results
    SELECT 
        TotalEmployees = @TotalEmployees,
        NewEmployeesToday = @NewEmployeesToday,
        NewEmployeesThisWeek = @NewEmployeesThisWeek,
        ActiveEmployees = @ActiveEmployees;
END;