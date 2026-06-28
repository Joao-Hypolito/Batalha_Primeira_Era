using Batalha_Primeira_Era.Items.Inventory;
using Batalha_Primeira_Era.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core.Enimies
{
    public class Goblin : Character
    {
        private Horde _myHorde;

        public Goblin(string name, float life, int insight, float defense, int strength, int dexterity, int knowledge, Inventory item, Horde horde) 
        : base(name, life, insight, defense, strength, dexterity, knowledge, item)
        {
        _myHorde = horde;
        }

        public override void ReceiveDamage(float damage, BodyPart hitPart)
        {
            base.ReceiveDamage(damage, hitPart);

            // Se o Goblin morrer, ele sai da agregação e o bando enfraquece!
            if (this.lifePont <= 0 && _myHorde != null)
            {
                Console.WriteLine($"{Name} has fallen! The horde loses morale!");
                _myHorde.RemoveMember(this);
            }
        }           
    }
}
