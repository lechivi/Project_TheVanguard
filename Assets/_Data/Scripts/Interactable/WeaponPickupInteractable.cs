using UnityEngine;

public class WeaponPickupInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] protected string interactText;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform fakeParent;
    private Vector3 offsetPot;

    private void Start()
    {
        if (this.fakeParent == null) return;
        this.offsetPot = transform.position - fakeParent.position;
    }

    public void Interact(Transform interactorTransfrom)
    {
        PlayerWeaponManager playerWeaponManager = interactorTransfrom.parent.GetComponentInChildren<PlayerWeaponManager>();
        if (playerWeaponManager != null && this.weapon != null)
        {
            bool canAdd = playerWeaponManager.AddWeapon(this.weapon);
            if (canAdd)
            {
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    public string GetInteractableText()
    {
        return this.interactText + this.weapon.WeaponData.ItemName;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool CanInteract()
    {
        return true;
    }

    private void FixedUpdate()
    {
        if (this.fakeParent == null) return;
        this.transform.position = this.fakeParent.position + this.offsetPot;
    }
}
