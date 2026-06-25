using Batalha_Primeira_Era.Items.Inventory;
using Batalha_Primeira_Era.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core.Heroes
{
    public class Archer : Character, IRanged<Bow>, IDagger<Dagger>, IAgile
    {
        public Bow equippedBow { get; set; }
        public Dagger equippedDagger { get; set; }
        public Archer(string name, float life, int insight, float defense, int strength, int dexterity, int knowledge, Inventory item) :
        base(name, life, insight, defense, strength, dexterity, knowledge, item)
        {
            equippedBow = null;
            equippedDagger = null;
        }

        public void Dodge() => Console.WriteLine($"{Name} vanished into the shadows and dodged!");
        public override void ReceiveDamage(float damage, BodyPart hitPart)
        {
            if(new Random().NextDouble() < 0.3)
            {
                Dodge();
                Console.WriteLine($"{Name} suffered no damage!");
            }
            else
            {

                    base.ReceiveDamage(damage, hitPart);              
            }
        }
    }
}
