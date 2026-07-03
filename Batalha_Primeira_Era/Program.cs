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
            
            Inventory mochilaGoblin1 = new Inventory(5);
            Inventory mochilaGoblin2 = new Inventory(5);
            Inventory mochilaDoLadino = new Inventory(5);
            Inventory mochilaDoArqueiro = new Inventory(5);
            Inventory mochilaDoMago = new Inventory(5);
            Inventory mochilaDoDragao = new Inventory(5);
            Inventory mochilaDoEspectro = new Inventory(5);
            Inventory mochilaDoLich = new Inventory(5);

            Bow arcomagico = new Bow("ArcoMagico", 50, 3, 20, 40, 2f, 7f, 10f);
            Bow elvenBow = new Bow("Arco do Vento Élfico", 10f, 5, 5, 25, 10, 3.0f, 0.0f);
            Dagger sting = new Dagger("Sting", 97f, 10, 10, 10, 2.5f, 0.2f, 0.0f);
            Staff staffinitial = new Staff("Staff", 80f, 10, 9, 30, 0.0f, 0.0f, 3.5f);
            Great_Sword morgul = new Great_Sword("Morgul", 50f, 30, 25, 56, 0.2f, 3.0f, 1.5f);
            Grazing grazingDragon = new Grazing("Grazing", 80f, 10, 9, 30, 1.5f, 1.5f, 0.0f);
            DragonGaze gaze = new DragonGaze("Dragon Gaze", 120f, 10, 15, 40, 1.0f, 0.0f, 4.0f);

            Rogue frodo = new Rogue("Frodo", 100f, 60, 20, 40, 50, 15, mochilaDoLadino);
            Archer legolas = new Archer("Legolas", 87f, 60, 20, 40, 50, 15, mochilaDoArqueiro);
            Wizard galadriel = new Wizard("Galadriel", 90f, 70, 70, 14, 17, 57, mochilaDoMago);
            Dragon glaurung = new Dragon("Glaurung", 100f, 78, 80, 50, 30, 40, mochilaDoDragao);
            Spectrum nazgul = new Spectrum("Agnmar", 100f, 78, 80, 45, 78, 67, mochilaDoEspectro);
            Goblin goblin1 = new Goblin("Goblin Slasher", 40f, 10, 0, 0, 20, 5, mochilaGoblin1, orcHorde);
            Goblin goblin2 = new Goblin("Goblin Archer", 40f, 10, 0, 0, 20, 5, mochilaGoblin2, orcHorde);
            Lich sulyvahn = new Lich("Pontiff Sulyvahn", 120f, 80, 30, 25, 20, 70, mochilaDoLich, orcHorde);

            orcHorde.AddMember(goblin1);
            orcHorde.AddMember(goblin2);
            
            mochilaGoblin1.Additem(sting);
            mochilaGoblin2.Additem(sting);
            mochilaDoLadino.Additem(sting);
            mochilaDoArqueiro.Additem(elvenBow);
            mochilaDoArqueiro.Additem(arcomagico);
            mochilaDoMago.Additem(staffinitial);
            mochilaDoDragao.Additem(gaze);
            mochilaDoDragao.Additem(grazingDragon);
            mochilaDoEspectro.Additem(morgul);
            mochilaDoLich.Additem(morgul);

            mochilaDoDragao.EquipWeaponFromSlot(1, glaurung); 
            mochilaDoLadino.EquipWeaponFromSlot(0, frodo);    
            mochilaDoArqueiro.EquipWeaponFromSlot(0, legolas); 
            mochilaDoEspectro.EquipWeaponFromSlot(0, nazgul);
            mochilaDoLich.EquipWeaponFromSlot(0, sulyvahn);

            glaurung.LifeMultiplier(glaurung);
            nazgul.LifeMultiplier(nazgul);
    
            frodo.TakeAction(glaurung);
            legolas.TakeAction(glaurung);
            
            Console.WriteLine($"\nDano atual do Lich antes do Goblin morrer: {sulyvahn.CurrentDamage}");
            
            legolas.TakeAction(goblin1); 
            
            Console.WriteLine($"Dano atual do Lich após a morte do Goblin: {sulyvahn.CurrentDamage}\n");

            if (nazgul.DefendAgainstAttacker(frodo))
            {
                frodo.TakeAction(nazgul);
            }  

            Console.WriteLine("\n---Enemy's Turn---");

            gaze.Dragongaze(galadriel);
            nazgul.TakeAction(frodo);
            sulyvahn.TakeAction(galadriel);

        }
    }
}
