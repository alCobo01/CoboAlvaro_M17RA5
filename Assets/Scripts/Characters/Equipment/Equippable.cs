using UnityEngine;

public class Equippable : MonoBehaviour, IEquippable
{
    public virtual void Equip(Transform socket)
    {
        transform.SetParent(socket);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    
    public virtual void Unequip() => Destroy(gameObject);
}