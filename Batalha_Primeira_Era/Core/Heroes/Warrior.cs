using Batalha_Primeira_Era.Items.Inventory;
using Batalha_Primeira_Era.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core.Heroes
{
    public class Warrior : Character, IGreat_Sword<Great_Sword>, IGreat_Axe<Great_Axe>, ISword<Sword>, IShield<Shield>
    {
        public Great_Sword equippedGreat_Sword { get; set; }
        public Great_Axe equippedGreat_Axe { get; set; }
        public Sword equippedSword {  get; set; }
        public Shield equippedShield { get; set; }

        public Warrior(string name, float life, int insight, float defense, int strength, int dexterity, int knowledge, Inventory item) :
        base(name, life, insight, defense, strength, dexterity, knowledge, item)
        {
            equippedGreat_Sword = null;
            equippedGreat_Axe = null;
            equippedSword = null;
            equippedShield = null;
        }


        public override void ReceiveDamage(float damage, BodyPart hitPart)
        {
            base.ReceiveDamage(damage, hitPart);
        }

    }
}
