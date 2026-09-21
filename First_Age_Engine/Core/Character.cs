using First_Age_Engine.Behaviors;
using First_Age_Engine.Core;
using First_Age_Engine.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace First_Age_Engine.Core
{
    // ============================================================
    // CONTRATOS (Interfaces): O que o personagem PODE FAZER
    // ============================================================
    public interface IDamageable
    {
        void ReceiveDamage(float damage, Character.BodyPart randomPart); // Tornar tangivel a dano qualquer tipo de alvo
        string _Name { get; } // Para poder usar o nome no console
    }

    // Interface de discernimento para chefes
    public interface IDiscernment
    {
        void Wraiths();
    }

    public interface IAbility
    {
        string Name { get; }
        void Execute(Character caster, IDamageable target);
    }

    public class Character : IDamageable
    {
        public CharacterClass _class;

        public string _Name { get; set; }
        public float _lifePoint { get; set; }

        // Garante que os pontos de discernimento tenham o limite de 0 a 99
        private int SpectralInsight = 0;
        public int _SpectralInsight
        {
            get => SpectralInsight;
            set => SpectralInsight = Math.Clamp(value, 0, 99); 
        }
        public float _Armor { get; set; }

        // Garante que os pontos de forca tenham o limite de 0 a 99
        private int Strength = 0;
        public int _Strength 
        {          
            get => Strength;
            set => Strength = Math.Clamp(value, 0, 99); 
        }

        // Garante que os pontos de destreza tenham o limite de 0 a 99
        private int Dexterity = 0;
        public int _Dexterity
        {
            get => Dexterity;
            set => Dexterity = Math.Clamp(value, 0, 99); 
        }

        // Garante que os pontos de conhecimento tenham o limite de 0 a 99
        private int Knowledge = 0;
        public int _Knowledge
        {
            get => Knowledge;
            set => Knowledge = Math.Clamp(value, 0, 99);
        }

        public Weapon _EquippedWeapon { get; set; }
        public Inventory _EquippedInventory { get; set; }

        public List<IAbility> _Abilities { get; set; } = new List<IAbility>();

        public Character(string name, CharacterClass classDefinition) 
        {
            _Name = name;
            _class = classDefinition;
        }

        public Character WithLife(float life)
        {
            _lifePoint = life;
            return this;
        }

        public Character WithSpectral(int spectralInsight)
        {
            _SpectralInsight = spectralInsight;
            return this;    
        }

        public Character WithArmor(float armor)
        {
            _Armor = armor;
            return this;
        }

        public Character WithStrenght(int strength)
        {
            _Strength = strength;
            return this;
        }

        public Character WithDextery(int dexterity)
        {
            _Dexterity = dexterity;
            return this;
        }

        public Character WithKnowledge(int knowledge)
        {
            _Knowledge = knowledge;
            return this;
        }

        public Character WithEquippedWeapon(Weapon equippedWeapon)
        {
            _EquippedWeapon = equippedWeapon;
            return this;
        }

        public Character WithEquippedInventory(Inventory equippedInventory)
        {
            _EquippedInventory = equippedInventory;
            return this;
        }

        public Character WithAbilities(params IAbility[] abilities)
        {
            _Abilities.AddRange(abilities);
            return this;
        }

        // Método para verificar se este ser consegue interagir com o Reino Espectral
        public bool CanPerceiveWraiths()
        {
            return _SpectralInsight >= 50;
        }

        public bool EquipWeapon(Weapon weapon)
        {
            if (_class != null && !_class.AllowedWeapons.Contains(weapon.Type))
            {
                Console.WriteLine($"{_Name} ({_class.Name}) não pode equipar {weapon.Name}!");
                return false;
            }

            _EquippedWeapon = weapon;
            Console.WriteLine($"{_Name} equipou {weapon.Name} com sucesso!");
            return true;
        }

        //Uma lista de palavras que valem números, util para aliviar a memória e impede erros
        public enum BodyPart { Head, Torso, Legs, Arms, Wings, Belly }       
        
        public virtual List<BodyPart> GetTargetTableParts()
        {
            return new List<BodyPart> { BodyPart.Head, BodyPart.Torso, BodyPart.Arms, BodyPart.Legs };
        }

        public void SetLifePoint(float value)
        {
            _lifePoint = value; 
        }

        /// <summary>
        /// Um método publico (define uma acao publica), o parâmetro indica que o método espera receber um do tipo alvo.
        /// </summary>
        /// <param name="target">O alvo que receberá o ataque.</param>
        public void TakeAction(IDamageable target)
        {
            if (this._lifePoint <= 0)
            {
                Console.WriteLine($"{_Name} está morto e não pode atacar!");
                return;
            }   

            if (target is Character tChar && tChar._lifePoint <= 0)
            {
                Console.WriteLine($"{tChar._Name} já está morto! {_Name} não precisa atacá-lo.");
                return;
            }

            Random rng = new Random();

            if (target is Character targetCharacter)
            {
                // 2. Chamamos o método que criamos! Ele já vem com as partes certas (com ou sem asas)
                List<BodyPart> availableParts = targetCharacter.GetTargetTableParts();

                // 3. Sorteamos um índice baseado no tamanho da lista que recebemos
                int index = rng.Next(availableParts.Count);
                BodyPart randomPart = availableParts[index];

                if (_EquippedWeapon != null)
                {
                    if (_EquippedWeapon.IsBroken)
                    {
                        Console.WriteLine($"{_Name} tried to attack, but the weapon broke! Damage reduced.");
                    }
                    else
                    {
                        // Ele delega a responsabilidade do calculo para o objeto (_EquippedWeapon). O uso do "this" passa o personagem atual para a arma.
                        float rawDamage = _EquippedWeapon.CalculateDamage(this);

                        // Depois do Feedback do sistema (A interface), ele chama o "ReceiveDamage" do alvo, passando o valor calculated anteriormente.
                        Console.WriteLine($"\n{_Name} attacks {target._Name} with {_EquippedWeapon.Name}!");
                        Console.WriteLine($"Durability weapon after attack: {_EquippedWeapon.Durability}");
                        Console.WriteLine($"Part of the body affected: {randomPart}");
                        target.ReceiveDamage(rawDamage, randomPart);
                    }
                }
                else
                {
                    // Dano base do soco/corpo a corpo bruto 
                    int rawDamage = _Strength; 

                    Console.WriteLine($"\n{_Name} attacks {target._Name} with bare hands/natural weapons!");
                    Console.WriteLine($"Part of the body affected: {randomPart}");
            
                    // Aplica o dano no alvo do mesmo jeito!
                    target.ReceiveDamage(rawDamage, randomPart);
                }
            }
        }

        /// <summary>
        /// Processa o dano recebido pelo personagem, aplicando reduções baseadas na armadura.
        /// </summary>
        /// <param name="hitPart">A parte do corpo atingida pelo ataque.</param>
        
        public Imortality ImmortalityBehavior { get; set; }
        public Horde MyHorde { get; set; }

        public virtual void ReceiveDamage(float damage, BodyPart hitPart)
        {
            if (_lifePoint <= 0) return;

            // 1. Checa se JÁ ESTÁ invulnerável
            if (ImmortalityBehavior != null && ImmortalityBehavior.IsInvulnerable)
            {
                Console.WriteLine($"{_Name} está INVULNERÁVEL e não recebeu dano!");
                return;
            }

            float multiplier = GetDamageMultiplier(hitPart);
            float rawDamage = damage * multiplier;
            float armorConstant = 100f;
            float damageFactor = armorConstant / (armorConstant + (this._Armor / 2));
            float damageAfterDefense = rawDamage * damageFactor;

            if (damageAfterDefense < 0) damageAfterDefense = 0;

            Console.WriteLine($"{_Name}'s initial lifespan was {_lifePoint}");

            // 2. Calcula qual seria a vida pós-dano
            float expectedLife = _lifePoint - damageAfterDefense;

            // 3. SE O DANO FOR FATAL (ou deixar abaixo de 1%), aciona o Immortality ANTES de zerar a vida!
            if (ImmortalityBehavior != null && expectedLife <= 1f)
            {
                // Reduz a vida para o limiar de 1% em vez de matar
                _lifePoint = 1f; 
                Console.WriteLine($"{_Name} tomou um golpe fatal, mas sua resiliência o manteve em {_lifePoint} HP!");
        
                // Ativa a imunidade por 5 segundos
                ImmortalityBehavior.CheckAndTrigger(seconds: 5);
                return;
            }

            // Se não for o caso da imortalidade, reduz a vida normalmente
            _lifePoint = expectedLife;
            if (_lifePoint < 0) _lifePoint = 0;

            Console.WriteLine($"{_Name} took {damageAfterDefense:F1} damage.");
            Console.WriteLine($"{_Name}'s final lifespan is {_lifePoint}");

            // 4. Se morreu e pertencia a uma horda, remove da horda!
            if (_lifePoint <= 0 && MyHorde != null)
            {
                Console.WriteLine($"{_Name} foi derrotado!");
                MyHorde.RemoveMember(this); // Notifica o AbsorbSoul automaticamente!
                MyHorde = null;
            }
        }

        // uso de private é para garantir:
        // O multiplicador seja uma regra interna inviolavel
        // A classe Character é a única que precisa saber como converter a parte do corpo em um multiplicador de dano
        // Facil manutencao futura ou alteracao de valores de multiplicacao
        private float GetDamageMultiplier(BodyPart part)
        {
            return part switch
            {
                BodyPart.Belly => 3.0f,
                BodyPart.Head => 2.0f,
                BodyPart.Torso => 1.0f,
                BodyPart.Wings => 1.5f,
                BodyPart.Arms => 0.8f,
                BodyPart.Legs => 0.8f,
                _ => 1.0f
            };
        }
    }
}