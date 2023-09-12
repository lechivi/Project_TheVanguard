using UnityEngine;

public class NPCNinjaInteractable : NPCInteractable
{
    [SerializeField] protected GameObject gift;

    protected bool isGift;

    public override void Interact(Transform interactorTransfrom)
    {
        this.interactorTransfrom = interactorTransfrom;
        if (this.chatBubblePrefab != null && this.chatQuote.Count > 0)
        {
            foreach (Transform child in this.chatParent)
            {
                Destroy(child.gameObject);
            }
            ChatBubble chatBubble = Instantiate(this.chatBubblePrefab, this.chatParent);
            chatBubble.Setup(!this.isGift ? this.chatQuote[0] : this.chatQuote[1], 3.9f);
        }

        this.npcCtrl.SetAnimaton(NPCBehaviour.Wave.ToString(), 3f);

        Transform targetLookAt = this.interactorTransfrom.GetComponent<PlayerInteract>().PlayerCtrl.PlayerTransform;
        if (targetLookAt != null)
        {
            this.npcLookAt.LookAtTarget(targetLookAt);
        }

        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayVc(!this.isGift ? AUDIO.VC_MALE_HEYBUDDY_APP_ANNOUNCER_POSITIVE_MALE_HEY_BUDDY : AUDIO.VC_MALE_TAKECARE_APP_ANNOUNCER_POSITIVE_MALE_TAKE_CARE);
        }

        if (!this.isGift)
        {
            Invoke("Gift", 1f);
        }
    }

    private void Gift()
    {
        this.gift.SetActive(true);
        this.isGift = true;
    }
}
