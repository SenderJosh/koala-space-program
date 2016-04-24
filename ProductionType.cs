using System.Collections.Generic;

[System.Serializable]
public class ProductionType
{

    public static readonly ProductionType BUILD_SUPPLIES = new ProductionType();
    public static readonly ProductionType FOOD = new ProductionType();
    public static readonly ProductionType WATER = new ProductionType();
    public static readonly ProductionType RESEARCH = new ProductionType();

    public static IEnumerable<ProductionType> Values
    {
        get
        {
            yield return BUILD_SUPPLIES;
            yield return FOOD;
            yield return WATER;
            yield return RESEARCH;
        }
    }

    ProductionType() { }
}