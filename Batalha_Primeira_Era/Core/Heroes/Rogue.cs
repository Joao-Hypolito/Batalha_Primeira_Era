using Batalha_Primeira_Era.Items.Inventory;
using Batalha_Primeira_Era.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core.Heroes
{
    public class Rogue : Character, IAgile, IDagger<Dagger>
    {
        public Dagger equippedDagger { get; set; }
        public Rogue(string name, float life, int insight, float defense, int strength, int dexterity, int knowledge, Inventory item) :
        base(name, life, insight, defense, strength, dexterity, knowledge, item)
        {
            equippedDagger = null;
        }

        public void Dodge() => Console.WriteLine($"{Name} vanished into the shadows and dodged!");

        public override void ReceiveDamage(float damage, BodyPart hitPart)
        {
            // Lógica: 30% de chance de esquivar se for um Ladino
            if (new Random().NextDouble() < 0.3)
            {
                Dodge();
                Console.WriteLine($"{Name} suffered no damage!");
            }
            else
            {
                base.ReceiveDamage(damage, hitPart); // Se falhar, toma o dano normal
            }


        }
    }
}