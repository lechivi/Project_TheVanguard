using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [SerializeField] private Transform chatParentTransform;
    [SerializeField] private ChatBubble chatBubblePrefab;

    private Animator animator;
    private NPCLookAt npcLookAt;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
        this.npcLookAt = GetComponent<NPCLookAt>();
    }

    public void Interact(Transform interactorTransfrom)
    {
        if (this.chatBubblePrefab != null)
        {
            ChatBubble chatBubble = Instantiate(this.chatBubblePrefab, this.chatParentTransform);
            chatBubble.Setup("Hello Adventure! Do you want a gun?", 4f);
        }

        this.animator.SetTrigger("Talk");
        this.npcLookAt.LookAtTarget(interactorTransfrom);
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
        return true;
    }
}
