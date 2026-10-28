using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Hierophant2.Hierophant2Code.Character;
using Hierophant2.Hierophant2Code.Extensions;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace Hierophant2.Hierophant2Code.Cards.Token;
[Pool(typeof(TokenCardPool))]
public class Doubloon : ConstructedCardModel
{
    public Doubloon() : base(0,
        CardType.Skill,
        CardRarity.Token,
        TargetType.None)
    {
        WithTags(Hierophant2Card.IsDoubloon);
        WithKeyword(CardKeyword.Exhaust);
        WithCards(1, 1);
        WithVar(new GoldVar(4));
        WithVar(new HoardVar(2));
    }
    
    public override bool CanBeGeneratedInCombat => false;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
        await PlayerCmd.LoseGold(DynamicVars.Gold.IntValue, Owner);
    }
    
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}