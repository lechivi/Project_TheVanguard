using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RaycastWeapon;

public class RaycastWeapon : MonoBehaviour
{
    public float timeDelayShot = 1f;
    public float[] spreads;
    public ParticleSystem[] muzzleflash;
    public ParticleSystem hitEffect;
    public Transform raycastOrigin;
    private Ray ray;
    private RaycastHit hitInfo;
    public int fireRate = 25;
    public float runtTimeFire;
    public float bulletSpeed = 1000;
    public float bulletDrop = 0;
    public float maxlifeTime;
    public string weaponName;
    public PlayerWeaponActiveOld.Weaponslot weaponslot;
    public WeaponType WeaponType;
    public ShotGunType ShotGunType;
    public float ForceRaycast;
    public RecoilWeapon recoil;
    public GameObject magazine;
    public int ammo;
    public int maxAmmo;
    public int ammoPerShot;
    private void Awake()
    {
        runtTimeFire = 0;
        recoil = GetComponent<RecoilWeapon>();
    }

    public void FireBullet(Vector3 target)
    {
        if (ammo <= 0)
        {
            return;
        }
        ammo --;
        foreach (var particle in muzzleflash)
        {
            particle.Emit(1);
        }

        for (int i = 0; i < ammoPerShot; i++)
        {
            float xspread = Random.Range(spreads[0], spreads[1]);
            float yspread = Random.Range(spreads[2], spreads[3]);
            Vector3 randomSpread = new Vector3(xspread, yspread, 0f);
            Vector3 raycastDirection = ((target - raycastOrigin.position).normalized + randomSpread) * bulletSpeed;
            var bullet = ObjectPool.Instance.GetPooledObject();
            bullet.Active(raycastOrigin.position, raycastDirection);
            recoil.GenerateRecoil(weaponName);
        }
    }

    public void UpdateFiring(Vector3 target)
    {
        runtTimeFire += Time.deltaTime;
        float fireInterval = timeDelayShot / fireRate; // 1/firerate ( 1 ở đây đại diện cho 1 giây , firerate là số đạn nhả ra trong 1 giây , nếu ta set firerate = 25 là 25 viên trong 1s
        while (runtTimeFire >= 0.0f)
        {
            FireBullet(target);
            runtTimeFire -= fireInterval;
        }
    }

    public void UpdateBullets()
    {
        ObjectPool.Instance.pooledObjects.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += Time.deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
        DestroyBullets();
    }

    public Vector3 GetPosition(Bullet bullet)
    {
        // p = p0 + v*t +0.5*g*t*t
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.intitialPosition) + (bullet.intitialVelocity * bullet.time) + (0.5f * gravity * bullet.time * bullet.time);
    }

    public void RaycastSegment(Vector3 Start, Vector3 End, Bullet bullet)
    {
        Vector3 direction = End - Start;
        float distance = direction.magnitude;
        ray.origin = Start;
        ray.direction = direction;
        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward =/* HandleHiteffectDirection(ray.direction, hitInfo.normal);*/ hitInfo.normal;
            hitEffect.Emit(1);

            bullet.transform.position = hitInfo.point;
            bullet.time = maxlifeTime;
            End = hitInfo.point;

            AddForceToHitInfo(ray, hitInfo);

        }
        bullet.transform.position = End;
    }

    public void DestroyBullets()
    {
        foreach (Bullet bullet in ObjectPool.Instance.pooledObjects)
        {
            if (bullet.time >= maxlifeTime)
            {
                bullet.Deactive();
            }
        }
    }


    public Vector3 HandleHiteffectDirection(Vector3 ray, Vector3 hitNor)
    {
        float dot = Vector3.Dot(ray, hitNor);
        Vector3 newDerec = 2 * dot * hitNor;
        return ray - newDerec;
    }

    public void AddForceToHitInfo(Ray ray, RaycastHit hitInfo)
    {
        var hitpointObj = hitInfo.collider.GetComponent<Rigidbody>();
        if (hitpointObj != null)
        {
            hitpointObj.AddForceAtPosition(ray.direction * ForceRaycast, hitInfo.point, ForceMode.Impulse);
        }
    }

}
