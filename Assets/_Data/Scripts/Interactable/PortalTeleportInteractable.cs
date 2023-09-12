using UnityEngine;

public class PortalTeleportInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Travel to the Dungeon";

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
        if (UIManager.HasInstance)
        {
            UIManager.Instance.PopoutContainer.ShowIG_TravelDungeonPopout();
        }
    }
}
