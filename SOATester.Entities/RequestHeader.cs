namespace SOATester.Entities {
    public class RequestHeader {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? ScenarioId { get; set; }
        public int? TestId { get; set; }
        public int? StepId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual Project Project { get; set; }
        public virtual Scenario Scenario { get; set; }
        public virtual Test Test { get; set; }
        public virtual Step Step { get; set; }
    }
}
