using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : PlayerAbstract
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float interactDistance = 7.5f;
    [SerializeField] private LayerMask pickupLayer;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.mainCamera == null)
            this.mainCamera = Camera.main.transform;
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
        Physics.Raycast(this.mainCamera.transform.position, this.mainCamera.transform.forward, 
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(this.mainCamera.transform.position, this.mainCamera.transform.position + 
            (this.mainCamera.transform.forward * this.interactDistance));
    }
}
