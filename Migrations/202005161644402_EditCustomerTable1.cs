namespace MovieStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditCustomerTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "AddressOrPhone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "AddressOrPhone");
        }
    }
}
