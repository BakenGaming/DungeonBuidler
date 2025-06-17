using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler_TopDown : MonoBehaviour, IAttackHandler
{
    #region Variables
    private Transform firePoint;
    private float shotTimer = 0f;

    #endregion

    #region Initialize
    public void Initialize()
    {
        PlayerInputController_TopDown.OnPlayerAttack += SetupAttack;
    }

    private void OnDisable()
    {
        PlayerInputController_TopDown.OnPlayerAttack -= SetupAttack;
    }

    private void SetupAttack(Vector3 _target)
    {
        if (shotTimer <= 0)
        {
            FireWeapon(_target);
        }
    }

    #endregion
    #region Functions
    private void Start()
    {
        firePoint = transform.Find("FirePoint").gameObject.transform;
    }
    private void Update()
    {
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        shotTimer -= Time.deltaTime;
    }

    private void FireWeapon(Vector3 _target)
    { 
        GameObject newBullet = Instantiate(GameManager.i.GetProjectileHandler().GetCurrentProjectile(), firePoint.position, Quaternion.identity);
            
        Vector3 shootDir = (_target - firePoint.position).normalized;
        
        newBullet.GetComponent<Bullet>().InitializeBullet(shootDir);

        shotTimer = newBullet.GetComponent<ProjectileSOHolder>().projectileSO.fireRate;
            
        Destroy(newBullet, newBullet.GetComponent<ProjectileSOHolder>().projectileSO.lifetime);
    }
    #endregion
}
