using Batalha_Primeira_Era.Core;
using Batalha_Primeira_Era.Core.Bosses;
using Batalha_Primeira_Era.Core.Enimies;
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


            Dagger sting = new Dagger("Sting", 97f, 10, 10, 10);
            Staff staffinitial = new Staff("Staff", 80f, 10, 9, 30);
            Great_Sword morgul = new Great_Sword("Morgul", 50f, 30, 25, 56);
            Grazing GrazingDragon = new Grazing("Grazing", 80f, 10, 9, 30);
            DragonGaze Gaze = new DragonGaze("Dragon Gaze", 0f, 0, 0, 0);
            Bow Bow = new Bow("Bow", 67f, 20, 40, 20);

            Rogue frodo = new Rogue("Frodo", 100f, 60, 20, 40, 50, 15, mochilaDoLadino);
            Archer legolas = new Archer("Legolas", 87f, 60, 20, 40, 50, 15, mochilaDoArqueiro);
            Wizard galadriel = new Wizard("Galadriel", 90f, 70, 70, 14, 17, 57, mochilaDoMago);
            Dragon glaurung = new Dragon("Glaurung", 100f, 78, 80, 50, 30, 40, mochilaDoDragao);
            Spectrum nazgul = new Spectrum("Agnmar", 100f, 78, 80, 45, 78, 67, mochilaDoEspectro);
            Goblin goblin1 = new Goblin("Goblin Slasher", 40f, 10, 0, 0, 20, 5, mochilaGoblin1, orcHorde);
            Goblin goblin2 = new Goblin("Goblin Archer", 40f, 10, 0, 0, 20, 5, mochilaGoblin2, orcHorde);

            orcHorde.AddMember(goblin1);
            orcHorde.AddMember(goblin2);
            
            mochilaGoblin1.Additem(sting);
            mochilaGoblin2.Additem(sting);
            mochilaDoLadino.Additem(sting);
            mochilaDoArqueiro.Additem(Bow);
            mochilaDoMago.Additem(staffinitial);
            mochilaDoDragao.Additem(Gaze);
            mochilaDoDragao.Additem(GrazingDragon);
            mochilaDoEspectro.Additem(morgul);

            mochilaDoDragao.EquipWeaponFromSlot(1, glaurung); 
            mochilaDoLadino.EquipWeaponFromSlot(0, frodo);    
            mochilaDoArqueiro.EquipWeaponFromSlot(0, legolas); 
            mochilaDoEspectro.EquipWeaponFromSlot(0, nazgul);

            glaurung.LifeMultiplier(glaurung);
            nazgul.LifeMultiplier(nazgul);
    
            frodo.TakeAction(glaurung);
            legolas.TakeAction(glaurung);
            legolas.TakeAction(goblin1);

            if (nazgul.DefendAgainstAttacker(frodo))
            {
                frodo.TakeAction(nazgul);
            }  

            Console.WriteLine("\n---Enemy's Turn---");

            Gaze.Dragongaze(galadriel);
            nazgul.TakeAction(frodo);

        }
    }
}
