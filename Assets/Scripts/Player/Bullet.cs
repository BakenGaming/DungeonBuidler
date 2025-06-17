using UnityEngine;
using CodeMonkey.Utils;

public class Bullet : MonoBehaviour
{
 private Vector3 shootDir;
    private ProjectileSO projectileSO;

    private void Start()
    {
        projectileSO = GetComponent<ProjectileSOHolder>().projectileSO;
    }
    public void InitializeBullet(Vector3 t)
    {
        shootDir = t;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
    }

    void Update()
    {
        transform.position += shootDir * projectileSO.speed * Time.deltaTime;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        
        if(damageable != null) 
        {
            damageable.TakeDamage(projectileSO.damage);
            DestroyBullet();
        }
    }
}
