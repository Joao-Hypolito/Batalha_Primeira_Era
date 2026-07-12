using Batalha_Primeira_Era.Core.Behaviors;
using Batalha_Primeira_Era.Items.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core.Enemies
{
    public class Lamenters : Character
    {
        public Lamenters(string name, float life, int insight, float defense, int strength, int dexterity, int knowledge, Inventory item, Horde horde) 
        : base(name, life, insight, defense, strength, dexterity, knowledge, item)
        {
        }

        public override void ReceiveDamage(float damage, BodyPart hitPart)
        {
            base.ReceiveDamage(damage, hitPart);
        }  
    }
}
