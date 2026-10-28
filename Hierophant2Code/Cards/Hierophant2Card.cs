using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Patches.Content;
using BaseLib.Utils;
using Hierophant2.Hierophant2Code.Cards.Token;
using Hierophant2.Hierophant2Code.Character;
using Hierophant2.Hierophant2Code.Extensions;
using Hierophant2.Hierophant2Code.Patches;
using Hierophant2.Hierophant2Code.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Cards;

namespace Hierophant2.Hierophant2Code.Cards;

/// <summary>
/// This is the base class for your mod's cards, which is set up to load the card's images from your mod's resources.
/// When creating a card, right click the Cards folder and create a new file with the Custom Card template.
/// This will generate a class that extends this one.
/// You can also just create the class manually; just make sure to inherit from this class.
/// </summary>
[Pool(typeof(Hierophant2CardPool))]
public abstract class Hierophant2Card(int cost, CardType type, CardRarity rarity, TargetType target) :
    ConstructedCardModel(cost, type, rarity, target)
{
    //Image size:
    //Normal art: 1000x760 (Using 500x380 should also work, it will simply be scaled.)
    //Full art: 606x852
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();

    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190

    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    
    /* *** PIETY MECHANIC *** */
    
    protected ConstructedCardModel WithPiety(int baseVal, int upgrade)
    {
        WithPower<PietyPower>(baseVal, upgrade);
        return this;
    }

    protected ConstructedCardModel WithPietyTip()
    {
        LocString title = new LocString("powers", "HIEROPHANT2-PIETY_POWER.title");
        LocString description = new LocString("powers", "HIEROPHANT2-PIETY_POWER.description");
        WithTip(new TooltipSource(_ => new HoverTip(title, description)));
        return this;
    }
    
    protected static decimal GainedPietyThisTurnCount(CardModel card, Creature? _)
    {
        return CombatManager.Instance.History.Entries
            .OfType<PowerReceivedEntry>()
            .Count(e => e.HappenedThisTurn(card.CombatState) && e.Power is PietyPower && e.Applier?.Player == card.Owner && e.Amount > 0);
    }
    
    public static decimal PietyGainedThisTurn(CardModel card, Creature? _) 
        => CombatManager.Instance.History.Entries.OfType<PowerReceivedEntry>()
            .Where(e => e.HappenedThisTurn(card.CombatState) && e.Power is PietyPower && e.Applier?.Player == card.Owner && e.Amount > 0)
            .Sum(e => e.Amount);
    
    public static decimal CurrentPiety(CardModel card, Creature? _) 
        => card.Owner.Creature.HasPower<PietyPower>() ?
            card.Owner.Creature.GetPowerAmount<PietyPower>() : 0;

    /* *** FERVOR MECHANIC *** */
    
    [CustomEnum]
    public static CardTag KeepFervor;
    
    protected ConstructedCardModel WithFervor(int baseVal, int upgrade)
    {
        WithPower<FervorPower>(baseVal, upgrade);
        return this;
    }

    protected ConstructedCardModel WithFervorTip()
    {
        LocString title = new LocString("powers", "HIEROPHANT2-FERVOR_POWER.title");
        LocString description = new LocString("powers", "HIEROPHANT2-FERVOR_POWER.description");
        WithTip(new TooltipSource(_ => new HoverTip(title, description)));
        return this;
    }
    
    public static decimal CurrentFervor(CardModel card, Creature? creature)
    {
        if (card.Owner.HasPower<FervorPower>())
        {
            return card.Owner.Creature.GetPowerAmount<FervorPower>();
        }

        return 0;
    }
    
    /* *** ENEMY BLOCK MECHANIC *** */

    protected ConstructedCardModel WithEnemyBlock(int baseVal, int upgrade)
    {
        WithVar(new EnemyBlockVar(baseVal).WithUpgrade(upgrade));
        return this;
    }

    public bool AnyEnemyHasBlock()
    {
        if (CombatState != null)
        {
            return CombatState.Enemies.Count(e => e.Block > 0) > 0;
        }

        return false;
    }

    protected static decimal GetEnemyBlock(CardModel _, Creature? creature)
    {
        if (creature != null)
        {
            return creature.Block;
        }

        return 0;
    }

    /* *** GOLD MECHANIC *** */
    
    public static decimal GoldLostThisTurn(CardModel card, Creature? _) 
        => GoldHistorySingleton.Entries
            .Where(e => e.HappenedThisTurn(card.CombatState) && e.Player == card.Owner && e.Amount > 0)
            .Sum(e => e.Amount);
    
    public static decimal GoldLostThisCombat(CardModel card, Creature? _)
        => GoldHistorySingleton.Entries
            .Where(e => e.Player == card.Owner && e.Amount > 0)
            .Sum(e => e.Amount);
    
    public static bool GoldWasLostThisTurn(CardModel card, Creature? _) 
        => CombatManager.Instance.History.Entries
            .OfType<GoldLostEntry>().Any(e => e.HappenedThisTurn(card.CombatState) && e.Player == card.Owner && e.Amount > 0);

    public static decimal CurrentGoldPercent(CardModel card, decimal perc)
    {
        return card.Owner.Gold * perc;
    }
    
    /* *** HOLY MECHANIC *** */
    
    [CustomEnum]
    public static CardTag IsHoly;
    
    protected static decimal HolyCardsInDeck(CardModel card, Creature? _)
    {
        return card.Owner.PlayerCombatState.AllCards
            .Count(c => c.Tags.Contains(IsHoly)) - 1;
    }
    
    /* *** HOARD MECHANIC *** */
    
    protected ConstructedCardModel WithHoard(int baseVal, int upgrade)
    {
        WithVar(new HoardVar(baseVal).WithUpgrade(upgrade));
        // WithHoardTip();
        return this;
    }

    protected ConstructedCardModel WithHoardTip()
    {
        LocString title = new LocString("static_hover_tips", "HIEROPHANT2-HOARD.title");
        LocString description = new LocString("static_hover_tips", "HIEROPHANT2-HOARD.description");
        WithTip(new TooltipSource(_ => new HoverTip(title, description)));
        return this;
    }
    
    /* *** GILDED MECHANIC *** */

    [CustomEnum]
    public static CardTag IsDoubloon;
    
    public static bool IsAnUnupgradedDoubloon(CardModel card)
    {
        return !card.Tags.Contains(IsDoubloon) && !card.IsUpgraded;
    }
    
    protected ConstructedCardModel WithGilded()
    {
        return WithGilded(1);
    }
    
    protected ConstructedCardModel WithGilded(int baseVal)
    {
        WithVar(new GildedVar(baseVal));
        WithGildedTip(baseVal);
        return this;
    }

    protected ConstructedCardModel WithGildedTip(int n)
    {
        LocString title = new LocString("static_hover_tips", "HIEROPHANT2-GILDED.title");
        LocString description;
        if (n > 1)
        {
            description = new LocString("static_hover_tips", "HIEROPHANT2-GILDED_TWO.description");
        }
        else
        {
            description = new LocString("static_hover_tips", "HIEROPHANT2-GILDED.description");
        }

        WithTip(new TooltipSource(_ => new HoverTip(title, description)));
        WithTip(new TooltipSource(_ => HoverTipFactory.FromCard<Doubloon>()));
        return this;
    }
    
    protected static bool PlayedDoubloonThisTurn(CardModel card)
    {
        return CombatManager.Instance.History.Entries
            .OfType<CardPlayFinishedEntry>()
            .Count(p => p.HappenedThisTurn(card.CombatState)
                        && p.CardPlay.Card.Tags.Contains(IsDoubloon)
                        && p.CardPlay.Player == card.Owner) > 0;
    }

    protected static decimal DoubloonsInExhaustPile(CardModel card, Creature? _)
    {
        return PileType.Exhaust.GetPile(card.Owner).Cards
            .Count(c => c.Tags.Contains(IsDoubloon));
    }

    /* *** TITHE MECHANIC *** */
    public static readonly int TitheAmount = 10;
    
    [CustomEnum, KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword Tithe;
    
    public static readonly SpireField<CardModel, bool> HasSingleTurnTithe = new(()=>false);

    public static bool CanHaveTithe(CardModel card)
    {
        return !card.Keywords.Contains(Tithe) && !card.EnergyCost.CostsX;
    }
    
    public static void ApplySingleTurnTithe(CardModel card)
    {
        if (CanHaveTithe(card))
        {
            HasSingleTurnTithe.Set(card, true);
            card.AddKeyword(Tithe);
            NCard.FindOnTable(card)?.UpdateVisuals(card.Pile.Type, CardPreviewMode.Normal);
        }
    }
    
    protected ConstructedCardModel WithTitheTip()
    {
        LocString title = new LocString("card_keywords", "HIEROPHANT2-TITHE.title");
        LocString description = new LocString("card_keywords", "HIEROPHANT2-TITHE.description");
        WithTip(new TooltipSource(_ => new HoverTip(title, description)));
        return this;
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext,
        CombatSide side,
        IEnumerable<Creature> participants)
    {
        if (HasSingleTurnTithe.Get(this))
        {
            RemoveKeyword(Tithe);
            NCard.FindOnTable(this)?.UpdateVisuals(Pile.Type, CardPreviewMode.Normal);
        }
    }
    
    /* *** OTHER STUFF *** */
    
    // counts the total energy cost of cards in hand besides the card given as a parameter
    protected static decimal TotalCostOfCardsInHand(CardModel card, Creature? _)
    {
        return PileType.Hand.GetPile(card.Owner).Cards
            .Where(c => c != card)
            .Sum(c => c.EnergyCost.GetAmountToSpend());
    }
    
    protected static decimal TotalCostOfCardsInDeck(CardModel card, Creature? _)
    {
        return (
            from pile in card.Owner.Piles
            where pile.Type != PileType.Deck
            from c in pile.Cards
            select c.EnergyCost.Canonical
        ).Sum();
    }
}