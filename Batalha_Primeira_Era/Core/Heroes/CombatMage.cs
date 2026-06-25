using Batalha_Primeira_Era.Items.Inventory;
using Batalha_Primeira_Era.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core.Heroes
{
    public class CombatMage : Character, IGreat_Sword<Great_Sword>, ISword<Sword>
    {
        public Great_Sword equippedGreat_Sword { get; set;}
        public Sword equippedSword { get; set;}
        public CombatMage(string name, float life, int insight, float defense, int strength, int dexterity, int knowledge, Inventory item) :
        base(name, life, insight, defense, strength, dexterity, knowledge, item)
        {
            equippedGreat_Sword = null;
            equippedSword = null;
        }

        public override void ReceiveDamage(float damage, BodyPart hitPart)
        {
            base.ReceiveDamage(damage, hitPart);
        }
    }
}
