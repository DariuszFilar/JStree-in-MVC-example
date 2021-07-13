namespace IdeoInterview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedJsTreeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JsTreeModels",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        parent = c.String(),
                        text = c.String(),
                        icon = c.String(),
                        state = c.String(),
                        opened = c.Boolean(nullable: false),
                        disabled = c.Boolean(nullable: false),
                        selected = c.Boolean(nullable: false),
                        li_attr = c.String(),
                        a_attr = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JsTreeModels");
        }
    }
}
