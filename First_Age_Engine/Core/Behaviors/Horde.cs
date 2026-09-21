using System;
using First_Age_Engine.Core;
using System.Collections.Generic;
using System.Text;

namespace First_Age_Engine.Behaviors
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
            if (count >= 5) // Bando Gigante: Sincronia Espectral e Fúria
            {
                enemy._Armor = 40f;         // Paredão de defesa com os escudos colados
                enemy._Strength = 40;       // Dano físico pesado
                enemy._Dexterity = 35;      // Agilidade em grupo pra não errar ataques
            
                // A pimenta do código: Em bando gigante, a horda canaliza o Reino Espectral!
                // Isso faz eles ultrapassarem o limiar de 50 e enxergarem/interagirem com o Espectral!
                enemy._SpectralInsight = 60; 
                enemy._Knowledge = 30;       // Tática de cerco coordenada
            }
            else if (count >= 2) // Grupo Pequeno: Confiança Moderada
            {
                enemy._Armor = 20f;
                enemy._Strength = 20;
                enemy._Dexterity = 15;
                enemy._SpectralInsight = 20; // Perdem a visão do reino espectral
                enemy._Knowledge = 10;
            }
            else // O Último Sobrevivente: O Tremer de Pernas!
            {
                // O coitado fica apavorado:
                enemy._Armor = 5f;           // Armadura ridícula de tanto tremer
                enemy._Strength = 5;          // Ataques fracos e sem convicção
                enemy._Dexterity = 50;        // A DEXTERITY SOBE! Ele não ataca, mas esquiva em pânico!
                enemy._Knowledge = 0;
                enemy._SpectralInsight = 0;   // Cego pro espectral

                // A SACANAGEM FINAL:
                // O último sobrevivente desesperado ganha a resiliência/imortalidade temporária do seu ImmortalityBehavior!
                // Ele recusa a morrer de primeira e ganha aqueles 5s de invulnerabilidade pra pregar uma peça no jogador!
                if (enemy.ImmortalityBehavior != null && !enemy.ImmortalityBehavior.IsInvulnerable)
                {
                    // Garante que o último cara ative o modo 'resistência de desespero'
                    enemy.ImmortalityBehavior.CheckAndTrigger(seconds: 3);
                }
            }
        }
    }

    }
}