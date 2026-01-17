using UnityEngine;

public interface IEquippable
{
    void Equip(Transform socket);
    void Unequip();
}