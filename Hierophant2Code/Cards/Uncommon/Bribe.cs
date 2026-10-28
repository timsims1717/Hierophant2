using BaseLib.Utils;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Hierophant2.Hierophant2Code.Cards.Uncommon;

public class Bribe : Hierophant2Card
{
    public Bribe() : base(4,
        CardType.Skill,
        CardRarity.Uncommon,
        TargetType.AllEnemies)
    {
        WithPower<BribedPower>(15, 3);
        WithKeyword(Tithe);
        WithKeyword(CardKeyword.Exhaust);
        WithTip(new TooltipSource(_ => HoverTipFactory.FromPower<StrengthPower>()));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState == null)
        {
            return;
        }
        foreach (Creature enemy in CombatState.Enemies)
        {
            await PowerCmd.Apply<BribedPower>(choiceContext, enemy,
                DynamicVars[nameof(BribedPower)].BaseValue,
                Owner.Creature, this);
        }
    }
}