using Batalha_Primeira_Era.Items.Inventory;
using Batalha_Primeira_Era.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core.Bosses
{
    public class Spectrum : Character, IDiscernment
    {
        public Spectrum(string name, float life, int insight, float defense, int strength, int dexterity, int knowledge, Inventory item) :
        base(name, life, insight, defense, strength, dexterity, knowledge, item)
        {
        }
        public override List<BodyPart> GetTargetTableParts()
        {
            var parts = base.GetTargetTableParts();
            parts.Remove(BodyPart.Legs);
            parts.Remove(BodyPart.Head);
            parts.Remove(BodyPart.Arms);
            return parts;
        }
        public void LifeMultiplier(Character target)
        {
            lifePoint *= 5;
        }

        public void Wraiths()
        {
            Console.WriteLine($"\nThe veil of reality tears open. {Name} cries out to the forces of the Unseen World...");
        }

        // Retorna TRUE se o herói pode atacar, ou FALSE se o Spectrum bloquear e quebrar a arma dele
        public bool DefendAgainstAttacker(Character target)
        {
            // Cenário: Discernimento baixo (Menor que 40) -> BLOQUEIA E QUEBRA A ARMA
            if (target.SpectralInsight < 40)
            {
                Console.WriteLine($"\n==================================================");
                Console.WriteLine($"{target.Name} tries to strike {Name}, but their eyes cannot focus on the spectral plane!");
                Console.WriteLine($"{Name} raises an iron hand and speaks a direct order into the mind of {target.Name}:");
                Console.WriteLine($"— \"BREAK...\"");
                Console.WriteLine($"==================================================");

                if (target.EquippedWeapon != null)
                {
                    Console.WriteLine($"The dark aura cracks! The weapon {target.EquippedWeapon.Name} loses its glow and SHATTERS completely!");
                    
                    // 1. Primeiro a propriedade Durability da sua classe Weapon zera o item!
                    target.EquippedWeapon.Durability = 0f; 

                    // 2. AGORA PRINTA! Puxa direto da arma para provar na tela que ela está zerada!
                    Console.WriteLine($"Item Status: {target.EquippedWeapon.Name} | Current Durability: {target.EquippedWeapon.Durability} (LOCKED AT ZERO!)");
                }

                Console.WriteLine($"{target.Name} missed the attack and is in shock!");
                return false; // Retorna false: o ataque foi cancelado!
            }

            // Cenário: Discernimento alto (40 ou mais) -> BOSS FIGHT NORMAL
            Console.WriteLine($"\n{target.Name} resists the aura of {Name} with {target.SpectralInsight} Discernment! The battle continues!");
            return true; // Retorna true: pode desferir o golpe!
        }

    }
}