CREATE PROCEDURE CreateTeamMember(
	@FullName NVARCHAR(200),
	@Email NVARCHAR(256),
	@RoleName NVARCHAR(50)
)
AS SET NOCOUNT ON
INSERT INTO TeamMember (FullName, Email, RoleName)
VALUES (@FullName, @Email, @RoleName)

SELECT CAST(SCOPE_IDENTITY() AS INT)
