using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using Hierophant2.Hierophant2Code.Extensions;
using Godot;
using Hierophant2.Hierophant2Code.Cards;
using Hierophant2.Hierophant2Code.Cards.Basic;
using Hierophant2.Hierophant2Code.Relics;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;

namespace Hierophant2.Hierophant2Code.Character;
  public class Hierophant2 : PlaceholderCharacterModel
{
    public const string CharacterId = "Hierophant2";

    public static readonly Color Color = new("E3BE00");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Masculine;
    public override int StartingHp => 70;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeHierophant>(),
        ModelDb.Card<StrikeHierophant>(),
        ModelDb.Card<StrikeHierophant>(),
        ModelDb.Card<StrikeHierophant>(),
        ModelDb.Card<DefendHierophant>(),
        ModelDb.Card<DefendHierophant>(),
        ModelDb.Card<DefendHierophant>(),
        ModelDb.Card<DefendHierophant>(),
        ModelDb.Card<Chastise>(),
        ModelDb.Card<MorningPrayer>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<HallowedStipend>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<Hierophant2CardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<Hierophant2RelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<Hierophant2PotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomIconTexturePath => "character_icon_hierophant.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_hierophant.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_hierophant_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_hierophant.png".CharacterUiPath();
}