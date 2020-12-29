namespace TaskManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskModels",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        Priority = c.Int(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        Finished = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TaskModels");
        }
    }
}
