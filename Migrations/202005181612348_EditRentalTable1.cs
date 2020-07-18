namespace MovieStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditRentalTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "AskedRented", c => c.Boolean());
            AddColumn("dbo.Rentals", "AskedRentedDate", c => c.DateTime());
            AlterColumn("dbo.Rentals", "DateRented", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rentals", "DateRented", c => c.DateTime(nullable: false));
            DropColumn("dbo.Rentals", "AskedRentedDate");
            DropColumn("dbo.Rentals", "AskedRented");
        }
    }
}
