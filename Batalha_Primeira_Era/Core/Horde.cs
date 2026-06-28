using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core
{
    public class Horde
    {
        public string HordeName { get; private set; }
        private List<Character> _members = new List<Character>();

        public Horde(string name)
        {
            HordeName = name;
        }

        public void AddMember(Character enemy)
        {
        _members.Add(enemy);
        UpdateHordeBuffs();
        }

        public void RemoveMember(Character enemy) 
        {
        _members.Remove(enemy);
        UpdateHordeBuffs();
        }
        
        public void UpdateHordeBuffs()
        {

        int count = _members.Count;

            foreach (var enemy in _members)
            {
                if (count >= 5) // Bando Gigante: Coragem total!
                {
                    enemy.Armor = 40f;    // Mais armadura por estarem compactados
                    enemy.Strength = 30;  // Bônus massivo de dano
                }
                else if (count >= 2) // Pequeno grupo: Ganhando confiança
                {
                    enemy.Armor = 20f;
                    enemy.Strength = 20;
                }
                else // Sobrou apenas 1: O covarde sozinho!
                {
                    enemy.Armor = 5f;    // Defesa ridícula de medo
                    enemy.Strength = 5;   // Ataque fraco porque quer fugir
                }
            }
        }

    }
}