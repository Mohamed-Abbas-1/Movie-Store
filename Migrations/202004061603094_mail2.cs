namespace MovieStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mail2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mails", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mails", "Name");
        }
    }
}
