using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "AutomaticRifle", menuName = "Weapon type", order = 1)]

public class WeaponType : ScriptableObject
{
    [SerializeField] private int magazineCapacity;
    [SerializeField] private int bulletPerBurstShot;
    [SerializeField] private float bulletSpread;
    [SerializeField] private ShootingDistanceEnum shootingDistance;
    [SerializeField] private bool isAutomatic,isBurst,isSingle;

    public int MagazineCapacity { get => magazineCapacity; set => magazineCapacity = value; }
    public int BulletPerBurstShot { get => bulletPerBurstShot; set => bulletPerBurstShot = value; }
    public float BulletSpread { get => bulletSpread; set => bulletSpread = value; }
    public ShootingDistanceEnum ShootingDistance { get => shootingDistance; set => shootingDistance = value; }
    public bool IsAutomatic { get => isAutomatic; set => isAutomatic = value; }
    public bool IsBurst { get => isBurst; set => isBurst = value; }
    public bool IsSingle { get => isSingle; set => isSingle = value; }

    public enum ShootingDistanceEnum
    {
        Short,
        Medium,
        Long
    }


 
}
