namespace Contoso.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAgoToDob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "DateofBirth", c => c.DateTime(nullable: false));
            DropColumn("dbo.People", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.People", "DateofBirth");
        }
    }
}
