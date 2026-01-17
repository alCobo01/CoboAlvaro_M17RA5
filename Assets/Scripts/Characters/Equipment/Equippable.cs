using UnityEngine;

public class WeaponEquippable : MonoBehaviour, IEquippable
{
    public void Equip(Transform socket)
    {
        transform.SetParent(socket);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    
    public void Unequip() => transform.SetParent(null);
}