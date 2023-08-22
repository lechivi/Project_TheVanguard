using System.Collections.Generic;
using UnityEngine;

public class NPCShopkeeperInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private ChatBubble chatBubblePrefab;
    [SerializeField] private string interactText;
    [SerializeField] private List<string> chatQuote = new List<string>();

    public List<ItemDataSO> ItemList = new List<ItemDataSO>();

    private Animator animator;
    private NPCLookAt npcLookAt;
    private Transform chatParent;
    private Transform interactorTransfrom;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
        this.npcLookAt = GetComponent<NPCLookAt>();
        this.chatParent = transform.Find("ChatParent");
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
            chatBubble.Setup(this.chatQuote[Random.Range(0, this.chatQuote.Count)], 4f);
        }

        this.animator.SetTrigger("Talk");

        Transform targetLookAt = this.interactorTransfrom.GetComponent<PlayerInteract>().PlayerCtrl.PlayerTransform;
        if (targetLookAt != null)
        {
            this.npcLookAt.LookAtTarget(targetLookAt);
        }

        Invoke("DisplayShopUI", 2f);
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

    private void DisplayShopUI()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.InGamePanel.ShowOther(null);
            UI_ExchangePanel exchangePanel = UIManager.Instance.InGamePanel.InGame_Other.UI_ExchangePanel;
            exchangePanel.Show(null);
            exchangePanel.ShopList.SetShopList(this);
            exchangePanel.InventoryList.SetInventoryList();

            PlayerInput playerInput = this.interactorTransfrom.GetComponent<PlayerInteract>().PlayerCtrl.PlayerInput;
            playerInput.SetPlayerInput(false);

        }

    }
}
