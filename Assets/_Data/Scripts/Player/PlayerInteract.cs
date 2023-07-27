using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : PlayerAbstract
{
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private float pickupDistance = 7.5f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IInteractable interactable = this.GetInteractableObjectByRaycast();
            if (interactable != null)
            {
                interactable.Interact(transform);
            }
        }
    }

    public IInteractable GetInteractableObject()
    {
        List<IInteractable> interactableList = new List<IInteractable>();
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, this.interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                if (interactable.CanInteract())
                    interactableList.Add(interactable);
            }
        }

        IInteractable closestInteractable = null;
        foreach (IInteractable interactable in interactableList)
        {
            if (closestInteractable == null)
            {
                closestInteractable = interactable;
            }
            else
            {
                if (Vector3.Distance(transform.position, interactable.GetTransform().position) <
                    Vector3.Distance(transform.position, closestInteractable.GetTransform().position))
                {
                    closestInteractable = interactable;
                }
            }
        }

        return closestInteractable;
    }

    public IInteractable GetInteractableObjectByRaycast()
    {
        Physics.Raycast(this.playerCamera.transform.position, this.playerCamera.transform.forward, out RaycastHit hitInfo, this.pickupDistance, this.pickupLayer);
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
        Gizmos.DrawLine(this.playerCamera.transform.position, this.playerCamera.transform.position + (this.playerCamera.transform.forward * this.pickupDistance));
    }
}
