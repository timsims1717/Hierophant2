using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Hierophant2.Hierophant2Code.Character;

public class HoardVar : DynamicVar
{
    public const string DefaultName = "Hoard";

    public HoardVar(int hoard)
        : base(DefaultName, hoard)
    {
    }

    public HoardVar(string name, int hoard)
        : base(name, hoard)
    {
        this.WithTooltip("HEIROPHANT2-HOARD");
    }
}