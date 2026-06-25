using Batalha_Primeira_Era.Items.BossAction;
using Batalha_Primeira_Era.Items.Inventory;
using Batalha_Primeira_Era.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core.Bosses
{
    public class Dragon : Character
    {
        public Dragon(string name, float life, int insight, float defense, int strength, int dexterity, int knowledge, Inventory item) :
        base(name, life, insight, defense, strength, dexterity, knowledge, item){}
        public override List<BodyPart> GetTargetTableParts()
        {
            var parts = base.GetTargetTableParts();
            parts.Add(BodyPart.Wings);
            parts.Add(BodyPart.Belly);
            return parts;
        }
        public void LifeMultiplier(Character target)
        {
            lifePont *= 10;
        }

    }
}
