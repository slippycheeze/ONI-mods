namespace SlippyCheeze.SupportCode;

public interface IModMain {
    public IModMain Instance { get; }
    public Harmony  Harmony  { get; }
    public PBuildingManager BuildingManager { get; }
}
