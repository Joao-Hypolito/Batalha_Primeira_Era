using Batalha_Primeira_Era.Core.Behaviors;
using Batalha_Primeira_Era.Items.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core.Enemies
{
    // Changed to public so your Program.cs can instantiate it without inheritance issues
    public class Lich : Character
    {
        // The Lich now has an instance of its AbsorbSoul mechanic
        private AbsorbSoul _absorbSoulBehavior;

        // The constructor receives the Lich's data and the Horde it will monitor
        public Lich(string name, float life, int insight, float defense, int strength, int dexterity, int knowledge, Inventory item, Horde horde) 
            : base(name, life, insight, defense, strength, dexterity, knowledge, item)
        {
            // Initializes the mechanic and passes the horde to start monitoring the Goblins
            _absorbSoulBehavior = new AbsorbSoul();
            _absorbSoulBehavior.SetHorde(horde);
        }

        // Property for the Lich to get the updated damage from within AbsorbSoul
        // When you trigger the Lich's attack command, use "lich.CurrentDamage"
        public int CurrentDamage 
        {
            get { return _absorbSoulBehavior.CurrentDamage; }
        }

        public override void ReceiveDamage(float damage, BodyPart hitPart)
        {
            base.ReceiveDamage(damage, hitPart);

            if (this.lifePoint <= 0)
            {
                Console.WriteLine($"{Name} has been vanquished!");
            }
        }           
    }
}