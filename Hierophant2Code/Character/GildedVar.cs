using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Hierophant2.Hierophant2Code.Character;

public class GildedVar : DynamicVar
{
    public const string DefaultName = "Gilded";

    public GildedVar(int gilded)
        : base(DefaultName, gilded)
    {
    }

    public GildedVar(string name, int gilded)
        : base(name, gilded)
    {
        this.WithTooltip("HEIROPHANT2-GILDED");
    }
}