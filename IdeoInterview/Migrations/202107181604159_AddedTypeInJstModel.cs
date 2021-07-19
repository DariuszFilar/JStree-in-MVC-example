namespace IdeoInterview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTypeInJstModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JsTreeModels", "type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JsTreeModels", "type");
        }
    }
}
