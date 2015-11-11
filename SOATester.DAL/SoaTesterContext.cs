using SOATester.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SOATester.DAL {
    public class SoaTesterContext : DbContext {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<RequestHeader> RequestHeaders { get; set; }

        public SoaTesterContext() : base("SoaTester") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            /*modelBuilder.Entity<RequestHeader>().HasRequired(h => h.Project).WithMany(p => p.RequestHeaders).HasForeignKey(h => h.ProjectId);
            modelBuilder.Entity<RequestHeader>().HasRequired(h => h.Scenario).WithMany(s => s.RequestHeaders).HasForeignKey(h => h.ScenarioId);
            modelBuilder.Entity<RequestHeader>().HasRequired(h => h.Test).WithMany(t => t.RequestHeaders).HasForeignKey(h => h.TestId);
            modelBuilder.Entity<RequestHeader>().HasRequired(h => h.Step).WithMany(s => s.RequestHeaders).HasForeignKey(h => h.StepId);*/
        }
    }
}
