using Batalha_Primeira_Era.Core.Behaviors;
using Batalha_Primeira_Era.Items.Inventory;
using Batalha_Primeira_Era.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace Batalha_Primeira_Era.Core
{
    // ============================================================
    // CONTRATOS (Interfaces): O que o personagem PODE FAZER
    // ============================================================
    public interface IDamageable
    {
        void ReceiveDamage(float damage, Character.BodyPart randomPart); // Tornar tangivel a dano qualquer tipo de alvo
        string Name { get; } // Para poder usar o nome no console
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
        public HeroClass ClassDefinition { get; set; }

        public string Name { get; set; }
        public float lifePoint { get; set; }

        // Garante que os pontos de discernimento tenham o limite de 0 a 99
        private int _spectralInsight = 0;
        public int SpectralInsight
        {
            get => _spectralInsight;
            set => _spectralInsight = Math.Clamp(value, 0, 99); 
        }
        public float Armor { get; set; }

        // Garante que os pontos de forca tenham o limite de 0 a 99
        private int _Strength = 0;
        public int Strength 
        {          
        get => _Strength;
        set => _Strength = Math.Clamp(value, 0, 99); 
        }

        // Garante que os pontos de destreza tenham o limite de 0 a 99
        private int _Dexterity = 0;
        public int Dexterity
        {
            get => _Dexterity;
            set => _Dexterity = Math.Clamp(value, 0, 99); 
        }

        // Garante que os pontos de conhecimento tenham o limite de 0 a 99
        private int _Knowledge = 0;
        public int Knowledge
        {
            get => _Knowledge;
            set => _Knowledge = Math.Clamp(value, 0, 99);
        }

        public Weapon EquippedWeapon { get; set; }
        public Inventory EquippedInventory { get; set; }

        public List<IAbility> Abilities { get; set; } = new List<IAbility>();

        public Character(string name, HeroClass heroClass, float life, int insight, float defense, int strength, int dexterity, int knowledge, Inventory item) 
        {
            Name = name;
            ClassDefinition = heroClass; // <-- Faltava essa linha pra salvar a classe do herói!
            lifePoint = life;
            SpectralInsight = insight;
            Armor = defense;
            Strength = strength;
            Dexterity = dexterity;
            Knowledge = knowledge;
            EquippedInventory = item;
            EquippedWeapon = null;
        }

        // Método para verificar se este ser consegue interagir com o Reino Espectral
        public bool CanPerceiveWraiths()
        {
            return SpectralInsight >= 50;
        }

        public bool EquipWeapon(Weapon weapon)
        {
            if (ClassDefinition != null && !ClassDefinition.AllowedWeapons.Contains(weapon.Type))
            {
                Console.WriteLine($"{Name} ({ClassDefinition.Name}) não pode equipar {weapon.Name}!");
                return false;
            }

            EquippedWeapon = weapon;
            Console.WriteLine($"{Name} equipou {weapon.Name} com sucesso!");
            return true;
        }

        //Uma lista de palavras que valem números, util para aliviar a memória e impede erros
        public enum BodyPart { Head, Torso, Legs, Arms, Wings, Belly}       
            public virtual List<BodyPart> GetTargetTableParts()
            {
                return new List<BodyPart> {BodyPart.Head, BodyPart.Torso, BodyPart.Arms, BodyPart.Legs };
            }

        public void SetLifePoint(float value)
        {
            lifePoint = value; 
        }


        /// <summary>
        /// Um método publico (define uma acao publica), o parâmetro indica que o método espera receber um do tipo alvo.
        /// </summary>
        /// <param Idamageable="target">O alvo que receberá o ataque.</param>
        public void TakeAction(IDamageable target)
        {
            if (this.lifePoint <= 0)
            {
                Console.WriteLine($"{Name} está morto e não pode atacar!");
                return;
            }   

            if (target is Character tChar && tChar.lifePoint <= 0)
            {
                Console.WriteLine($"{tChar.Name} já está morto! {Name} não precisa atacá-lo.");
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


            if (EquippedWeapon != null)
                {
                    if (EquippedWeapon.IsBroken)
                    {
                        Console.WriteLine($"{Name} tried to attack, but the weapon broke! Damage reduced.");
                    }
                    else
                    {
                        //Ele delega a responsabilidade do calculo para o objeto(EquippedWeapon). O uso do "this" passa o personagem atual para a arma.
                        float rawDamage = EquippedWeapon.CalculateDamage(this);

                        //Depois do Feedback do sistema (A interface), ele chama o "ReceiveDamage" do alvo, passando o valor calculado anteriormente.
                        Console.WriteLine($"\n{Name} attacks {target.Name} with {EquippedWeapon.Name}!");
                        Console.WriteLine($"Durability weapon after attack: {EquippedWeapon.Durability}");
                        Console.WriteLine($"Part of the body affected: {randomPart}");
                        target.ReceiveDamage(rawDamage, randomPart);
                    }
                }
                else
                {
                    // Dano base do soco/corpo a corpo bruto 
                    int rawDamage = Strength; 

                    Console.WriteLine($"\n{Name} attacks {target.Name} with bare hands/natural weapons!");
                    Console.WriteLine($"Part of the body affected: {randomPart}");
            
                    // Aplica o dano no alvo do mesmo jeito!
                    target.ReceiveDamage(rawDamage, randomPart);
                }
            }
        }

        /// <summary>
        /// Processa o dano recebido pelo personagem, aplicando reduções baseadas na armadura.
        /// </summary>
        /// <param BodyPart="hitPart">O personagem que receberá o ataque.</param>
        
        public Imortality ImmortalityBehavior { get; set; }
        public Horde MyHorde { get; set; }
        public virtual void ReceiveDamage(float damage, BodyPart hitPart)
        {
            if (lifePoint <= 0) return;

            // 1. Checa se JÁ ESTÁ invulnerável
            if (ImmortalityBehavior != null && ImmortalityBehavior.IsInvulnerable)
            {
                Console.WriteLine($"{Name} está INVULNERÁVEL e não recebeu dano!");
                return;
            }

            float multiplier = GetDamageMultiplier(hitPart);
            float rawDamage = damage * multiplier;
            float armorConstant = 100f;
            float damageFactor = armorConstant / (armorConstant + (this.Armor / 2));
            float damageAfterDefense = rawDamage * damageFactor;

            if (damageAfterDefense < 0) damageAfterDefense = 0;

            Console.WriteLine($"{Name}'s initial lifespan was {lifePoint}");

            // 2. Calcula qual seria a vida pós-dano
            float expectedLife = lifePoint - damageAfterDefense;

            // 3. SE O DANO FOR FATAL (ou deixar abaixo de 1%), aciona o Imortality ANTES de zerar a vida!
            if (ImmortalityBehavior != null && expectedLife <= 1f)
            {
                // Reduz a vida para o limiar de 1% em vez de matar
                lifePoint = 1f; 
                Console.WriteLine($"{Name} tomou um golpe fatal, mas sua resiliência o manteve em {lifePoint} HP!");
        
                // Ativa a imunidade por 5 segundos
                ImmortalityBehavior.CheckAndTrigger(seconds: 5);
                return;
            }

            // Se não for o caso da imortalidade, reduz a vida normalmente
            lifePoint = expectedLife;
            if (lifePoint < 0) lifePoint = 0;

            Console.WriteLine($"{Name} took {damageAfterDefense:F1} damage.");
            Console.WriteLine($"{Name}'s final lifespan is {lifePoint}");

            // 4. Se morreu e pertencia a uma horda, remove da horda!
            if (lifePoint <= 0 && MyHorde != null)
            {
                Console.WriteLine($"{Name} foi derrotado!");
                MyHorde.RemoveMember(this); // Notifica o AbsorbSoul automaticamente![cite: 5, 6]
                MyHorde = null;
            }
        }

        //uso de private é para garantir:
        //O multiplicador seja uma regra interna inviolavel
        //A classe Character é a única que precisa saber como converter a parte do corpo em um multiplicador de dano
        //Facil manutencao futura ou alteracao de valores de multiplicacao
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
