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

            // O SEGREDO ESTÁ AQUI: O AbsorbSoul se inscreve no evento da Horde!
            // Toda vez que a horda disparar "OnMemberDied", o método "AumentarPoderPelaMorte" será executado.
            _myHorde.OnMemberDied += AumentarPoderPelaMorte;
        }

        // Esse método roda AUTOMATICAMENTE sempre que um Goblin morre
        private void AumentarPoderPelaMorte()
        {
            if (_myHorde == null) return;
            
            // Calcula quantos morreram baseando-se no estado atual da lista
            int deadGoblins = _myHorde.InitialCount - _myHorde._members.Count;

            // O Lich ganha +5 de dano por cada alma que já se foi
            CurrentDamage = BaseDamage + (deadGoblins * 5);

            if (deadGoblins > 0) 
            {
                Console.WriteLine($"[SOUL ABSORB] Um aliado caiu! O Lich absorveu a alma. Total de mortos: {deadGoblins}. Dano atual do Lich: {CurrentDamage}");
            }
        }
    }
}