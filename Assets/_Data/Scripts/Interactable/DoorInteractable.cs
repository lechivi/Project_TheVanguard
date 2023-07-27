using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    private enum DoorType { InswingDoor, OutswingDoor };

    [SerializeField] private string interactText = "Open/Close the door";
    [SerializeField] private DoorType doorType;

    private Animator animator;
    private bool isOpen;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();

        this.animator.SetBool("IsInswing", this.doorType == DoorType.InswingDoor);
        this.isOpen = transform.localRotation.y != 0;
    }
    public void Interact(Transform interactorTransfrom)
    {
        this.isOpen = !this.isOpen;

        this.animator.SetTrigger("Trigger");
        this.animator.SetBool("IsOpen", this.isOpen);

    }

    public string GetInteractableText()
    {
        this.interactText = this.isOpen ? "Close the door" : "Open the door";
        return this.interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool CanInteract()
    {
        return true;
    }
}
