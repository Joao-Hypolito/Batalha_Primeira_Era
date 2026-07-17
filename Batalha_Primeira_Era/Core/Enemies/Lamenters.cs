using Batalha_Primeira_Era.Core.Behaviors;
using Batalha_Primeira_Era.Items.Inventory;
using Batalha_Primeira_Era.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core.Enemies
{
    public class Lamenters : Character
    {
        private Imortality _imortality;

        public Lamenters(string name, float life, int insight, float defense, int strength, int dexterity, int knowledge, Inventory item) 
        : base(name, life, insight, defense, strength, dexterity, knowledge, item)
        {
            // Criamos a instância real passando 'this' (os Lamenters) e a vida inicial/máxima
            _imortality = new Imortality(this, life);
        }

        public override void ReceiveDamage(float damage, BodyPart hitPart)
        {
            // Se o comportamento disser que está invulnerável, ignora o dano
            if (_imortality.IsInvulnerable)
            {
                Console.WriteLine($"{this.Name} está IMUNE a danos agora!");
                return;
            }

            // Se não estiver imune, toma o dano normal da base
            base.ReceiveDamage(damage, hitPart);

            // Pergunta para o comportamento de imortalidade se é hora de ativar o 1%
            _imortality.CheckAndTrigger(5); // 5 segundos
        }  
    }
}