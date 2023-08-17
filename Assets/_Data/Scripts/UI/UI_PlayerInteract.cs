using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PlayerInteract : SaiMonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text text;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.canvasGroup == null)
            this.canvasGroup = GetComponent<CanvasGroup>();

        if (this.text == null)
            this.text = transform.Find("Container").Find("Interaction_Text").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (PlayerCtrl.Instance.PlayerInteract.GetInteractableObjectByRaycast() != null)
        {
            this.Show();
        }
        else
        {
            this.Hide();
        }
    }

    private void Show()
    {
        this.canvasGroup.alpha = 1;
        this.text.SetText(PlayerCtrl.Instance.PlayerInteract.GetInteractableObjectByRaycast().GetInteractableText());
    }

    private void Hide()
    {
        this.canvasGroup.alpha = 0;
    }
}
