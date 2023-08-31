using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Juggernaut : Character
{
    [Header("JUGGERNAUT")]
    public int TitanID;
    public float transformTime;
    public GameObject Gun_JuggernautModel;
    public WeaponRaycast Juggernaut_Raycast;
    public Transform raycastOriginHand;
    public Transform raycastOrigin;
    public ParticleSystem EnergyGlobe;
    public ParticleSystem EnergyGlobeHand;
    public ParticleSystem BomExplosion;
    float xScaleEnergyGl;
    float yScaleEnergyGl;
    float zScaleEnergyGl;

    public LineRenderer line;
    public LineRenderer lineHand;
    private LineRenderer lineMain;
    public GameObject Volume;
    public float timesetExplosion;
    float timecharging;
    RaycastHit Hit;
    Vector3 HitPositionLine;
    bool Hit_Bool;
    bool isCharging;
    public float damageRangeExplosion;


    protected override void Awake()
    {
        base.Awake();
        line.enabled = false;
        lineHand.enabled = false;
        Juggernaut_Raycast = Gun_JuggernautModel.GetComponentInParent<WeaponRaycast>();
        Gun_JuggernautModel.SetActive(false);
        Volume.gameObject.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        //ExecutionPhase
        if (!this.isReadySpecialSkill && !this.isCoolingDownSpecicalSkill)
        {
            if (Juggernaut_Raycast.currentAmmo <= 0)
            {
                StopCoroutine(TransformationCoroutine());
                RevertoForm();
            }
        }

        if (!IsCharacterForm) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (PlayerCtrl.Instance.PlayerCamera.FPSCam.gameObject.activeInHierarchy == true)
            {
                EnergyGlobe.Play();
            }
            else
            {
                EnergyGlobeHand.Play();
            }
            timesetExplosion = Time.time;
        }
        if (Input.GetMouseButton(0))
        {
            ChargingEnergy();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Fire();
            Invoke("DeactiveLine", 0.1f);
        }


        //
        this.HandleAfterFire();
        this.HandleRender();
        this.HandleChargingEnergy();
        this.Juggernaut_Raycast.UpdateBullets();

    }

    public void Fire()
    {
        Juggernaut_Raycast.FireBullet(PlayerCtrl.Instance.PlayerWeapon.PlayerWeaponActive.crossHairTarget.position);
        lineMain.enabled = true;
        isCharging = false;
        Volume.gameObject.SetActive(true);
    }

    public override void SpecialSkill()
    {
        base.SpecialSkill();
        if (this.isReadySpecialSkill)
        {
            StartCoroutine(TransformationCoroutine());
        }
    }

    private void HandleChargingEnergy()
    {
        if (isCharging)
        {
            PlayerCtrl.Instance.PlayerLocomotion.IsSprinting = false;
            PlayerCtrl.Instance.PlayerLocomotion.IsWalking = true;
            PlayerCtrl.Instance.PlayerInput.InteractInput = false;
        }
        else if (!isCharging)
        {
            this.SetOriginalScaleParticle();
        }
    }

    private void SetOriginalScaleParticle()
    {
        xScaleEnergyGl = 0;
        yScaleEnergyGl = 0;
        zScaleEnergyGl = 0;
        EnergyGlobe.gameObject.transform.localScale = Vector3.zero;
        EnergyGlobeHand.gameObject.transform.localScale = Vector3.zero;
        if (EnergyGlobe.isPlaying)
        {
            EnergyGlobe.Stop();
            EnergyGlobe.Clear();
        }
        if (EnergyGlobeHand.isPlaying)
        {
            EnergyGlobeHand.Stop();
            EnergyGlobeHand.Clear();
        }
    }

    private void ChargingEnergy()
    {
        timecharging = Time.time - timesetExplosion;
        isCharging = true;
        if (timecharging >= 0 && timecharging < 3)
        {
            damageRangeExplosion = 1.4f;
            BomExplosion.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (timecharging >= 3 && timecharging < 6)
        {
            damageRangeExplosion = 4.1f;
            BomExplosion.gameObject.transform.localScale = new Vector3(3, 3, 3);
        }
        else if (timecharging >= 6 && timecharging < 12)
        {
            damageRangeExplosion = 6.5f;
            BomExplosion.gameObject.transform.localScale = new Vector3(4.5f, 4.5f, 4.5f);
        }
        else if (timecharging >= 12)
        {
            damageRangeExplosion = 10.7f;
            BomExplosion.gameObject.transform.localScale = new Vector3(7f, 7f, 7f);
        }
        if (timecharging >= 12) return;
        xScaleEnergyGl += (float)0.02 * Time.deltaTime;
        yScaleEnergyGl += (float)0.02 * Time.deltaTime;
        zScaleEnergyGl += (float)0.02 * Time.deltaTime;
        EnergyGlobe.gameObject.transform.localScale = new Vector3(xScaleEnergyGl, yScaleEnergyGl, zScaleEnergyGl);
        EnergyGlobeHand.gameObject.transform.localScale = new Vector3(xScaleEnergyGl * 4, yScaleEnergyGl * 4, zScaleEnergyGl * 4);
    }

    public void EnableBomExplosion()
    {
        BomExplosion.gameObject.transform.position = Hit.point;
        BomExplosion.Play();
        Collider[] colliders = Physics.OverlapSphere(Hit.point, damageRangeExplosion);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<ColliderHit>())
            {
                collider.GetComponent<ColliderHit>().Hit();
            }
        }
    }
    private void HandleAfterFire()
    {
        Vector3 crossHair = PlayerCtrl.Instance.PlayerWeapon.PlayerWeaponActive.crossHairTarget.position;
        Vector3 ifNotHitpoint = crossHair - Juggernaut_Raycast.raycastOrigin.position;
        Vector3 direction = crossHair - Juggernaut_Raycast.raycastOrigin.position;
        Ray ray = new Ray(Juggernaut_Raycast.raycastOrigin.position, direction);
        Hit_Bool = Physics.Raycast(ray, out Hit, 1000f);
        HitPositionLine = Hit_Bool ? Hit.point : ifNotHitpoint * 1000f;
        if (lineMain)
        {
            lineMain.SetPosition(0, Juggernaut_Raycast.raycastOrigin.position);
            lineMain.SetPosition(1, HitPositionLine);
        }
        float distance;

        if (Hit_Bool)
        {
            distance = (Hit.point - transform.position).magnitude;
            if (distance < 20f && Input.GetMouseButtonUp(0))
            {
                Invoke("EnableBomExplosion", 0.01f);
            }
            if (distance >= 20f && Input.GetMouseButtonUp(0))
            {
                Invoke("EnableBomExplosion", 0.2f);
            }

        }
    }

    private void HandleRender()
    {
        if (PlayerCtrl.Instance.PlayerCamera.FPSCam.gameObject.activeInHierarchy == true)
        {
            PlayerCtrl.Instance.PlayerCamera.cameraMain.cullingMask &= ~(1 << 6);
            this.lineMain = this.line;
            Gun_JuggernautModel.gameObject.SetActive(true);
            this.RigAnimator.SetBool("aim_character", true);
            Juggernaut_Raycast.raycastOrigin = raycastOrigin;
            if (!EnergyGlobe.isPlaying && isCharging)
            {
                EnergyGlobe.Play();
            }
        }
        else
        {
            PlayerCtrl.Instance.PlayerCamera.cameraMain.cullingMask |= 1 << 6;
            this.lineMain = this.lineHand;
            Gun_JuggernautModel.gameObject.SetActive(false);
            this.RigAnimator.SetBool("aim_character", false);
            Juggernaut_Raycast.raycastOrigin = raycastOriginHand;
            if (!EnergyGlobeHand.isPlaying && isCharging)
            {
                EnergyGlobeHand.Play();
            }
        }
    }


    public void DeactiveLine()
    {
        lineMain.enabled = false;
        Volume.gameObject.SetActive(false);
    }
    public override void ActionMouseL()
    {
        base.ActionMouseL();
        //this.Animator.SetTrigger("shoot");
    }

    public override void ActionMouseR(bool inputButton)
    {
        // if (isCharging) return;
        PlayerCtrl.Instance.PlayerAim.isAim = inputButton;
    }

    private IEnumerator TransformationCoroutine()
    {
        this.Transform();
        Debug.Log("StartCRT");
        yield return new WaitForSeconds(this.transformTime);

        this.RevertoForm();
    }

    public void Transform()
    {
        this.isCharacterForm = true;
        this.isReadySpecialSkill = false;
        PlayerCtrl.Instance.PlayerCombatAction.SetActionMouseLeft(CombatAction.CharacterSpecific);
        PlayerCtrl.Instance.PlayerCombatAction.SetActionMouseRight(CombatAction.CharacterSpecific);
        this.rigAnimator.SetBool("transform", true);
    }

    public void RevertoForm()
    {
        this.Juggernaut_Raycast.currentAmmo = this.Juggernaut_Raycast.maxAmmo;
        this.isCharacterForm = false;
        this.isCoolingDownSpecicalSkill = true;
        this.SetOriginalScaleParticle();
        PlayerCtrl.Instance.PlayerCombatAction.SetActionMouseLeft(CombatAction.None);
        this.RigAnimator.SetBool("transform", false);
        PlayerCtrl.Instance.PlayerCamera.cameraMain.cullingMask |= 1 << 6;
        this.Gun_JuggernautModel.gameObject.SetActive(false);
    }
}
