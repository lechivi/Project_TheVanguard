using UnityEngine;

public interface IInteractable
{
    void Interact(Transform interactorTransfrom);
    string GetInteractableText();
    Transform GetTransform();
    bool CanInteract();
}
