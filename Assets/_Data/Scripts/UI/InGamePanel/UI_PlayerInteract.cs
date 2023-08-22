using UnityEngine;
using TMPro;

public class UI_PlayerInteract : BaseUIElement
{
    [Header("UI_PLAYER INTERACT")]
    [SerializeField] private TMP_Text text;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.text == null)
            this.text = transform.Find("Container").Find("Interaction_Text").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (PlayerCtrl.Instance.PlayerInteract.GetInteractableObjectByRaycast() != null)
        {
            this.Show(null);
        }
        else
        {
            this.Hide();
        }
    }
}
