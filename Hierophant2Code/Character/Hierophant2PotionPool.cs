using BaseLib.Abstracts;
using Hierophant2.Hierophant2Code.Extensions;
using Godot;

namespace Hierophant2.Hierophant2Code.Character;

public class Hierophant2PotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => Hierophant2.Color;


    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}