using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core.Behaviors
{
    public class AbsorbSoul
    {
        private Horde _myHorde;
        public int BaseDamage { get; set; } = 10;
        public int CurrentDamage { get; private set; }

        public void SetHorde(Horde horde)
        {
            _myHorde = horde;
            CurrentDamage = BaseDamage;

            // THE SECRET IS HERE: The AbsorbSoul subscribes to the Horde event!
            // Every time that horde triggers "OnMemberDied", the method "IncreasePowerByDeath" will be executed.
            _myHorde.OnMemberDied += IncreasePowerByDeath;
        }

        // This method runs AUTOMATICALLY whenever a Goblin dies
        private void IncreasePowerByDeath()
        {
            if (_myHorde == null) return;
            
            // Calcule how many died based on the current state of list.
            int deadGoblins = _myHorde.InitialCount - _myHorde._members.Count;

            // The Lich gains +5 for damage for each soul that is already gone
            CurrentDamage = BaseDamage + (deadGoblins * 5);

            if (deadGoblins > 0) 
            {
                Console.WriteLine($"[SOUL ABSORB] An ally has fallen! The Lich absorbed the soul. Total dead: {deadGoblins}. Lich's current damage: {CurrentDamage}");
            }
        }
    }
}