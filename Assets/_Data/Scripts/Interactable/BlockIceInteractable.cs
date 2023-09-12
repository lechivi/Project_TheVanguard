using UnityEngine;

public class BlockIceInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Can't go further";

    public bool CanInteract()
    {
        return true;
    }

    public string GetInteractableText()
    {
        return this.interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransfrom)
    {
        
    }
}
