using Unity.Mathematics;
using UnityEngine;

public class StaffHandler : MonoBehaviour
{
    private GameObject equippedStaff;
    private Transform castPoint;
    public void EquipStaff(StaffSO _staff, Transform _parent)
    {
        equippedStaff = Instantiate(_staff.staffObject, _parent.position, Quaternion.identity);
        equippedStaff.transform.SetParent(_parent);
        castPoint = equippedStaff.transform.Find("FirePoint");
    }
    public GameObject GetStaffObject() { return equippedStaff; }
    public Transform GetCastPoint() { return castPoint; }
    
}
