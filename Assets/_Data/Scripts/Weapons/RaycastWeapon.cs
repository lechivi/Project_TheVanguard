using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public class Bullet
    {
        public float time;
        public Vector3 intitialPosition;
        public Vector3 intitialVelocity;
        public TrailRenderer tracer;
        public int bounce;
    }

    public bool isFiring = false;
    public ParticleSystem[] muzzleflash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;
    public Transform raycastOrigin;
    private Ray ray;
    private RaycastHit hitInfo;
    public int fireRate = 25;
    public float runtTimeFire;
    public float bulletSpeed = 1000;
    public float bulletDrop = 0;
    //public Bullet bullet;
    private List<Bullet> bullets = new List<Bullet>();
    public float maxlifeTime;
    public string weaponName;
    public PlayerWeaponActiveOld.Weaponslot weaponslot;
    public float ForceRaycast;
    public int maxBounce;
    public RecoilWeapon recoil;
    public GameObject magazine;
    public int ammo;
    public int maxAmmo;
    private void Awake()
    {
        runtTimeFire = 0;
        recoil = GetComponent<RecoilWeapon>();
    }
    /* public void StartFiring()
     {
         isFiring = true;
         FireBullet();
        // runtTimeFire = 0;
     }*/

    public void FireBullet(Vector3 target)
    {
        if (ammo <= 0)
        {
            return;
        }
        ammo--;
        foreach (var particle in muzzleflash)
        {
            particle.Emit(1);
        }

        Vector3 raycastDirection = (target - raycastOrigin.position).normalized  * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, raycastDirection);
        recoil.GenerateRecoil(weaponName);
        bullets.Add(bullet);
    }

    public void UpdateFiring(Vector3 target)
    {
        runtTimeFire += Time.deltaTime;
        float fireInterval = 1.0f / fireRate; // 1/firerate ( 1 ở đây đại diện cho 1 giây , firerate là số đạn nhả ra trong 1 giây , nếu ta set firerate = 25 là 25 viên trong 1s
        while (runtTimeFire >= 0.0f)
        {
            FireBullet(target);
            runtTimeFire -= fireInterval;
        }
    }


    public Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet()
        {
            intitialPosition = position,
            intitialVelocity = velocity,
            time = 0f,
            tracer = Instantiate(tracerEffect, position, Quaternion.identity),
            bounce = maxBounce
        };
        bullet.tracer.AddPosition(position);
        return bullet;
    }
    public void UpdateBullets()
    {
        bullets.ForEach(bullet =>
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

            bullet.tracer.transform.position = hitInfo.point;
            bullet.time = maxlifeTime;
            End = hitInfo.point;

            AddForceToHitInfo(ray, hitInfo);
           // FireBounce(bullet);

        }
        bullet.tracer.transform.position = End;
    }

    public void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time > maxlifeTime);
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

    public void FireBounce(Bullet bullet)
    {
        if (bullet.bounce > 0)
        {
            bullet.time = 0f;
            bullet.intitialPosition = hitInfo.point;
            bullet.intitialVelocity = Vector3.Reflect(bullet.intitialVelocity, hitInfo.normal);
            bullet.bounce--;

        }
    }
}
