using System.Collections.Generic;
public static class Const
{
    public static readonly string[] LEVEL_NAMES = new string[]
    {
        "Prologue",
        "Easy-1",
        "Easy-2",
        "Easy-3",
        "Intermediate-1",
        "Hard-1"
    };

    public static readonly Collectible.Metal[] COLLECTIBLE_METALS = new Collectible.Metal[]
    {
        Collectible.Metal.Copper,
        Collectible.Metal.Silver,
        Collectible.Metal.Gold
    };

    public static readonly Dictionary<string, string> LEVEL_TO_SHAPE = new Dictionary<string, string>()
    {
        { "Intermediate-1", "Pyramid" },
        { "Hard-1", "Diamond" }
    };
}
