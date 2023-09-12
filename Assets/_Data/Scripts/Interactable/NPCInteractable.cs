using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : SaiMonoBehaviour, IInteractable
{
    [SerializeField] protected ChatBubble chatBubblePrefab;
    [SerializeField] protected string interactText;
    [SerializeField] protected List<string> chatQuote = new List<string>();
    [SerializeField] protected bool isFemale;
    [SerializeField] protected float chatExistTime = 3.9f;

    [SerializeField] protected NPCCtrl npcCtrl;
    [SerializeField] protected NPCLookAt npcLookAt;
    [SerializeField] protected Transform chatParent;

    protected Transform interactorTransfrom;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.npcCtrl == null)
            this.npcCtrl = GetComponent<NPCCtrl>(); 

        if (this.npcLookAt == null)
            this.npcLookAt = GetComponent<NPCLookAt>(); 

        if (this.chatParent == null)
            this.chatParent = transform.Find("ChatParent");

        if (this.chatBubblePrefab == null)
            this.chatBubblePrefab = (Resources.Load("Prefabs/Any/ChatBubble") as GameObject).GetComponent<ChatBubble>();
    }

    public virtual void Interact(Transform interactorTransfrom)
    {
        this.interactorTransfrom = interactorTransfrom;
        if (this.chatBubblePrefab != null && this.chatQuote.Count > 0)
        {
            foreach (Transform child in this.chatParent)
            {
                Destroy(child.gameObject);
            }
            ChatBubble chatBubble = Instantiate(this.chatBubblePrefab, this.chatParent);
            chatBubble.Setup(this.chatQuote[Random.Range(0, this.chatQuote.Count)], this.chatExistTime);
        }

        this.npcCtrl.SetAnimaton((Random.Range(0, 2) == 0 ? NPCBehaviour.Talk1.ToString() : NPCBehaviour.Wave.ToString()), this.chatExistTime);

        Transform targetLookAt = this.interactorTransfrom.GetComponent<PlayerInteract>().PlayerCtrl.PlayerTransform;
        if (targetLookAt != null)
        {
            this.npcLookAt.LookAtTarget(targetLookAt);
        }

        if (AudioManager.HasInstance)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
                AudioManager.Instance.PlayVc(this.isFemale ? AUDIO.VC_FEMALE_HEY_APP_ANNOUNCER_FEMALE_HEY_2 : AUDIO.VC_MALE_HEY_SOLDIER_HUNTER_HEY);
            else if (random == 1)
                AudioManager.Instance.PlayVc(this.isFemale ? AUDIO.VC_FEMALE_HI_APP_ANNOUNCER_FEMALE_HI_1 : AUDIO.VC_MALE_HI_SOLDIER_HUNTER_HI_2);
        }
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
