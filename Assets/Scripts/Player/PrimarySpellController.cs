using CodeMonkey.Utils;
using UnityEngine;


public class PrimarySpellController : MonoBehaviour
{
    private Vector3 shootDir;
    private Rigidbody2D rb;
    private PrimarySpellSO primarySpellSO;

    private void Start()
    {
        primarySpellSO = GetComponent<PrimarySpellSOHolder>().primarySpellSO;
    }
    public void InitializeSpell(Vector3 t)
    {
        rb = GetComponent<Rigidbody2D>();
        shootDir = t;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
    }

    void Update()
    {
        rb.linearVelocity = transform.right * primarySpellSO.speed;
        //transform.position += shootDir * primarySpellSO.speed * Time.deltaTime;
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        
        if(damageable != null) 
        {
            damageable.TakeDamage(primarySpellSO.damage);
            DestroyProjectile();
        }
    }
}
