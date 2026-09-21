using System;
using First_Age_Engine.Core;
using System.Threading.Tasks;

namespace First_Age_Engine.Behaviors
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
            if (_character._lifePoint <= (_maxLife * 0.01f) && !_alreadyTriggeredInvulnerability && _character._lifePoint > 0)
            {
                TriggerInvulnerability(seconds);
            }
        }

        private async Task TriggerInvulnerability(int durationSeconds, CancellationToken cancellationToken = default)
        {
            _isInvulnerable = true;
            _alreadyTriggeredInvulnerability = true;

            // Garante que o HP mínimo seja ao menos 1% da vida máxima (sem morrer)
            float minLifeThreshold = _maxLife * 0.01f;
            if (_character._lifePoint < minLifeThreshold)
            {
                float roundedLife = MathF.Round(minLifeThreshold);
                _character.SetLifePoint(roundedLife);
            }

            Console.WriteLine($"{_character._Name} ativou determinação dos Lamenters! Invulnerável por {durationSeconds} segundos.");

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(durationSeconds), cancellationToken);
            }
            catch (TaskCanceledException)
            {
                // Trata o cancelamento graciosamente se a cena/entidade for destruída antes do tempo
                return;
            }
            finally
            {
                _isInvulnerable = false;
                Console.WriteLine($"{_character._Name} não está mais invulnerável!");
            }
        }
    }
}