using Batalha_Primeira_Era.Core;
using static Batalha_Primeira_Era.Core.Character;
using Batalha_Primeira_Era.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Items.BossAction.DragonAtack
{
    public class DragonGaze: Weapon
    {
        public DragonGaze(string name, float baseDamage, int reStrength, int reDexterity, int reKnowledge) :
            base(name, baseDamage, reStrength, reDexterity, reKnowledge)
        { }


        public override float CalculateDamage(Character wielder)
        {
            float finalDamage = this.BaseDamage + (wielder.Strength * 1.5f);

            return finalDamage;
        }

        public void Dragongaze(Character wielder)
        {

            Random random = new Random();

            bool Raffle = random.Next(2) == 1;

            if (Raffle)
            {
                Console.WriteLine($"\n{wielder.Name} is terrified");
            }
            else
            {
                Console.WriteLine($"\n{wielder.Name} resisted the gaze of {this.Name}!");
            }
        }

    }
}
