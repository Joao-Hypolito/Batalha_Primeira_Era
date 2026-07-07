using Batalha_Primeira_Era.Core;
using Batalha_Primeira_Era.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Items.Inventory
{
    public class Inventory
    {
        private List<Weapon> _slots;
        public int MaxCapacity { get; set; }

        public Inventory(int maxCapacity)
        {
            MaxCapacity = maxCapacity;
            _slots = new List<Weapon>();
        }

        public void AddItem(Weapon item)
        {
            if(_slots.Count >= MaxCapacity) 
            {
                Console.WriteLine("Inventory is full!");
                return;
            }
            _slots.Add(item);
            Console.WriteLine($"{item.Name} added to inventory.");
        }

        public void ShowInventory()
        {
            Console.WriteLine("\n--- Current Inventory ---");
            foreach (var item in _slots) 
            {
                Console.WriteLine($"- {item.Name} | Damage: {item.CurrentDamage}");
            }
            Console.WriteLine("-------------------------\n");
        }

        public void EquipWeaponFromSlot(int slotIndex, Character character)
        {
            
            // Physical Validation: does the slot exist in the bag?
            if (slotIndex < 0 || slotIndex >= _slots.Count)
            {
                Console.WriteLine("Invalid inventory slot!");
                return;
            }

            // Get the weapon chosen from the inventory
            Weapon weaponToEquip = _slots[slotIndex];

            // Directly Change the character's state 
            character.EquippedWeapon = weaponToEquip;
            Console.WriteLine($"\n[EQUIP] {character.Name} successfully equipped: {weaponToEquip.Name}!");
        }
    }
}