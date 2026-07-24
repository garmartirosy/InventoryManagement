CREATE PROCEDURE UpdateTeamMemberRole(
	@TeamMemberId INT,
	@RoleName NVARCHAR(50)
)
AS SET NOCOUNT ON
UPDATE TeamMember
SET RoleName = @RoleName
WHERE Id = @TeamMemberId
