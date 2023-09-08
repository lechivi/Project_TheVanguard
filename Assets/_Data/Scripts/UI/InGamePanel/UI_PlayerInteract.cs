using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PlayerInteract : BaseUIElement
{
    [Header("UI_PLAYER INTERACT")]
    [SerializeField] private Image inputIconImage;
    [SerializeField] private TMP_Text text;

    public Image InputIconImage { get => this.inputIconImage; set => this.inputIconImage = value; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.inputIconImage == null)
            this.inputIconImage = transform.Find("Container/InputIcon_Image").GetComponent<Image>();

        if (this.text == null)
            this.text = transform.Find("Container/Interaction_Text").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (PlayerCtrl.HasInstance)
        {
            IInteractable interactable = PlayerCtrl.Instance.PlayerInteract.GetInteractableObjectByRaycast();
            if (interactable != null)
            {
                this.text.SetText(interactable.GetInteractableText());
                this.Show(null);
            }
            else
            {
                this.Hide();
            }
        }
    }
}
