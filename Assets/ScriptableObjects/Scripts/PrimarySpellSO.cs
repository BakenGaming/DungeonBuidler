using UnityEngine;

[CreateAssetMenu(menuName = "Primary Spell")]
public class PrimarySpellSO : ScriptableObject
{
    public float speed;
    public float fireRate;
    public float lifetime;
    public int mpCost;
    public int damage;
    public AdditionalEffectSO additionalEffect;
}
