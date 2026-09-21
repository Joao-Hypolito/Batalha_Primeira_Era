using System;
using System.Collections.Generic;
using System.Text;

namespace First_Age_Engine.Core
{
    public enum WeaponType { Sword, GreatSword, GreatAxe, Dagger, Bow, Staff, Shield }

    public class CharacterClass
    {
        public string Name { get; set; }
        public List<WeaponType> AllowedWeapons { get; set; } = new List<WeaponType>();

        public CharacterClass(string name, List<WeaponType> allowedWeapons)
        {
            Name = name;
            AllowedWeapons = allowedWeapons;
        }
    }
}
