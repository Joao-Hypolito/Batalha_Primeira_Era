using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core.Behaviors
{
    public class Horde
    {
        public string HordeName { get; private set; }
        public List<Character> _members {get; private set;} = new List<Character>();
        public int InitialCount { get; private set; }
        public event Action OnMemberDied;

        public Horde(string name)
        {
            HordeName = name;
        }

        public void AddMember(Character enemy)
        {
        _members.Add(enemy);
        InitialCount = _members.Count;
        UpdateHordeBuffs();
        }

        public void RemoveMember(Character enemy) 
        {
        _members.Remove(enemy);
        OnMemberDied?.Invoke();
        UpdateHordeBuffs();
        }
        
            public void UpdateHordeBuffs()
            {
                int count = _members.Count;

                foreach (var enemy in _members)
                {
                    if (count >= 5) // Giant Pack: Total courage!
                    {
                        enemy.Armor = 40f;    // More armor because they are tightly packed
                        enemy.Strength = 30;  // Massive damage bonus
                    }
                    else if (count >= 2) // Small group: Gaining confidence
                    {
                        enemy.Armor = 20f;
                        enemy.Strength = 20;
                    }
                    else // Only 1 left: The coward alone!
                    {
                        enemy.Armor = 5f;    // Ridiculous defense due to fear
                        enemy.Strength = 5;   // Weak attack because they want to flee
                    }
                }
            }

    }
}