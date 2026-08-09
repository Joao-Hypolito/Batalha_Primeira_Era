using Batalha_Primeira_Era.Items.Inventory;
using Batalha_Primeira_Era.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core.Heroes
{
    public class Wizard : Character
    {
        public Wizard(string name, float life, int insight, float defense, int strength, int dexterity, int knowledge, Inventory item) :
        base(name, life, insight, defense, strength, dexterity, knowledge, item)
        {
        }

        public override void ReceiveDamage(float damage, BodyPart hitPart)
        {
            base.ReceiveDamage(damage, hitPart);
        }
        public float CastArcaneBlast(Character target)
        {
            
            if (this.SpectralInsight >= 10) 
                {
                    this.SpectralInsight -= 10;

                    float magicDamage = 15f + (this.Knowledge * 1.8f);
        
                    float finalDamage = MathF.Max(1f, magicDamage - (target.Armor * 0.3f));

                    target.ReceiveDamage(finalDamage, BodyPart.Torso); 

                    return finalDamage;
                }    
            return 0f;
        }

    }
}
