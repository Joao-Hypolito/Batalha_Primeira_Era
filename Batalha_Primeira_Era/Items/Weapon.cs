using System;
using System.Collections.Generic;
using System.Text;
using Batalha_Primeira_Era.Core;
using static Batalha_Primeira_Era.Core.Character;

namespace Batalha_Primeira_Era.Items.Weapons
{
    public class Weapon
    {
        public string Name { get; set; }
        public WeaponType Type { get; set; }
        public float BaseDamage { get; set; }

        //Requisitos
        public int RequiredStrength { get; set; }
        public int RequiredDexterity { get; set; }
        public int RequiredKnowledge { get; set; }

        // Escalonamento de atributos (Ex: 0.8f = 80% do atributo vira dano extra)
        public float StrengthScaling { get; set; }
        public float DexterityScaling { get; set; }
        public float KnowledgeScaling { get; set; }

        //Durabilidade
        private float _durability = 100f; 
        public float Durability
        {
            get { return _durability; }
            set
            {
                
                if (value > 100) _durability = 100;
                else if (value < 0) _durability = 0;
                else _durability = value;
            }
        }
        public bool IsBroken => Durability <= 0;


        public Weapon(string name, WeaponType type, float baseDamage, int reqStr, int reqDex, int reqKnw, float strScale = 0f, float dexScale = 0f, float knwScale = 0f)
        {
            Name = name;
            Type = type;
            BaseDamage = baseDamage;
            RequiredStrength = reqStr;
            RequiredDexterity = reqDex;
            RequiredKnowledge = reqKnw;
            StrengthScaling = strScale;
            DexterityScaling = dexScale;
            KnowledgeScaling = knwScale;
        }
        public float CalculateDamage(Core.Character wielder)
        {
            if (IsBroken) return BaseDamage * 0.2f; // Dano mínimo se quebrada

            // Verifica se o personagem atende aos requisitos
            if (wielder.Strength < RequiredStrength || 
                wielder.Dexterity < RequiredDexterity || 
                wielder.Knowledge < RequiredKnowledge)
            {
                Console.WriteLine($"{wielder.Name} não tem os atributos necessários para usar {Name} perfeitamente!");
                return BaseDamage * 0.5f; // Penalidade por não ter os requisitos
            }

            // Usa sua excelente lógica de rendimento de atributos (Hard Cap)
            float bonusStr = CalculateAttributeBonus(wielder.Strength, StrengthScaling);
            float bonusDex = CalculateAttributeBonus(wielder.Dexterity, DexterityScaling);
            float bonusKnw = CalculateAttributeBonus(wielder.Knowledge, KnowledgeScaling);

            // Reduz durabilidade ao usar
            Use();

            return CurrentDamage + bonusStr + bonusDex + bonusKnw;
        }

        /// <summary>
        /// Reduz a durabilidade da arma a cada uso. Se chegar a valores negativos, trava em zero.
        /// </summary>
        public void Use()
        {
            if (Durability > 0)
            {
                Durability -= 0.25f;
                if (Durability < 0) Durability = 0;
            }
        }
        /// <summary>
        /// Calcula o dano atual da arma com base no seu estado de conservação. 
        /// Retorna 0 se quebrada ou 70% do dano se a durabilidade estiver baixa (abaixo de 30).
        /// </summary>
        public float CurrentDamage
        {
            get
            {
                if (Durability == 0) return 0;
                if (Durability < 30) return BaseDamage * 0.7f; // Dano reduzido por cansaço
                return BaseDamage;
            }
        }

        public float CalculateAttributeBonus(int attributeValue, float scaling)
        {
            float effectiveValue = 0f;

            if (attributeValue <= 30)
            {
                // 100% de rendimento até o 30º ponto
                effectiveValue = attributeValue;
            }
            else if (attributeValue <= 60)
            {
                // Rendimento cai para 50% entre o ponto 31 e 60
                effectiveValue = 30 + ((attributeValue - 30) * 0.5f);
            }
            else
            {
                // Rendimento cai para 15% acima do ponto 60 (Hard Cap)
                effectiveValue = 30 + (30 * 0.5f) + ((attributeValue - 60) * 0.15f);
            }

            return effectiveValue * scaling;
        }

    }
}

