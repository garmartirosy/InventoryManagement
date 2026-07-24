CREATE PROCEDURE GetTeamMembers
AS SET NOCOUNT ON
SELECT Id, FullName, Email, RoleName
FROM TeamMember
ORDER BY FullName
