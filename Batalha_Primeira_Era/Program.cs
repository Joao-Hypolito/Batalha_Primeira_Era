using System;
using System.Collections.Generic;
using Batalha_Primeira_Era.Core;
using Batalha_Primeira_Era.Core.Behaviors;
using Batalha_Primeira_Era.Items.Inventory;
using Batalha_Primeira_Era.Items.Weapons;

namespace Batalha_Primeira_Era
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== BATALHA DA PRIMEIRA ERA (SISTEMA MODULAR) ===\n");

            // 1. CLASSES DE RPG
            HeroClass rogueClass = new HeroClass("Ladino", new List<WeaponType> { WeaponType.Dagger, WeaponType.Bow });
            HeroClass archerClass = new HeroClass("Arqueiro", new List<WeaponType> { WeaponType.Bow, WeaponType.Dagger });
            HeroClass monsterClass = new HeroClass("Monstro", new List<WeaponType> { WeaponType.Sword, WeaponType.GreatSword, WeaponType.Dagger });

            // 2. ARMAS
            Weapon sting = new Weapon("Sting", WeaponType.Dagger, baseDamage: 30f, reqStr: 10, reqDex: 10, reqKnw: 0, dexScale: 0.8f);
            Weapon elvenBow = new Weapon("Arco Élfico", WeaponType.Bow, baseDamage: 40f, reqStr: 5, reqDex: 20, reqKnw: 0, dexScale: 1.0f);
            Weapon morgul = new Weapon("Lâmina de Morgul", WeaponType.GreatSword, baseDamage: 50f, reqStr: 20, reqDex: 10, reqKnw: 10, strScale: 1.0f);

            // 3. PERSONAGENS (HERÓIS)
            Character frodo = new Character("Frodo", rogueClass, life: 100f, insight: 60, defense: 10f, strength: 15, dexterity: 30, knowledge: 10, new Inventory(5));
            Character legolas = new Character("Legolas", archerClass, life: 120f, insight: 50, defense: 15f, strength: 20, dexterity: 50, knowledge: 15, new Inventory(5));

            frodo.EquipWeapon(sting);
            legolas.EquipWeapon(elvenBow);

            // 4. PERSONAGENS (INIMIGOS E CHEFES)
            Character goblin1 = new Character("Goblin Slasher", monsterClass, life: 30f, insight: 0, defense: 0f, strength: 10, dexterity: 15, knowledge: 0, new Inventory(5));
            Character goblin2 = new Character("Goblin Archer", monsterClass, life: 30f, insight: 0, defense: 0f, strength: 10, dexterity: 15, knowledge: 0, new Inventory(5));
            Character sulyvahn = new Character("Pontiff Sulyvahn (Lich)", monsterClass, life: 150f, insight: 80, defense: 30f, strength: 25, dexterity: 20, knowledge: 70, new Inventory(5));
            Character lamenter = new Character("Lamenter", monsterClass, life: 100f, insight: 50, defense: 20f, strength: 30, dexterity: 20, knowledge: 10, new Inventory(5));

            sulyvahn.EquipWeapon(morgul);

            // ============================================================
            // 5. CONECTANDO OS BEHAVIORS (A MÁGICA ACONTECE AQUI!)
            // ============================================================

            // A) Configurando a Horda de Goblins
            Horde orcHorde = new Horde("Gorgoroth Vanguard");
            orcHorde.AddMember(goblin1);
            orcHorde.AddMember(goblin2);
            goblin1.MyHorde = orcHorde;
            goblin2.MyHorde = orcHorde;

            // B) Configurando a Imortalidade do Lamenter
            lamenter.ImmortalityBehavior = new Imortality(lamenter, maxLife: 100f);

            // C) Configurando o AbsorbSoul do Lich
            AbsorbSoul lichSoulAbsorb = new AbsorbSoul();
            lichSoulAbsorb.SetHorde(orcHorde); // O Lich passa a escutar quando membros da horda morrem!

            // ============================================================
            // 6. SIMULAÇÃO DE BATALHA COM OS BEHAVIORS FUNCIONANDO
            // ============================================================

            Console.WriteLine($"\nDano atual do Lich (AbsorbSoul): {lichSoulAbsorb.CurrentDamage}");

            Console.WriteLine("\n--- Legolas ataca e mata o Goblin 1 ---");
            // Damos dano suficiente para matar o goblin
            legolas.TakeAction(goblin1); 
            legolas.TakeAction(goblin1); 

            // O AbsorbSoul dispara o evento sozinho quando o goblin morre!
            Console.WriteLine($"\nDano do Lich APÓS a morte do Goblin: {lichSoulAbsorb.CurrentDamage}");

            Console.WriteLine("\n--- Frodo ataca o Lamenter até quase matar ---");
            lamenter.lifePoint = 1f; // Forçando vida baixa pra testar imortalidade
            frodo.TakeAction(lamenter); // Vai ativar o Imortality.cs!

            Console.WriteLine("\n--- Frodo tenta atacar o Lamenter MENTRAS ESTÁ INVULNERÁVEL ---");
            frodo.TakeAction(lamenter); // Ataque é bloqueado!

            Console.WriteLine("\n=== FIM DO TESTE DE BEHAVIORS ===");
        }
    }
}