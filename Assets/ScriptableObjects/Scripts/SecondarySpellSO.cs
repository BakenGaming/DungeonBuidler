using UnityEngine;

[CreateAssetMenu(menuName = "Secondary Spell")]
public class SecondarySpellSO : ScriptableObject
{
    public float speed;
    public float fireRate;
    public float lifetime;
    public int mpCost;
    public int damage;
    public AdditionalEffectSO additionalEffect;
}
