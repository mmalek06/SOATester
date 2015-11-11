namespace SOATester.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Protocol = c.String(),
                        Method = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RequestHeader",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(),
                        ScenarioId = c.Int(),
                        TestId = c.Int(),
                        StepId = c.Int(),
                        Name = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .ForeignKey("dbo.Scenario", t => t.ScenarioId)
                .ForeignKey("dbo.Test", t => t.TestId)
                .ForeignKey("dbo.Step", t => t.StepId)
                .Index(t => t.ProjectId)
                .Index(t => t.ScenarioId)
                .Index(t => t.TestId)
                .Index(t => t.StepId);
            
            CreateTable(
                "dbo.Scenario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        Protocol = c.String(),
                        Method = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScenarioId = c.Int(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        Protocol = c.String(),
                        Method = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scenario", t => t.ScenarioId, cascadeDelete: true)
                .Index(t => t.ScenarioId);
            
            CreateTable(
                "dbo.Step",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestId = c.Int(nullable: false),
                        RequestBody = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        Protocol = c.String(),
                        Method = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Test", t => t.TestId, cascadeDelete: true)
                .Index(t => t.TestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Step", "TestId", "dbo.Test");
            DropForeignKey("dbo.RequestHeader", "StepId", "dbo.Step");
            DropForeignKey("dbo.Test", "ScenarioId", "dbo.Scenario");
            DropForeignKey("dbo.RequestHeader", "TestId", "dbo.Test");
            DropForeignKey("dbo.Scenario", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.RequestHeader", "ScenarioId", "dbo.Scenario");
            DropForeignKey("dbo.RequestHeader", "ProjectId", "dbo.Project");
            DropIndex("dbo.Step", new[] { "TestId" });
            DropIndex("dbo.Test", new[] { "ScenarioId" });
            DropIndex("dbo.Scenario", new[] { "ProjectId" });
            DropIndex("dbo.RequestHeader", new[] { "StepId" });
            DropIndex("dbo.RequestHeader", new[] { "TestId" });
            DropIndex("dbo.RequestHeader", new[] { "ScenarioId" });
            DropIndex("dbo.RequestHeader", new[] { "ProjectId" });
            DropTable("dbo.Step");
            DropTable("dbo.Test");
            DropTable("dbo.Scenario");
            DropTable("dbo.RequestHeader");
            DropTable("dbo.Project");
        }
    }
}
