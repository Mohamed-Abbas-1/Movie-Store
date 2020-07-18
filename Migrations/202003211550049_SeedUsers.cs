namespace MovieStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES ( N'admin@moviestore.com', 0, N'AIwQMo1P7xQSSSzWmr+m/NrsgcNL6EstitZ3n1sWviowb955lkEr/ScAL/XViWrP2Q==', N'd3b2390c-6c44-4062-a5db-2eaa53d44d56', NULL, 0, 0, NULL, 1, 0, N'admin@moviestore.com')
INSERT INTO [dbo].[AspNetUsers] ([Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'guest@moviestore.com', 0, N'AI+xucQbS3spRDCo7y66E3gUBMHdgavrFhuSNOd7BOK+eqyz0BVDIa8iWi/loexKOw==', N'77c4fc0d-64c1-42f8-ad04-c79f666e2096', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')


INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a383eb1e-4476-4873-b906-d9df8400e0f5', N'CanManageMovies')
");
        }
        
        public override void Down()
        {
        }
    }
}
