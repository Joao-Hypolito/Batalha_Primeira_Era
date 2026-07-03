using Batalha_Primeira_Era.Core.Behaviors;
using Batalha_Primeira_Era.Items.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Primeira_Era.Core.Enemies
{
    // Mudei para public para que o seu Program.cs consiga instanciar sem problemas de herança
    public class Lich : Character
    {
        // O Lich agora tem uma instância da mecânica AbsorbSoul dele
        private AbsorbSoul _absorbSoulBehavior;

        // O construtor recebe os dados do Lich e a Horda que ele vai monitorar
        public Lich(string name, float life, int insight, float defense, int strength, int dexterity, int knowledge, Inventory item, Horde horde) 
            : base(name, life, insight, defense, strength, dexterity, knowledge, item)
        {
            // Inicializa a mecânica e passa a horda para ela começar a monitorar os Goblins
            _absorbSoulBehavior = new AbsorbSoul();
            _absorbSoulBehavior.SetHorde(horde);
        }

        // Criamos uma propriedade para o Lich pegar o dano atualizado de dentro do AbsorbSoul
        // Quando você for dar o comando de ataque do Lich, você usa "lich.CurrentDamage"
        public int CurrentDamage 
        {
            get { return _absorbSoulBehavior.CurrentDamage; }
        }

        public override void ReceiveDamage(float damage, BodyPart hitPart)
        {
            base.ReceiveDamage(damage, hitPart);

            if (this.lifePont <= 0)
            {
                Console.WriteLine($"{Name} has been vanquished!");
            }
        }           
    }
}