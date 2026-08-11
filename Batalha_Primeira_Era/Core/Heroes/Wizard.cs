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

                // Dano Base da Magia
                float baseMagicDamage = 15f + (this.Knowledge * 1.8f);
        
                // Bônus da Arma: Se tiver arma equipada e não estiver quebrada, calcula o dano dela
                float weaponBonus = 0f;
                if (this.EquippedWeapon != null && !this.EquippedWeapon.IsBroken)
                {
                    weaponBonus = this.EquippedWeapon.CalculateDamage(this);
                }

                float totalMagicDamage = baseMagicDamage + weaponBonus;

                // Aplica o abatimento de armadura
                float finalDamage = MathF.Max(1f, totalMagicDamage - (target.Armor * 0.3f));

                target.ReceiveDamage(finalDamage, BodyPart.Torso); 

                return finalDamage;
            }    
            return 0f;
        }

    }
}
