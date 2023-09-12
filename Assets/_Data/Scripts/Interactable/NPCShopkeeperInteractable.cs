using System.Collections.Generic;
using UnityEngine;

public class NPCShopkeeperInteractable : NPCInteractable
{
    public List<ItemDataSO> ItemList = new List<ItemDataSO>();

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
            chatBubble.Setup(this.chatQuote[Random.Range(0, this.chatQuote.Count)], this.chatExistTime);
        }

        this.npcCtrl.SetAnimaton("Talk", this.chatExistTime);

        Transform targetLookAt = this.interactorTransfrom.GetComponent<PlayerInteract>().PlayerCtrl.PlayerTransform;
        if (targetLookAt != null)
        {
            this.npcLookAt.LookAtTarget(targetLookAt);
        }

        if (AudioManager.HasInstance)
        {
            //int random = Random.Range(0, 3);
            //if (random == 0)
            //    AudioManager.Instance.PlayVc(this.isFemale ? AUDIO.VC_FEMALE_HEY_APP_ANNOUNCER_FEMALE_HEY_2 : AUDIO.VC_MALE_HEY_SOLDIER_HUNTER_HEY);
            //else if (random == 1)
            //    AudioManager.Instance.PlayVc(this.isFemale ? AUDIO.VC_FEMALE_HI_APP_ANNOUNCER_FEMALE_HI_1 : AUDIO.VC_MALE_HI_SOLDIER_HUNTER_HI_2);         
            //else
            //    AudioManager.Instance.PlayVc(this.isFemale ? AUDIO.VC_FEMALE_WELCOME_APP_ANNOUNCER_FEMALE_WELCOME_1 : AUDIO.VC_MALE_WELCOME_SOLDIER_HUNTER_WELCOME);         
            AudioManager.Instance.PlayVc(this.isFemale ? AUDIO.VC_FEMALE_WELCOME_APP_ANNOUNCER_FEMALE_WELCOME_1 : AUDIO.VC_MALE_WELCOME_SOLDIER_HUNTER_WELCOME);
        }

        Invoke("DisplayShopUI", 2f);
    }

    private void DisplayShopUI()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.InGamePanel.ShowOther(null);
            UIManager.Instance.InGamePanel.Other.ShowExchangePanel();

            UI_ExchangePanel exchangePanel = UIManager.Instance.InGamePanel.Other.ExchangePanel;
            exchangePanel.ShopList.SetShopList(this);
            exchangePanel.InventoryList.SetInventoryList();

            PlayerInput playerInput = this.interactorTransfrom.GetComponent<PlayerInteract>().PlayerCtrl.PlayerInput;
            playerInput.SetPlayerInput(false);

        }

    }
}
