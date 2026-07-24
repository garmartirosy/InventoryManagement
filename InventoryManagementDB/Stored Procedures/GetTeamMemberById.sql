CREATE PROCEDURE GetTeamMemberById(
	@TeamMemberId INT
)
AS SET NOCOUNT ON
SELECT Id, FullName, Email, RoleName
FROM TeamMember
WHERE Id = @TeamMemberId
