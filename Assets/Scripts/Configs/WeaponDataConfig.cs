using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataConfig", menuName = "Weapon Data/Base")]
public class WeaponDataConfig : ScriptableObject
{
    [Header("Base Data")]
    [SerializeField, Range(2, 200)] private int _damage;
    [SerializeField, Range(1, 50)] private int _maxAmmoCapacity;
    [SerializeField, Range(1, 10)] private float _reloadTime;
    [SerializeField, Range(0.03f, 5)] private float _delayBetweenShoots;
    [SerializeField, Range(10, 200)] private float _fireRange;
    [SerializeField, Range(1, 10)] private float _spreadRange;

    public int Damage => _damage;
    public int MaxAmmo => _maxAmmoCapacity;
    public float ReloadTime => _reloadTime;
    public float DelayBetweenShoots => _delayBetweenShoots;
    public float FireRange => _fireRange;
    public float SpreadRange => _spreadRange;
}
