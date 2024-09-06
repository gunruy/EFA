using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class WeaponType : ScriptableObject
{
    [SerializeField] private bool isAutomatic;
    [SerializeField] private int magazineCapacity;
    [SerializeField] private int bulletPerShot;
    [SerializeField] private float bulletSpread;
    [SerializeField] private ShootingDistanceEnum shootingDistance;

    public bool IsAutomatic { get => isAutomatic; set => isAutomatic = value; }
    public int MagazineCapacity { get => magazineCapacity; set => magazineCapacity = value; }
    public int BulletPerShot { get => bulletPerShot; set => bulletPerShot = value; }
    public float BulletSpread { get => bulletSpread; set => bulletSpread = value; }
    public ShootingDistanceEnum ShootingDistance { get => shootingDistance; set => shootingDistance = value; }


    public enum ShootingDistanceEnum
    {
        Short,
        Medium,
        Long
    }

    public abstract void shoot();

 
}
