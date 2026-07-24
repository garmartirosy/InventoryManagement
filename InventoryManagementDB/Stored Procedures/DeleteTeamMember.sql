CREATE PROCEDURE DeleteTeamMember(
	@TeamMemberId INT
)
AS SET NOCOUNT ON
DELETE FROM TeamMember
WHERE Id = @TeamMemberId
