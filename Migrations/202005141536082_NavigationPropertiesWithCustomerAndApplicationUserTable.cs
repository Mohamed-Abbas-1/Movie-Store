namespace MovieStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NavigationPropertiesWithCustomerAndApplicationUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Customers", "ApplicationUser_Id");
            AddForeignKey("dbo.Customers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Customers", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Customers", "ApplicationUser_Id");
        }
    }
}
