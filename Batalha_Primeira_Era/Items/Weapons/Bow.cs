using Batalha_Primeira_Era.Items.Weapons;

public class Bow : Weapon
{
    // Novas propriedades específicas de arcos para controlar o escalonamento
    public float DexScaling { get; private set; }
    public float StrScaling { get; private set; }

    // Adicionamos reDexterity, reKnowledge, etc., e também os novos scalings no construtor
    public Bow(string name, float baseDamage, int reStrength, int reDexterity, int reKnowledge, float dexScaling, float strScaling) :
        base(name, baseDamage, reStrength, reDexterity, reKnowledge)
    {
        DexScaling = dexScaling;
        StrScaling = strScaling;
    }

    public override float CalculateDamage(Batalha_Primeira_Era.Core.Character wielder)
    {
        Use();

        // Agora a fórmula usa os atributos do personagem MULTIPLICADOS pelo escalonamento único DESSE arco
        float dexBonus = wielder.Dexterity * DexScaling;
        float strBonus = wielder.Strength * StrScaling;

        return CurrentDamage + dexBonus + strBonus;
    }
}