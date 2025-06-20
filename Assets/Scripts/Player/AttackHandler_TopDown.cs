using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class AttackHandler_TopDown : MonoBehaviour, IAttackHandler
{
    #region Variables
    private GameObject staff;
    private Transform castPoint;
    private float spellTimer = 0f;
    private bool initialized = false;

    #endregion

    #region Initialize
    public void Initialize(GameObject _staff, Transform _castPoint)
    {
        PlayerInputController_TopDown.OnPlayerAttack += SetupAttack;
        staff = _staff;
        castPoint = _castPoint;
        initialized = true;
    }

    private void OnDisable()
    {
        PlayerInputController_TopDown.OnPlayerAttack -= SetupAttack;
    }

    private void SetupAttack(Vector3 _target)
    {
        if (spellTimer <= 0)
        {
            CastSpell(_target);
        }
    }

    #endregion
    #region Functions
    private void Update()
    {
        UpdateTimers();
        if(initialized) HandleStaffRotation();
    }

    private void UpdateTimers()
    {
        spellTimer -= Time.deltaTime;
    }

    private void HandleStaffRotation()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 direction = (worldPosition - staff.transform.position).normalized;

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        staff.transform.eulerAngles = new Vector3(0, 0, angle);
        
    }

    private void CastSpell(Vector3 _target)
    {
        GameObject newSpell = Instantiate(GameManager.i.GetSpellHandler().GetCurrentPrimarySpell(), castPoint.position, Quaternion.identity);

        Vector3 shootDir = (_target - castPoint.position).normalized;

        newSpell.GetComponent<PrimarySpellController>().InitializeSpell(shootDir);

        spellTimer = newSpell.GetComponent<PrimarySpellSOHolder>().primarySpellSO.fireRate;

        Destroy(newSpell, newSpell.GetComponent<PrimarySpellSOHolder>().primarySpellSO.lifetime);
    }
    #endregion
}
