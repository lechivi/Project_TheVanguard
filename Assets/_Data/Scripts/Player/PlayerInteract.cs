using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : PlayerAbstract
{
    [SerializeField] private Transform raycastOriginal;
    [SerializeField] private float interactDistance = 7.5f;
    [SerializeField] private LayerMask pickupLayer;

    public Transform InteractableRaycastObject;

    private void Update()
    {
        if(playerCtrl.PlayerCamera.TPSCam.gameObject.activeInHierarchy == true)
        {
            this.raycastOriginal = InteractableRaycastObject.transform;
        }
        else
        {
            this.raycastOriginal = Camera.main.transform;
        }
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.raycastOriginal == null)
            this.raycastOriginal = /*Camera.main.transform;*/InteractableRaycastObject.transform;
    }
    public void Interact()
    {
        IInteractable interactable = this.GetInteractableObjectByRaycast();
        if (interactable != null)
        {
            interactable.Interact(transform);
        }
    }

    //[SerializeField] private float interactRange = 2f;
    //public IInteractable GetInteractableObject()
    //{
    //    List<IInteractable> interactableList = new List<IInteractable>();
    //    Collider[] colliderArray = Physics.OverlapSphere(transform.position, this.interactRange);
    //    foreach (Collider collider in colliderArray)
    //    {
    //        if (collider.TryGetComponent(out IInteractable interactable))
    //        {
    //            if (interactable.CanInteract())
    //                interactableList.Add(interactable);
    //        }
    //    }

    //    IInteractable closestInteractable = null;
    //    foreach (IInteractable interactable in interactableList)
    //    {
    //        if (closestInteractable == null)
    //        {
    //            closestInteractable = interactable;
    //        }
    //        else
    //        {
    //            if (Vector3.Distance(transform.position, interactable.GetTransform().position) <
    //                Vector3.Distance(transform.position, closestInteractable.GetTransform().position))
    //            {
    //                closestInteractable = interactable;
    //            }
    //        }
    //    }

    //    return closestInteractable;
    //}

    public IInteractable GetInteractableObjectByRaycast()
    {
        Physics.Raycast(this.raycastOriginal.transform.position, this.raycastOriginal.transform.forward, 
            out RaycastHit hitInfo, this.interactDistance, this.pickupLayer);
        if (hitInfo.collider != null && hitInfo.transform.TryGetComponent(out IInteractable interactable))
        {
            if (interactable.CanInteract())
                return interactable;
            else
                return null;
        }
        return null;
    }

}
