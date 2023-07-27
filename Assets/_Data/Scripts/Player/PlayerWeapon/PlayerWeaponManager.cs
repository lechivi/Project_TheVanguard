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
    [SerializeField] private UI_InventoryPanel ui_InventoryPanel;

    [SerializeField] private int maxEquippedWeapon = 3;
    [SerializeField] private int maxBackpackWeapon = 3;
    [SerializeField] private NullAwareList<Weapon> equippedWeapons = new NullAwareList<Weapon>();
    [SerializeField] private NullAwareList<Weapon> backpackWeapons = new NullAwareList<Weapon>();
    [SerializeField] private Transform[] weaponHolderSlots = new Transform[4];

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform dropPoint;
    [SerializeField] private float dropForce = 3f;
    [SerializeField] private GameObject pickupObjectPrefab;

    [Header("Offset Weapon Holder Slot")]
    [SerializeField] private Vector3 offsetPosMeleeHolderBack = new Vector3(0, 0.11f, -0.27f);
    [SerializeField] private Vector3 offsetRotMeleeHolderBack = new Vector3(90, 0, 0);
    [SerializeField] private Vector3 offsetPosMeleeHolderLeft = new Vector3(0, 0, 0.08f);
    [SerializeField] private Vector3 offsetRotMeleeHolderLeft = new Vector3(-90, -90, -90);

    private int currentWeaponIndex = -1;
    private bool isReadySwap = true;
    [SerializeField] private float swapCooldown = 0.2f;

    protected override void Awake()
    {
        base.Awake();
        PlayerWeaponManager.Instance = this;
        this.equippedWeapons.GenerateList(this.maxEquippedWeapon);
        this.backpackWeapons.GenerateList(this.maxBackpackWeapon);
    }

    private void Update()
    {
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
                this.SetActiveWeapon(2, true);
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            int index = this.equippedWeapons.GetList().FindIndex(item => item != null);
            if (index == -1) return;
            this.RemoveWeaponFromEquipped(this.equippedWeapons.GetList()[index]);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            int index = this.backpackWeapons.GetList().FindIndex(item => item != null);
            if (index == -1) return;
            this.RemoveWeaponFromBackpack(this.backpackWeapons.GetList()[index]);
        }

        //if (Input.GetKeyDown(KeyCode.Alpha7))
        //{
        //    this.SwitchWeapon(this.equippedWeapons.GetList()[0], this.equippedWeapons.GetList()[2]);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha8))
        //{
        //    this.SwitchWeapon(this.equippedWeapons.GetList()[0], this.backpackWeapons.GetList()[0]);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    this.SwitchWeapon(this.backpackWeapons.GetList()[0], this.equippedWeapons.GetList()[0]);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    this.SwitchWeapon(this.backpackWeapons.GetList()[0], this.backpackWeapons.GetList()[2]);
        //}

        if (Input.GetKeyDown(KeyCode.I))
        {
            this.PlayerWeapon.PlayerCtrl.PlayerInput.gameObject.SetActive(false);
        }
    }

    public bool AddWeapon(Weapon weapon)
    {
        if (this.equippedWeapons.GetList().Count < this.maxEquippedWeapon || (this.equippedWeapons.ContainsNull() && this.equippedWeapons.GetList().Count <= this.maxEquippedWeapon))
        {
            this.AddWeaponToEquipped(this.GetNewWeapon(weapon));
            this.UpdateUI();
            return true;
        }
        if (this.backpackWeapons.GetList().Count < this.maxBackpackWeapon || (this.backpackWeapons.ContainsNull() && this.backpackWeapons.GetList().Count <= this.maxBackpackWeapon))
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
        this.equippedWeapons.Add(weapon);

        this.SetHolsterForWeapon(weapon);

        if (this.currentWeaponIndex == -1)
        {
            this.SetActiveWeapon(this.equippedWeapons.GetList().IndexOf(weapon), false);
        }
    }

    private void AddWeaponToBackpack(Weapon weapon)
    {
        this.backpackWeapons.Add(weapon);

        this.SetHolsterForWeapon(weapon);
    }

    private void SetHolsterForWeapon(Weapon weapon)
    {
        weapon.transform.SetParent(this.weaponHolderSlots[(int)weapon.WeaponSlot[0] - 1]); //TODO: Change to RigLayer

        if (weapon.WeaponData.WeaponType == WeaponType.Melee)
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
        }

        weapon.gameObject.SetActive(false);
    }

    public void RemoveWeaponFromEquipped(int weaponIndex)
    {
        Weapon weapon = this.equippedWeapons.GetList()[weaponIndex];
        this.RemoveWeaponFromEquipped(weapon);
    }

    public void RemoveWeaponFromEquipped(Weapon weapon)
    {
        this.equippedWeapons.Remove(weapon);
        this.DropWeapon(weapon);
    }

    public void RemoveWeaponFromBackpack(int weaponIndex)
    {
        Weapon weapon = this.backpackWeapons.GetList()[weaponIndex];
        this.RemoveWeaponFromBackpack(weapon);
    }

    public void RemoveWeaponFromBackpack(Weapon weapon)
    {
        this.backpackWeapons.Remove(weapon);
        this.DropWeapon(weapon);
    }

    private void DropWeapon(Weapon weapon)
    {
        GameObject pickupObject = Instantiate(this.pickupObjectPrefab, this.dropPoint.position, this.dropPoint.rotation, this.spawnPoint);
        PickupWeapons pickupWeapon = pickupObject.GetComponent<PickupWeapons>();
        pickupWeapon.SetWeapon(weapon.gameObject);

        Rigidbody weaponRigidbody = pickupObject.transform.GetComponent<Rigidbody>();
        if (weaponRigidbody != null)
        {
            float randomDropForce = Random.Range(this.dropForce - 1f, this.dropForce + 1f);
            Vector3 randomDropDirection = new Vector3(Random.Range(-0.5f, 0.6f), 0, 0);
            weaponRigidbody.AddForce((this.dropPoint.forward + randomDropDirection) * randomDropForce, ForceMode.Impulse);
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

        if (isTheCurrentWeaponA)
            this.SetActiveWeapon(weaponIndexA, true);
        if (isTheCurrentWeaponB) 
            this.SetActiveWeapon(weaponIndexB, true);
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
        if (weaponIndex == this.currentWeaponIndex && listEquippedWeapon[weaponIndex] == null) return;

        if (this.currentWeaponIndex > -1 && listEquippedWeapon[currentWeaponIndex] != null)
        {
            listEquippedWeapon[this.currentWeaponIndex].gameObject.SetActive(false);
        }

        if (listEquippedWeapon[weaponIndex] == null) return;
        listEquippedWeapon[weaponIndex].gameObject?.SetActive(true);

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
        List<UI_DraggableItem> equippedList = this.ui_InventoryPanel.EquippedListPanel;
        List<UI_DraggableItem> backpackList = this.ui_InventoryPanel.BackpackListPanel;

        for (int i = 0; i < this.equippedWeapons.GetList().Count; i++)
        {
            if (this.equippedWeapons.GetList()[i] == null) continue;
            equippedList[i].SetWeaponData(this.equippedWeapons.GetList()[i].WeaponData);
            equippedList[i].SetModel();
            //GameObject weaponIcon = Instantiate(this.equippedWeapons.GetList()[i].WeaponData.Icon, equippedList[i].transform);
            //equippedList[i].WeaponIconObject = weaponIcon;
        }
        for (int i = 0; i < this.backpackWeapons.GetList().Count; i++)
        {
            if (this.backpackWeapons.GetList()[i] == null) continue;
            backpackList[i].SetWeaponData(this.backpackWeapons.GetList()[i].WeaponData);
            backpackList[i].SetModel();
            //GameObject weaponIcon = Instantiate(this.backpackWeapons.GetList()[i].WeaponData.Icon, backpackList[i].transform);
            //backpackList[i].WeaponIconObject = weaponIcon;
        }

        if (this.currentWeaponIndex == -1) return;
        this.ui_InventoryPanel.UI_EquippedListManager.SetEquipSlot(this.currentWeaponIndex);
    }
}
