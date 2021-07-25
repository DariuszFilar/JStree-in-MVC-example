namespace IdeoInterview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDTITLE : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forms", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Forms", "Title");
        }
    }
}
