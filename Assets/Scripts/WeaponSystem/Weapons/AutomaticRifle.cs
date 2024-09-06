using UnityEngine;

[CreateAssetMenu(fileName = "AutomaticRifle", menuName = "Weapon type/Automatic Rifle", order = 1)]
public class AutomaticRifle : WeaponType
{

    public override void shoot()
    {
    }

    private void OnEnable()
    {
        BulletSpread = 0;
        BulletPerShot = 1;
        IsAutomatic = true;
        ShootingDistance = ShootingDistanceEnum.Medium;
    }
}