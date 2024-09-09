using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Camera playerCamera;

    //shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    float shootingDelay = 2f;

    //burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    //spread
    public float spreadIntensity;


    //bullet properties
    public WeaponType weaponType;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifetime = 3f;

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShoot && isShooting)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon(); 
        }

    }

    private void FireWeapon()
    {
        readyToShoot = false;
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        bullet.transform.forward = shootingDirection;
        
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifetime));

        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon",shootingDelay);
        }

    }

    private Vector3 CalculateDirectionAndSpread()
    {

    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float bulletPrefabLifetime)
    {
        yield return new WaitForSeconds(bulletPrefabLifetime);
        Destroy(bullet);
    }
}
