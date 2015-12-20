namespace SOATester.Infrastructure.Events {
    public enum ChosenItemType { NONE, PROJECT, SCENARIO, TEST, STEP }

    public enum RunKind { NONE, START, STOP, PAUSE }

    public enum StartupActivity { NONE, PROJECTS_INIT, WORKSPACE_INIT }
}
