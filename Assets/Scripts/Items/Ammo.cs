using UnityEngine;

public class Ammo : Item
{
    [SerializeField] private AmmoData.AmmoType _ammoType;

    public AmmoData.AmmoType AmmoType => _ammoType;

    private void Awake()
    {
        Init();
    }
}

public class AmmoData
{
    public enum AmmoType
    {
        Pistol,
        Rifle,
        Shotgun,
        RPG
    }
}