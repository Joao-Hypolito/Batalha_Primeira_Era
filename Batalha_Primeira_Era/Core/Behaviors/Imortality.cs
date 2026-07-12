using System;
using System.Threading.Tasks;

namespace Batalha_Primeira_Era.Core.Behaviors
{
    public class Imortality
    {
        private bool _isInvulnerable = false;
        private bool _alreadyTriggeredInvulnerability = false;
        private float _maxLife;
        private Character _character; // Guarda a referência do inimigo dono desse comportamento

        // O construtor agora recebe o Character dono e a vida máxima dele
        public Imortality(Character character, float maxLife)
        {
            _character = character;
            _maxLife = maxLife;
        }

        // Propriedade pública para o inimigo saber se está imune ou não antes de tomar dano
        public bool IsInvulnerable => _isInvulnerable;

        // Método público que os Lamenters vão chamar no ReceiveDamage
        public void CheckAndTrigger(int seconds)
        {
            // Checa o 1% usando a vida do Character
            if (_character.lifePoint <= (_maxLife * 0.01f) && !_alreadyTriggeredInvulnerability && _character.lifePoint > 0)
            {
                TriggerInvulnerability(seconds);
            }
        }

        private async void TriggerInvulnerability(int seconds)
        {
            _isInvulnerable = true;
            _alreadyTriggeredInvulnerability = true;
            
            if (_character.lifePoint < (_maxLife * 0.01f))
            {
                _character.lifePoint = (float)Math.Round(_maxLife * 0.01f);
            }

            Console.WriteLine($"{_character.Name} ativou determinação dos Lamenters! Invulnerável por {seconds} segundos.");

            await Task.Delay(seconds * 1000);

            _isInvulnerable = false;
            Console.WriteLine($"{_character.Name} não está mais invulnerável!");
        }
    }
}