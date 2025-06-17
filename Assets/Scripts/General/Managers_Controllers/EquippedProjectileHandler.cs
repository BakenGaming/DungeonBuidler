using UnityEngine;

public class EquippedProjectileHandler : MonoBehaviour
{
[SerializeField] private GameObject startingProjectile;
    private GameObject currentProjectile;

    private void OnEnable() 
    {
        //ProjectilePickupHandler.OnNewProjectilCollected += EquipNewProjectile;    
    }

    private void Disable() 
    {
        //ProjectilePickupHandler.OnNewProjectilCollected -= EquipNewProjectile;    
    }
    private void Awake() 
    {
        currentProjectile = startingProjectile;
    }

    private void EquipNewProjectile(GameObject newProjectile)
    {
        currentProjectile = newProjectile;
    }

    public GameObject GetCurrentProjectile(){return currentProjectile;}
}
