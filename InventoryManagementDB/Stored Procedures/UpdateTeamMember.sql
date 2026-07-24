CREATE PROCEDURE UpdateTeamMember(
	@TeamMemberId INT,
	@FullName NVARCHAR(200),
	@Email NVARCHAR(256),
	@RoleName NVARCHAR(50)
)
AS SET NOCOUNT ON
UPDATE TeamMember
SET FullName = @FullName,
	Email = @Email,
	RoleName = @RoleName
WHERE Id = @TeamMemberId
