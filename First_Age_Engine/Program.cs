using System;
using System.Collections.Generic;
using First_Age_Engine.Core;
using First_Age_Engine.Behaviors;
using First_Age_Engine.Core;
using First_Age_Engine.Items.Weapons;

namespace First_Age_Engine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== BATALHA DA PRIMEIRA ERA (SISTEMA MODULAR) ===\n");

            // 1. CLASSES DE RPG
            CharacterClass rogueClass = new CharacterClass("Ladino", new List<WeaponType> { WeaponType.Dagger, WeaponType.Bow });
            CharacterClass archerClass = new CharacterClass("Arqueiro", new List<WeaponType> { WeaponType.Bow, WeaponType.Dagger });
            CharacterClass monsterClass = new CharacterClass("Monstro", new List<WeaponType> { WeaponType.Sword, WeaponType.GreatSword, WeaponType.Dagger });

            // 2. ARMAS
            Weapon sting = new Weapon("Sting", WeaponType.Dagger, baseDamage: 30f, reqStr: 10, reqDex: 10, reqKnw: 0, dexScale: 0.8f);
            Weapon elvenBow = new Weapon("Arco Élfico", WeaponType.Bow, baseDamage: 40f, reqStr: 5, reqDex: 20, reqKnw: 0, dexScale: 1.0f);
            Weapon morgul = new Weapon("Lâmina de Morgul", WeaponType.GreatSword, baseDamage: 50f, reqStr: 20, reqDex: 10, reqKnw: 10, strScale: 1.0f);

            // 3. PERSONAGENS (HERÓIS) - Usando a chamada fluida estilo Builder
            Character frodo = new Character("Frodo", rogueClass)
                .WithLife(100f)
                .WithSpectral(60)
                .WithArmor(10f)
                .WithStrenght(15)
                .WithDextery(30)
                .WithKnowledge(10)
                .WithEquippedInventory(new Inventory(5));

            Character legolas = new Character("Legolas", archerClass)
                .WithLife(120f)
                .WithSpectral(50)
                .WithArmor(15f)
                .WithStrenght(20)
                .WithDextery(50)
                .WithKnowledge(15)
                .WithEquippedInventory(new Inventory(5));

            frodo.EquipWeapon(sting);
            legolas.EquipWeapon(elvenBow);

            // 4. PERSONAGENS (INIMIGOS E CHEFES)
            Character goblin1 = new Character("Goblin Slasher", monsterClass)
                .WithLife(30f)
                .WithSpectral(0)
                .WithArmor(0f)
                .WithStrenght(10)
                .WithDextery(15)
                .WithKnowledge(0)
                .WithEquippedInventory(new Inventory(5));

            Character goblin2 = new Character("Goblin Archer", monsterClass)
                .WithLife(30f)
                .WithSpectral(0)
                .WithArmor(0f)
                .WithStrenght(10)
                .WithDextery(15)
                .WithKnowledge(0)
                .WithEquippedInventory(new Inventory(5));

            Character sulyvahn = new Character("Pontiff Sulyvahn (Lich)", monsterClass)
                .WithLife(150f)
                .WithSpectral(80)
                .WithArmor(30f)
                .WithStrenght(25)
                .WithDextery(20)
                .WithKnowledge(70)
                .WithEquippedInventory(new Inventory(5));

            Character lamenter = new Character("Lamenter", monsterClass)
                .WithLife(100f)
                .WithSpectral(50)
                .WithArmor(20f)
                .WithStrenght(30)
                .WithDextery(20)
                .WithKnowledge(10)
                .WithEquippedInventory(new Inventory(5));

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
            lamenter._lifePoint = 1f; // Forçando vida baixa pra testar imortalidade com o atributo atualizado (_lifePoint)
            frodo.TakeAction(lamenter); // Vai ativar o Imortality.cs!

            Console.WriteLine("\n--- Frodo tenta atacar o Lamenter ENQUANTO ESTÁ INVULNERÁVEL ---");
            frodo.TakeAction(lamenter); // Ataque é bloqueado!

            Console.WriteLine("\n=== FIM DO TESTE DE BEHAVIORS ===");
        }
    }
}