using BaseLib.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Hierophant2.Hierophant2Code.Cards.Rare;

public class HolyHandGrenade : Hierophant2Card
{
    public HolyHandGrenade() : base(1,
        CardType.Skill,
        CardRarity.Rare,
        TargetType.AnyEnemy)
    {
        WithVar(new DynamicVar("Turns", 3));
        WithVar(new DynamicVar("BombDamage", 30).WithUpgrade(10));
        WithKeyword(CardKeyword.Exhaust);
        WithTags(IsHoly);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        (await PowerCmd.Apply<HolyHandGrenadePower>(choiceContext,
            play.Target,
            DynamicVars["Turns"].BaseValue,
            Owner.Creature, this)
            ).SetDamage(DynamicVars["BombDamage"].BaseValue);
    }
}