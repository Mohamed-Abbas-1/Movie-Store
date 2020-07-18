namespace MovieStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditRentalTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "AskedReturn", c => c.Boolean());
            AddColumn("dbo.Rentals", "AskedReturnDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rentals", "AskedReturnDate");
            DropColumn("dbo.Rentals", "AskedReturn");
        }
    }
}
