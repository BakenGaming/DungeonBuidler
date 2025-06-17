using UnityEngine;

[CreateAssetMenu(menuName ="Projectile")]
public class ProjectileSO : ScriptableObject
{
    public float speed;
    public float fireRate;
    public float lifetime;
    public int damage;
}
