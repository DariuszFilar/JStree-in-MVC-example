namespace IdeoInterview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTest : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Forms", "Formid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Forms", "Formid", c => c.String());
        }
    }
}
