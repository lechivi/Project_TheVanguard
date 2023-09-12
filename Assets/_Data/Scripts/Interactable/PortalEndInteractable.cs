using UnityEngine;

public class PortalEndInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Exit the Dungeon";

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
        Debug.Log("asda");
        if (UIManager.HasInstance)
        {
            UIManager.Instance.InGamePanel.ShowOther(null);
            UIManager.Instance.InGamePanel.Other.ShowVictoryPanel();
        }
    }
}
