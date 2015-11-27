namespace SOATester.Infrastructure.Events {
    public enum ChosenItemType { PROJECT, SCENARIO, TEST, STEP }

    public enum RunKind { START, STOP, PAUSE }

    public enum StartupActivity { PROJECTS_INIT, WORKSPACE_INIT }
}
