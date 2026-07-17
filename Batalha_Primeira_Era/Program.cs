using Batalha_Primeira_Era.Core.Behaviors;
using Batalha_Primeira_Era.Core.Bosses;
using Batalha_Primeira_Era.Core.Enemies;
using Batalha_Primeira_Era.Core.Heroes;
using Batalha_Primeira_Era.Items.BossAction.DragonAtack;
using Batalha_Primeira_Era.Items.Inventory;
using Batalha_Primeira_Era.Items.Weapons;

namespace Batalha_Primeira_Era
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Horde orcHorde = new Horde("Gorgoroth Vanguard");
            
            Inventory goblin1Backpack = new Inventory(5);
            Inventory goblin2Backpack = new Inventory(5);
            Inventory rogueBackpack = new Inventory(5);
            Inventory archerBackpack = new Inventory(5);
            Inventory mageBackpack = new Inventory(5);
            Inventory dragonBackpack = new Inventory(5);
            Inventory wraithBackpack = new Inventory(5);
            Inventory lichBackpack = new Inventory(5);
            Inventory LamenterBackpack = new Inventory(5);

            Bow arcomagico = new Bow("ArcoMagico", 50, 3, 20, 40, 2f, 7f, 10f);
            Bow elvenBow = new Bow("Arco do Vento Élfico", 10f, 5, 5, 25, 10, 3.0f, 0.0f);
            Dagger sting = new Dagger("Sting", 97f, 10, 10, 10, 2.5f, 0.2f, 0.0f);
            Staff staffinitial = new Staff("Staff", 80f, 10, 9, 30, 0.0f, 0.0f, 3.5f);
            Great_Sword morgul = new Great_Sword("Morgul", 50f, 30, 25, 56, 0.2f, 3.0f, 1.5f);
            Grazing grazingDragon = new Grazing("Grazing", 80f, 10, 9, 30, 1.5f, 1.5f, 0.0f);
            DragonGaze gaze = new DragonGaze("Dragon Gaze", 120f, 10, 15, 40, 1.0f, 0.0f, 4.0f);
            Great_Sword bloodsword = new Great_Sword("Lamenter Sword", 50f, 30, 25, 56, 0.2f, 3.0f, 1.5f);

            Rogue frodo = new Rogue("Frodo", 100f, 60, 20, 40, 50, 15, rogueBackpack);
            Archer legolas = new Archer("Legolas", 87f, 60, 20, 40, 50, 15, archerBackpack);
            Wizard galadriel = new Wizard("Galadriel", 90f, 70, 70, 14, 17, 57, mageBackpack);
            Dragon glaurung = new Dragon("Glaurung", 100f, 78, 80, 50, 30, 40, dragonBackpack);
            Spectrum nazgul = new Spectrum("Agnmar", 100f, 78, 80, 45, 78, 67, wraithBackpack);
            Goblin goblin1 = new Goblin("Goblin Slasher", 40f, 10, 0, 0, 20, 5, goblin1Backpack, orcHorde);
            Goblin goblin2 = new Goblin("Goblin Archer", 40f, 10, 0, 0, 20, 5, goblin2Backpack, orcHorde);
            Lich sulyvahn = new Lich("Pontiff Sulyvahn", 120f, 80, 30, 25, 20, 70, lichBackpack, orcHorde);
            Lamenters lamenter = new Lamenters("Agnmar", 100f, 78, 80, 45, 78, 67, LamenterBackpack);

            orcHorde.AddMember(goblin1);
            orcHorde.AddMember(goblin2);
            
            goblin1Backpack.AddItem(sting);
            goblin2Backpack.AddItem(sting);
            rogueBackpack.AddItem(sting);
            archerBackpack.AddItem(elvenBow);
            archerBackpack.AddItem(arcomagico);
            mageBackpack.AddItem(staffinitial);
            dragonBackpack.AddItem(gaze);
            dragonBackpack.AddItem(grazingDragon);
            wraithBackpack.AddItem(morgul);
            lichBackpack.AddItem(morgul);
            LamenterBackpack.AddItem(bloodsword);

            dragonBackpack.EquipWeaponFromSlot(1, glaurung); 
            rogueBackpack.EquipWeaponFromSlot(0, frodo);    
            rogueBackpack.EquipWeaponFromSlot(0, legolas); 
            wraithBackpack.EquipWeaponFromSlot(0, nazgul);
            dragonBackpack.EquipWeaponFromSlot(0, sulyvahn);

            glaurung.LifeMultiplier(glaurung);
            nazgul.LifeMultiplier(nazgul);
    
            frodo.TakeAction(glaurung);
            legolas.TakeAction(glaurung);
            legolas.TakeAction(lamenter);
            
            Console.WriteLine($"\nLich's current damage before the Goblin dies: {sulyvahn.CurrentDamage}");
            
            legolas.TakeAction(goblin1); 
            
            Console.WriteLine($"Lich's current damage after the Goblin's death: {sulyvahn.CurrentDamage}\n");

            if (nazgul.DefendAgainstAttacker(frodo))
            {
                frodo.TakeAction(nazgul);
            }  

            Console.WriteLine("\n---Enemy's Turn---");

            gaze.Dragongaze(galadriel);
            lamenter.TakeAction(legolas);
            nazgul.TakeAction(frodo);
            sulyvahn.TakeAction(galadriel);

        }
    }
}
