namespace SOATester.Infrastructure.Plugins {
    public interface IPlugin {
        bool IsActive { get; set; }
        int Priority { get; set; }
    }
}
