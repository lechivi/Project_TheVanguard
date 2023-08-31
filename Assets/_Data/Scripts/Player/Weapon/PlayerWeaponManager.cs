using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum WeaponList
{
    EquippedWeapons,
    BackpackWeapons
}

public class PlayerWeaponManager : PlayerWeaponAbstract
{
    public static PlayerWeaponManager Instance;
    private PlayerWeapon playerWeapon;
    [SerializeField] private UI_InventoryPanel ui_InventoryPanel;

    [SerializeField] private int maxEquippedWeapon = 3;
    [SerializeField] private int maxBackpackWeapon = 3;
    [SerializeField] private NullAwareList<Weapon> equippedWeapons = new NullAwareList<Weapon>();
    [SerializeField] private NullAwareList<Weapon> backpackWeapons = new NullAwareList<Weapon>();
    [SerializeField] private Transform[] weaponSheathSlots = new Transform[3];

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float dropForce = 3f;
    [SerializeField] private GameObject pickupObjectPrefab;

    private int currentWeaponIndex = -1;
    private bool isReadySwap = true;

    private bool isHolstering;
    private bool originalHolster = false;
    public bool IsHolstering { get => this.isHolstering; }
    [SerializeField] private float swapCooldown = 0.2f;

    public NullAwareList<Weapon> EquippedWeapons { get => this.equippedWeapons; }
    public NullAwareList<Weapon> BackpackWeapons { get => this.backpackWeapons; }
    public Transform[] WeaponSheathSlots { get => this.weaponSheathSlots; set => this.weaponSheathSlots = value; }

    protected override void Awake()
    {
        base.Awake();
        PlayerWeaponManager.Instance = this;
        playerWeapon = transform.GetComponentInParent<PlayerWeapon>();
        this.equippedWeapons.GenerateList(this.maxEquippedWeapon);
        this.backpackWeapons.GenerateList(this.maxBackpackWeapon);
        isHolstering = false;
    }

    private void Update()
    {
        setOffset();
        SetHolster(false);
        SetDefaultCurrentindex();
        CharacterState();

        if (this.isReadySwap)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                this.SetActiveWeapon(this.currentWeaponIndex + 1, false);
            }

            if (Input.mouseScrollDelta.y > 0)
            {
                this.SetActiveWeapon(this.currentWeaponIndex - 1, false);
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                this.SetActiveWeapon(this.currentWeaponIndex + 1, false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                this.SetActiveWeapon(0, true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                this.SetActiveWeapon(1, true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PlayerWeapon playerWeapon = transform.GetComponentInParent<PlayerWeapon>();
                this.SetActiveWeapon(2, true);
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            int index = this.equippedWeapons.GetList().FindIndex(item => item != null);
            if (index == -1) return;
            this.RemoveWeaponFromEquipped(this.equippedWeapons.GetList()[index], true);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            int index = this.backpackWeapons.GetList().FindIndex(item => item != null);
            if (index == -1) return;
            this.RemoveWeaponFromBackpack(this.backpackWeapons.GetList()[index], true);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            this.PlayerWeapon.PlayerCtrl.PlayerInput.gameObject.SetActive(false);
        }
    }

    public bool AddWeapon(Weapon weapon)
    {
        if (this.equippedWeapons.GetList().Count < this.maxEquippedWeapon || (this.equippedWeapons.IsContainsNull() && this.equippedWeapons.GetList().Count <= this.maxEquippedWeapon))
        {
            this.AddWeaponToEquipped(this.GetNewWeapon(weapon));
            this.UpdateUI();
            return true;
        }
        if (this.backpackWeapons.GetList().Count < this.maxBackpackWeapon || (this.backpackWeapons.IsContainsNull() && this.backpackWeapons.GetList().Count <= this.maxBackpackWeapon))
        {
            this.AddWeaponToBackpack(this.GetNewWeapon(weapon));
            this.UpdateUI();
            return true;
        }
        Debug.Log("Can't hold any more weapon");
        return false;
    }

    private Weapon GetNewWeapon(Weapon weapon)
    {
        Weapon w = Instantiate(weapon);
        return w;
    }

    private void AddWeaponToEquipped(Weapon weapon)
    {
        WeaponRaycast raycastWeapon = weapon.GetComponent<WeaponRaycast>();
        if (raycastWeapon != null)
        {
            raycastWeapon.recoil.playerFPSCam = playerWeapon.PlayerCtrl.PlayerCamera.FPSCam;
            raycastWeapon.recoil.playerTPSCam = playerWeapon.PlayerCtrl.PlayerCamera.TPSCam;
            raycastWeapon.recoil.rigController = playerWeapon.PlayerCtrl.RigAnimator;
        }

        this.equippedWeapons.Add(weapon);
        this.SetSheathForWeapon(weapon);
        if (this.currentWeaponIndex == -1)
        {
            this.SetActiveWeapon(this.equippedWeapons.GetList().IndexOf(weapon), false);
            // playerWeapon.PlayerCtrl.RigAnimator.SetTrigger("equip");
            SetAnimationEquip(weapon);
        }
    }

    private void AddWeaponToBackpack(Weapon weapon)
    {
        WeaponRaycast raycastWeapon = weapon.GetComponent<WeaponRaycast>();
        if (raycastWeapon != null)
        {
            raycastWeapon.recoil.playerFPSCam = playerWeapon.PlayerCtrl.PlayerCamera.FPSCam;
            raycastWeapon.recoil.playerTPSCam = playerWeapon.PlayerCtrl.PlayerCamera.TPSCam;
            raycastWeapon.recoil.rigController = playerWeapon.PlayerCtrl.RigAnimator;
        }
        this.backpackWeapons.Add(weapon);

        this.SetSheathForWeapon(weapon);
    }

    private void SetSheathForWeapon(Weapon weapon)
    {
        weapon.transform.SetParent(this.weaponSheathSlots[(int)weapon.WeaponSlot[0] - 1]); //TODO: Change to RigLayer

        /* if (weapon.WeaponData.WeaponType == WeaponType.Melee)
         {
             if (weapon.WeaponSlot[0] == WeaponSlot.Back)
             {
                 weapon.transform.localPosition = this.offsetPosMeleeHolderBack;
                 weapon.transform.localRotation = Quaternion.Euler(this.offsetRotMeleeHolderBack);
                 weapon.transform.localScale = Vector3.one;
             }
             else if (weapon.WeaponSlot[0] == WeaponSlot.LeftHip)
             {
                 weapon.transform.localPosition = this.offsetPosMeleeHolderLeft;
                 weapon.transform.localRotation = Quaternion.Euler(this.offsetRotMeleeHolderLeft);
                 weapon.transform.localScale = Vector3.one;
             }
         }
         else
         {
             weapon.transform.localPosition = Vector3.zero;
             weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
             weapon.transform.localScale = Vector3.one;
         }*/

        weapon.gameObject.SetActive(false);
    }

    public void RemoveWeaponFromEquipped(int weaponIndex, bool isDrop)
    {
        Weapon weapon = this.equippedWeapons.GetList()[weaponIndex];
        this.equippedWeapons.Remove(weapon);
        if (isDrop)
        {
            this.DropWeapon(weapon);
        }
    }

    public void RemoveWeaponFromEquipped(Weapon weapon, bool isDrop)
    {
        this.equippedWeapons.Remove(weapon);
        if (isDrop)
        {
            this.DropWeapon(weapon);
        }
    }

    public void RemoveWeaponFromBackpack(int weaponIndex, bool isDrop)
    {
        Weapon weapon = this.backpackWeapons.GetList()[weaponIndex];
        this.backpackWeapons.Remove(weapon);
        if (isDrop)
        {
            this.DropWeapon(weapon);
        }
    }

    public void RemoveWeaponFromBackpack(Weapon weapon, bool isDrop)
    {
        this.backpackWeapons.Remove(weapon);
        if (isDrop)
        {
            this.DropWeapon(weapon);
        }
    }

    private void DropWeapon(Weapon weapon)
    {
        Transform playerTransform = this.playerWeapon.PlayerCtrl.PlayerTransform;
        Vector3 dropPosition = playerTransform.position + new Vector3(0, 1.35f, 0.67f);
        Quaternion dropRoation = playerTransform.rotation;

        GameObject pickupObject = Instantiate(this.pickupObjectPrefab, dropPosition, dropRoation, this.spawnPoint);
        PickupWeapons pickupWeapon = pickupObject.GetComponent<PickupWeapons>();
        pickupWeapon.SetWeapon(weapon.gameObject);

        Rigidbody weaponRigidbody = pickupObject.transform.GetComponent<Rigidbody>();
        if (weaponRigidbody != null)
        {
            float randomDropForce = Random.Range(this.dropForce - 1f, this.dropForce + 1f);
            Vector3 randomDropDirection = new Vector3(Random.Range(-0.5f, 0.6f), 0, 0);

            weaponRigidbody.AddForce((playerTransform.forward + randomDropDirection) * randomDropForce, ForceMode.Impulse);
        }

        this.UpdateUI();
    }

    public void SwitchWeapon(WeaponList weaponListA, int weaponIndexA, WeaponList weaponListB, int weaponIndexB)
    {
        Weapon weaponA, weaponB;
        bool isTheCurrentWeaponA = false;
        bool isTheCurrentWeaponB = false;
        if (weaponListA == WeaponList.EquippedWeapons)
        {
            weaponA = this.equippedWeapons.GetList()[weaponIndexA];

            if (this.currentWeaponIndex == weaponIndexA && weaponA != null)
            {
                weaponA.gameObject.SetActive(false);
                isTheCurrentWeaponA = true;
            }
        }
        else
        {
            weaponA = this.backpackWeapons.GetList()[weaponIndexA];
        }

        if (weaponListB == WeaponList.EquippedWeapons)
        {
            weaponB = this.equippedWeapons.GetList()[weaponIndexB];
            if (this.currentWeaponIndex == weaponIndexB && weaponB != null)
            {
                weaponB.gameObject.SetActive(false);
                isTheCurrentWeaponB = true;
            }
        }
        else
        {
            weaponB = this.backpackWeapons.GetList()[weaponIndexB];
        }

        //bool foundWeaponA = this.equippedWeapons.GetList().Contains(weaponA) || this.backpackWeapons.GetList().Contains(weaponA);
        //bool foundWeaponB = this.equippedWeapons.GetList().Contains(weaponB) || this.backpackWeapons.GetList().Contains(weaponB);
        //if (!foundWeaponA || !foundWeaponB) return;

        List<Weapon> equippedList = this.equippedWeapons.GetList();
        List<Weapon> backpackList = this.backpackWeapons.GetList();

        //Case: Equipped -> Equipped
        if (weaponListA == WeaponList.EquippedWeapons && weaponListB == WeaponList.EquippedWeapons)
        {
            Weapon temp = weaponA;
            equippedList[weaponIndexA] = weaponB;
            equippedList[weaponIndexB] = temp;
        }

        //Case: Equipped -> Backpack
        else if (weaponListA == WeaponList.EquippedWeapons && weaponListB == WeaponList.BackpackWeapons)
        {
            equippedList[weaponIndexA] = weaponB;
            backpackList[weaponIndexB] = weaponA;
        }

        //Case: Backpack -> Equipped
        else if (weaponListA == WeaponList.BackpackWeapons && weaponListB == WeaponList.EquippedWeapons)
        {
            backpackList[weaponIndexA] = weaponB;
            equippedList[weaponIndexB] = weaponA;
        }

        //Case: Backpack -> Backpack
        else if (weaponListA == WeaponList.BackpackWeapons && weaponListB == WeaponList.BackpackWeapons)
        {
            Weapon temp = weaponA;
            backpackList[weaponIndexA] = weaponB;
            backpackList[weaponIndexB] = temp;
        }

        if (isTheCurrentWeaponA && weaponB != null)
            weaponB.gameObject.SetActive(true);
        if (isTheCurrentWeaponB && weaponA != null)
            weaponA.gameObject.SetActive(true);
    }

    public void SetActiveWeapon(int weaponIndex, bool isAlpha)
    {
        List<Weapon> listEquippedWeapon = this.equippedWeapons.GetList();
        int weaponCount = listEquippedWeapon.Count;
        if (weaponCount == 0) return;

        if (isAlpha)
        {
            if (weaponIndex < 0 || weaponIndex >= weaponCount || listEquippedWeapon[weaponIndex] == null)
            {
                Debug.Log("Don't have that weapon index");
                return;
            }
        }
        else
        {
            weaponIndex = (weaponIndex % weaponCount + weaponCount) % weaponCount;
            //if (weaponIndex >= weaponCount)
            //    weaponIndex = 0;
            //else if (weaponIndex < 0)
            //    weaponIndex = weaponCount - 1;
        }
        if (weaponIndex == this.currentWeaponIndex && !isHolstering || listEquippedWeapon[weaponIndex] == null) return;

        if (this.currentWeaponIndex > -1 && listEquippedWeapon[currentWeaponIndex] != null)
        {
            listEquippedWeapon[this.currentWeaponIndex].gameObject.SetActive(false);
            Debug.Log("Run");
        }


        listEquippedWeapon[weaponIndex].gameObject?.SetActive(true);

        isHolstering = false;
        SetAnimationEquip(listEquippedWeapon[weaponIndex]);
        // playerWeapon.PlayerCtrl.RigAnimator.SetTrigger("equip");


        this.currentWeaponIndex = weaponIndex;

        this.UpdateUI();
        StartCoroutine(this.StartCooldown());
    }
    private IEnumerator StartCooldown()
    {
        this.isReadySwap = false;
        yield return new WaitForSeconds(this.swapCooldown);
        this.isReadySwap = true;
    }

    public void UpdateUI()
    {
        if (this.ui_InventoryPanel == null) return;
        this.ui_InventoryPanel.ResetSlot();
        List<UI_DraggableItem> equippedList = this.ui_InventoryPanel.DraggablesEquippedList;
        List<UI_DraggableItem> backpackList = this.ui_InventoryPanel.DraggablesBackpackList;

        for (int i = 0; i < this.equippedWeapons.GetList().Count; i++)
        {
            if (this.equippedWeapons.GetList()[i] == null)
            {
                equippedList[i].SetActiveSlot(false);
                continue;
            }
            equippedList[i].SetActiveSlot(true);
            equippedList[i].SetWeaponData(this.equippedWeapons.GetList()[i].WeaponData);
            equippedList[i].SetModel();
        }
        for (int i = 0; i < this.backpackWeapons.GetList().Count; i++)
        {
            if (this.backpackWeapons.GetList()[i] == null)
            {
                backpackList[i].SetActiveSlot(false);
                continue;
            }
            backpackList[i].SetActiveSlot(true);
            backpackList[i].SetWeaponData(this.backpackWeapons.GetList()[i].WeaponData);
            backpackList[i].SetModel();
        }

        if (this.currentWeaponIndex == -1) return;
        this.ui_InventoryPanel.EquippedList.SetEquipSlot(this.currentWeaponIndex);
    }

    public int GetCurrentWeaponIndex()
    {
        return this.currentWeaponIndex;
    }
    public Weapon GetActiveWeapon()
    {
        if (this.currentWeaponIndex < 0/* && this.equippedWeapons.GetList()[this.currentWeaponIndex] == null*/) return null;
        return this.equippedWeapons.GetList()[this.currentWeaponIndex];
    }

    public WeaponRaycast GetActiveRaycastWeapon()
    {
        Weapon weapon = GetActiveWeapon();
        if (weapon == null) return null;
        WeaponRaycast activeRaycastWeapon = weapon.GetComponent<WeaponRaycast>();
        return activeRaycastWeapon;
    }


    public void CharacterState()
    {
        List<Weapon> listEquippedWeapon = this.equippedWeapons.GetList();
        if (currentWeaponIndex > -1 && listEquippedWeapon[currentWeaponIndex] == null || currentWeaponIndex == -1)
        {
            playerWeapon.PlayerCtrl.RigAnimator.SetBool("holster_weapon", true);
            return;
        }
        playerWeapon.PlayerCtrl.RigAnimator.SetBool("holster_weapon", isHolstering);
    }

    public void SetHolster(bool button)
    {
        List<Weapon> listEquippedWeapon = this.equippedWeapons.GetList();

        if (button && currentWeaponIndex > -1 && listEquippedWeapon[currentWeaponIndex] != null)
        {
            isHolstering = !isHolstering;
        }
        if (!button)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isHolstering ||
                PlayerCtrl.Instance.Character.IsCharacterForm && Input.GetKeyDown(KeyCode.C) && !isHolstering)
            {
                originalHolster = false;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && isHolstering ||
                 PlayerCtrl.Instance.Character.IsCharacterForm && Input.GetKeyDown(KeyCode.C) && isHolstering)

            {
                originalHolster = true;
            }

            if (playerWeapon.PlayerCtrl.PlayerLocomotion.IsSprinting == true && currentWeaponIndex > -1 ||
                playerWeapon.PlayerCtrl.Character.IsCharacterForm && currentWeaponIndex > -1)
            {
                isHolstering = true;
            }
/*            if (playerWeapon.PlayerCtrl.Character.IsCharacterForm && currentWeaponIndex > -1)
            {
                isHolstering = true;
            }*/
            if (Input.GetKeyUp(KeyCode.LeftShift) && currentWeaponIndex > -1 ||
                PlayerCtrl.Instance.Character.TimerCD_SpecialSkill < 0.05 && PlayerCtrl.Instance.Character.IsCoolingDownSpecicalSkill)
            {
                isHolstering = originalHolster;
            }

        }
    }
    public void SetDefaultCurrentindex()
    {
        List<Weapon> listEquippedWeapon = this.equippedWeapons.GetList();
        if (currentWeaponIndex > -1)
        {
            if (listEquippedWeapon[currentWeaponIndex] == null)
            {
                currentWeaponIndex = -1;
            }
        }

    }
    public void SetAnimationEquip(Weapon weapon)
    {
        if (PlayerCtrl.Instance.Character.IsCharacterForm) return;
        if (weapon.WeaponData.WeaponType == WeaponType.Melee && weapon.WeaponData.WeaponType != WeaponType.None)
        {
            playerWeapon.PlayerCtrl.RigAnimator.SetTrigger("equip_melee");
        }
        if (weapon.WeaponData.WeaponType == WeaponType.Pistol && weapon.WeaponData.WeaponType != WeaponType.None)
        {
            playerWeapon.PlayerCtrl.RigAnimator.SetTrigger("equip_pistol");
        }
        else
        {
            playerWeapon.PlayerCtrl.RigAnimator.SetTrigger("equip");
        }
    }

    public void setOffset()
    {
        Weapon weapon = GetActiveWeapon();
        if (weapon != null /*&& weapon.WeaponData.WeaponType == WeaponType.Melee*/)
        {
            if (isHolstering)
            {
                if (weapon.gameObject.transform.localPosition == weapon.WeaponData.PosHolster) return;
                weapon.gameObject.transform.localPosition = weapon.WeaponData.PosHolster;
                weapon.gameObject.transform.localRotation = Quaternion.Euler(weapon.WeaponData.RosHolster);
            }
            if (!isHolstering)
            {
                if (weapon.gameObject.transform.localPosition == weapon.WeaponData.PosEquip) return;
                Debug.Log("run333");
                weapon.gameObject.transform.localPosition = weapon.WeaponData.PosEquip;
                weapon.gameObject.transform.localRotation = Quaternion.Euler(weapon.WeaponData.RosEquip);
            }
        }
    }
}
