using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core
{
    public enum WeaponType { Sword, GreatSword, GreatAxe, Dagger, Bow, Staff, Shield }

    public class HeroClass
    {
        public string Name { get; set; }
        public List<WeaponType> AllowedWeapons { get; set; } = new List<WeaponType>();

        public HeroClass(string name, List<WeaponType> allowedWeapons)
        {
            Name = name;
            AllowedWeapons = allowedWeapons;
        }
    }
}
