using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Hierophant2.Hierophant2Code.Character;

public class EnemyBlockVar : DynamicVar
{
    public const string DefaultName = "EnemyBlock";

    public EnemyBlockVar(int enemyBlock)
        : base(DefaultName, enemyBlock)
    {
    }

    public EnemyBlockVar(string name, int enemyBlock)
        : base(name, enemyBlock)
    {
    }
}