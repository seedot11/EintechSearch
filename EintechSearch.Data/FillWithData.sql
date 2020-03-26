/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DECLARE @GroupId INT;

DELETE FROM [Person];
DELETE FROM [Group];

INSERT INTO [Group] ([Name]) VALUES ('Lions');
SET @GroupId = SCOPE_IDENTITY();

INSERT INTO [Person] ([FirstName], [LastName], [Created], [GroupId]) VALUES ('Tommy', 'Smith', GETDATE(), @GroupId);
INSERT INTO [Person] ([FirstName], [LastName], [Created], [GroupId]) VALUES ('Tim', 'Thorne', GETDATE(), @GroupId);
INSERT INTO [Person] ([FirstName], [LastName], [Created], [GroupId]) VALUES ('Tammy', 'Borne', GETDATE(), @GroupId);

INSERT INTO [Group] ([Name]) VALUES ('Tigers');
SET @GroupId = SCOPE_IDENTITY();

INSERT INTO [Person] ([FirstName], [LastName], [Created], [GroupId]) VALUES ('Timmy', 'Smit', GETDATE(), @GroupId);
INSERT INTO [Person] ([FirstName], [LastName], [Created], [GroupId]) VALUES ('Thomas', 'Mith', GETDATE(), @GroupId);