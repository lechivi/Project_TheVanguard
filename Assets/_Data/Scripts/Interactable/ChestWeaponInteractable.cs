using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestWeaponInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Open chest";
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();
    [SerializeField] private GameObject pickupObjectPrefab;

    private List<GameObject> gameObjects = new List<GameObject>();
    private Animator animator;
    private bool isOpen = false;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    public void Interact(Transform interactorTransfrom)
    {
        this.isOpen = true;
        this.animator.SetTrigger("Trigger");
        this.animator.SetBool("IsOpen", true);
    }

    public string GetInteractableText()
    {
        return this.interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool CanInteract()
    {
        return !this.isOpen;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach(var obj in this.gameObjects) 
            {
                Destroy(obj);
            }
            this.gameObjects.Clear();
            this.Spawnitem();
        }
    }

    public void Spawnitem() //Call in animation
    {
        if (this.pickupObjectPrefab == null) return;
        int amout = this.weapons.Count;
        float minX = -1f;
        float maxX = 1f;

        for (int i = 0; i < this.weapons.Count; i++)
        {
            GameObject newPickupObject = Instantiate(this.pickupObjectPrefab, this.transform);
            //float posX = minX + ((i + 1) / (float)(this.weapons.Count + 1)) * (maxX - minX);
            newPickupObject.transform.localPosition = CalculateChildPosition(i, amout, 3, minX, maxX, 0.5f);


            PickupWeapons pickupWeapon = newPickupObject.GetComponentInChildren<PickupWeapons>();
            pickupWeapon.SetWeapon(this.weapons[i]);
            newPickupObject.GetComponent<Rigidbody>().isKinematic = true;

            this.gameObjects.Add(newPickupObject);
        }
    }

    private Vector3 CalculateChildPosition(int childIndex, int totalChildren, int maxColumn, float minX, float maxX, float distanceZ)
    {
        int totalRow = Mathf.CeilToInt(totalChildren / (float)maxColumn);
        while (totalRow > maxColumn)
        {
            maxColumn++;
            totalRow = Mathf.CeilToInt(totalChildren / (float)maxColumn);
        }

        int row = childIndex / maxColumn;
        int column = childIndex % maxColumn;
        int lastRowCount = totalChildren % maxColumn;
        int rowCount;
        if (row == totalRow - 1 && lastRowCount > 0 && lastRowCount < maxColumn)
        {
            rowCount = lastRowCount;
        }
        else
        {
            rowCount = maxColumn;
        }

        float positionX = minX + ((column + 1) / (float)(rowCount + 1)) * (maxX - minX);
        float positionZ;
        if (totalRow % 2 == 1)
        {
            positionZ = (row - (totalRow - 1) / 2) * distanceZ;
        }
        else
        {
            positionZ = (row - totalRow / 2 + 0.5f) * distanceZ;
        }

        return new Vector3(positionX, 0.5f, positionZ);
    }
}
