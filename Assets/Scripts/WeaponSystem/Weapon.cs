using Photon.Pun;
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
    float shootingDelay = 0.2f;

    //burst
    public int burstBulletsLeft;

    //spread
    public float spreadIntensity;


    //bullet properties
    public WeaponType weaponType;
    PhotonView photonView;
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
        burstBulletsLeft = weaponType.BulletPerBurstShot;
    }

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if ((currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst))
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShoot && isShooting)
        {
            burstBulletsLeft = weaponType.BulletPerBurstShot;
            FireWeapon(); 
        }

    }

    private void FireWeapon()
    {
        if (photonView.IsMine){
        readyToShoot = false;
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        //instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        //adjusting direction
        bullet.transform.forward = shootingDirection;
        
        //shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);

        //destroy bullet after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifetime));

        // checking if we are done shooting
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

            // burst mode
            if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
            {
                burstBulletsLeft--;
                Invoke("FireWeapon", shootingDelay);
            }
        }

    }

    private void swapWeaponMode()
    {
        /*weapon swap function that operates with if conditions based on
        boolean operators that implemented in weaponType class.*/
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray,out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-weaponType.BulletSpread, weaponType.BulletSpread);
        float y = UnityEngine.Random.Range(-weaponType.BulletSpread, weaponType.BulletSpread);

        return direction + new Vector3(x, y, 0);

    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float bulletPrefabLifetime)
    {
        yield return new WaitForSeconds(bulletPrefabLifetime);
        Destroy(bullet);
    }
}
