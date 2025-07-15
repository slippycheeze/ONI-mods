namespace SlippyCheeze.MoreFoodCooksWhenHeated;

// [AddComponentMenu("KMonoBehaviour/scripts/TemperatureCookable")]
public class ImprovedTemperatureCookable: KMonoBehaviour, ISim1000ms {
    [MyCmpReq]
    private PrimaryElement element = null!;

    [MyCmpGet]
    private KSelectable? selectable = null;


    // IDK if it better than the Klei default, but ... at least it correct to their usage. :)
    public float cookTemperature = Constants.CELSIUS2KELVIN + 71;

    public string? cookedID = null;

    // multiple input mass by this to get output mass.
    // eg: the recipe for Meat => CookedMeat is 2KG meat to 1KG CookedMeat, so 0.5 here.
    public float cookedMassMultiplier = 1f;


    public void Sim1000ms(float dt) {
        if (this.cookedID is not null && element.Temperature > cookTemperature)
            TransformIntoCookedFood();
    }

    public virtual void TransformIntoCookedFood() {
        GameObject go = Util.KInstantiate(
            Assets.GetPrefab(cookedID),
            this.transform.GetPosition() with { z = Grid.GetLayerZ(Grid.SceneLayer.Ore) }
        );
        go.SetActive(true);

        if (selectable?.IsSelected == true)
            SelectTool.Instance?.Select(go.GetComponent<KSelectable>());

        PrimaryElement food = go.GetComponent<PrimaryElement>();
        food.Temperature    = element.Temperature;
        food.Mass           = element.Mass * cookedMassMultiplier;

        // delete ourselves, we done been replaced, friends.
        gameObject.DeleteObject();
    }
}
