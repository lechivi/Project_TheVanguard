using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PlayerInteract : SaiMonoBehaviour
{
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private GameObject container;
    [SerializeField] private TMP_Text text;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.playerInteract == null)
            this.playerInteract = PlayerCtrl.Instance.PlayerInteract;

        if (this.container == null)
            this.container = transform.Find("Container").gameObject;

        if (this.text == null)
            this.text = transform.Find("Container").Find("Text").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (this.playerInteract.GetInteractableObjectByRaycast() != null)
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
        this.container.SetActive(true);
        this.text.SetText(this.playerInteract.GetInteractableObjectByRaycast().GetInteractableText());
    }

    private void Hide()
    {
        this.container.SetActive(false);
    }
}
