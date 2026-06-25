using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Items.Weapons
{
    public class Great_Sword : Weapon
    {
        public Great_Sword(string name, float baseDamage, int reStrength, int reDexterity, int reKnowledge) :
            base(name, baseDamage, reStrength, reDexterity, reKnowledge)
        { }

        public override float CalculateDamage(Batalha_Primeira_Era.Core.Character wielder)
        {
            Use();

            return (CurrentDamage + (wielder.Strength * 1.5f));
        }


    }
}
