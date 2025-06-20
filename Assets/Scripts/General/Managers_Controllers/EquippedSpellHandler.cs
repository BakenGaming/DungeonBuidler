using UnityEngine;

public class EquippedSpellHandler : MonoBehaviour
{
    [SerializeField] private GameObject startingPrimarySpell;
    private GameObject currentPrimarySpell;

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
        currentPrimarySpell = startingPrimarySpell;
    }

    private void EquipNewPrimarySpell(GameObject newSpell)
    {
        currentPrimarySpell = newSpell;
    }

    public GameObject GetCurrentPrimarySpell(){return currentPrimarySpell;}
}
