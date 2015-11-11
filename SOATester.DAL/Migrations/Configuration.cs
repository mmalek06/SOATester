namespace SOATester.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<SoaTesterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            AppDomain.CurrentDomain.SetData("DataDirectory", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
        }

        protected override void Seed(SoaTesterContext context)
        {
            // delete all data
            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'");
            // deletes data from all tables except __MigrationHistory
            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'IF OBJECT_ID(''?'') NOT IN (ISNULL(OBJECT_ID(''[dbo].[__MigrationHistory]''),0)) DELETE FROM ?'");
            context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'");

            // seed
            var restProj = new Entities.Project { Name = "REST" };
            var soapProj = new Entities.Project { Name = "SOAP" };

            context.Projects.AddOrUpdate(p => p.Id, restProj, soapProj);

            var scenario1 = new Entities.Scenario { Name = "scenario 1", Project = restProj };
            var scenario2 = new Entities.Scenario { Name = "scenario 2", Project = restProj };
            var scenario3 = new Entities.Scenario { Name = "scenario 3", Project = soapProj };

            context.Scenarios.AddOrUpdate(s => s.Id, scenario1, scenario2, scenario3);

            var test1 = new Entities.Test { Name = "test 1", Scenario = scenario1 };
            var test2 = new Entities.Test { Name = "test 2", Scenario = scenario1 };
            var test3 = new Entities.Test { Name = "test 3", Scenario = scenario2 };
            var test4 = new Entities.Test { Name = "test 4", Scenario = scenario3 };

            context.Tests.AddOrUpdate(t => t.Id, test1, test2, test3, test4);

            var step1 = new Entities.Step { Name = "step 1", Test = test1 };
            var step2 = new Entities.Step { Name = "step 2", Test = test1 };
            var step3 = new Entities.Step { Name = "step 3", Test = test1 };
            var step4 = new Entities.Step { Name = "step 1", Test = test3 };

            context.Steps.AddOrUpdate(s => s.Id, step1, step2, step3, step4);
        }
    }
}
