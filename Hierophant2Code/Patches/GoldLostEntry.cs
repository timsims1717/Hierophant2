using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;

namespace Hierophant2.Hierophant2Code.Patches;

public class GoldLostEntry : CombatHistoryEntry
{
    public Player Player { get; }

    public Decimal Amount { get; }
    
    public GoldLossType GoldLossType { get; }

    public override string Description
    {
        get
        {
            switch (GoldLossType)
            {
                case GoldLossType.Spent:
                    return $"{Player.NetId} spent {Amount} gold";
                case GoldLossType.Stolen:
                    return $"{Player.NetId} had {Amount} gold stolen";
                default:
                    return $"{Player.NetId} lost {Amount} gold";
            }
        }
    }

    public GoldLostEntry(
        Player player,
        Decimal amount,
        GoldLossType lossType,
        int roundNumber,
        CombatSide currentSide,
        CombatHistory history,
        IEnumerable<Player> players)
        : base(player.Creature, roundNumber, currentSide, history, players)
    {
        Amount = amount;
        Player = player;
        GoldLossType = lossType;
    }
}
