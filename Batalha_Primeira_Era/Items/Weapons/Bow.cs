using System;
using System.Collections.Generic;
using System.Text;
using Batalha_Primeira_Era.Core;

namespace Batalha_Primeira_Era.Items.Weapons
{
    public class Bow : Weapon
    {
        public Bow(string name, float baseDamage, int reStrength, int reDexterity, int reKnowledge) :
            base(name, baseDamage, reStrength, reDexterity, reKnowledge)
        { }

        public override float CalculateDamage(Batalha_Primeira_Era.Core.Character wielder)
        {
            Use();

            return (CurrentDamage + (wielder.Dexterity * 1.5f));
        }


    }
}
