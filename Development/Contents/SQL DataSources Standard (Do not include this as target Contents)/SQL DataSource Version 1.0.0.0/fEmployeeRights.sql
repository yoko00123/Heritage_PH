ALTER FUNCTION dbo.fEmployeeRights (@ID_Session INT,@ID_Employee INT)
RETURNS bit
AS
BEGIN
--Edited by Yoku 04082019
	-- Declare the return variable here
	DECLARE @Result bit

	DECLARE @ID_User INT
	DECLARE @ID_Company INT
	DECLARE @ID_SessionEmployee INT
	DECLARE @ID_UserGroup int
	SELECT 
		@ID_User = s.ID_User
		,@ID_Company = s.ID_Company
		,@ID_SessionEmployee = s.ID_Employee
		,@ID_UserGroup = u.ID_UserGroup	
	FROM tSession s 
	INNER JOIN tUser u ON u.ID = s.ID_User
	WHERE s.ID =@ID_Session

	SELECT @Result = 0
------------------------------------------------------------------------------------------------------------------
SELECT @Result = 1
		FROM  
		(
		Select e.ID 
		From    
			dbo.tUser U 
			INNER JOIN dbo.tUserGroup UG ON ug.id = u.ID_UserGroup
			INNER JOIN dbo.tUserGroupDesignation UGD ON UG.ID = UGD.ID_UserGroup 
			INNER JOIN dbo.tEmployee E ON UGD.ID_Designation = E.ID_Designation 
		WHERE (U.ID=@ID_User) AND E.ID = @ID_Employee )a
	    RETURN @Result

END
GO
