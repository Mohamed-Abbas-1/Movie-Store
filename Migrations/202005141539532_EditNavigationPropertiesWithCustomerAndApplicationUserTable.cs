namespace MovieStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditNavigationPropertiesWithCustomerAndApplicationUserTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Customers", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Customers", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Customers", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Customers", name: "ApplicationUserId", newName: "ApplicationUser_Id");
        }
    }
}
