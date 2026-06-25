using Batalha_Primeira_Era.Core;
using static Batalha_Primeira_Era.Core.Character;
using Batalha_Primeira_Era.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Items.BossAction.DragonAtack
{
    public class Grazing: Weapon
    {
        public Grazing(string name, float baseDamage, int reStrength, int reDexterity, int reKnowledge) :
            base(name, baseDamage, reStrength, reDexterity, reKnowledge)
        { }
        

        public override float CalculateDamage(Character wielder)
        {
            float variation = new Random().Next(90, 110) / 100f;
            return (BaseDamage + (wielder.Strength * 1.5f)) * variation;
        }
    }
}
